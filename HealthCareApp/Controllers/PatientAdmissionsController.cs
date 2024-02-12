using HealthcareApp.Models.DataModels;
using HealthcareApp.Models.Shared;
using HealthcareApp.Models.ViewModels;
using HealthcareApp.Repository.Interface;
using HealthcareApp.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApp.Controllers
{
    public class PatientAdmissionsController : Controller
    {
        private readonly IPatientAdmissionRepository _patientAdmissionRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IMedicalReportRepository _medicalReportRepository;
        private readonly PdfGenerator _pdfGenerator;

        public PatientAdmissionsController(IPatientAdmissionRepository patientAdmissionRepository, IDoctorRepository doctorRepository, 
                                           IPatientRepository patientRepository, IMedicalReportRepository medicalRepository, PdfGenerator pdfGenerator)
        {
            _patientAdmissionRepository = patientAdmissionRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _medicalReportRepository = medicalRepository;
            _pdfGenerator = pdfGenerator;
        }

        #region Controller Action Methods
        // GET: PatientAdmissions
        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
        {
            var patientAdmissionVM = new PatientAdmissionViewModel() { PatientAdmissions = new List<PatientAdmission>() };
            if (startDate > endDate)
            {
                ViewData["ErrorMessage"] = "Start date cannot be after end date, please check your inputs.";
                startDate = endDate = null;
            }
            else
            {
                ViewData["ErrorMessage"] = null;
                patientAdmissionVM.PatientAdmissions = await _patientAdmissionRepository.GetAllDetailedPatientAdmissions(startDate, endDate, null);
            }
            patientAdmissionVM.StartDate = startDate;
            patientAdmissionVM.EndDate = endDate;
            return View(patientAdmissionVM);
        }

        // GET: PatientAdmissions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var patientAdmission = await _patientAdmissionRepository.GetDetailedPatientAdmission(id.Value);
            
            if (patientAdmission is null)
            {
                return NotFound();
            }

            if (patientAdmission.IsCancelled)
            {
                ViewBag.Cancelled = "Yes";
            }

            var medicalReport = (await _medicalReportRepository.FindBy(m => m.PatientAdmissionId == patientAdmission.Id)).FirstOrDefault();
            ViewBag.MedicalReport = new MedicalReportPartialViewModel() { MedicalReport = medicalReport, AdmissionId =  patientAdmission.Id };
            return View(patientAdmission);
        }

        // GET: PatientAdmissions/Create
        public async Task<IActionResult> Create(Guid? id)
        {
            var patientAdmission = new PatientAdmission();
            if (id is not null)
            {
                patientAdmission.PatientId = id.Value;  
            }
            patientAdmission.AdmissionDateTime = DateTime.Now.Date.AddDays(1);
            await LoadSelectLists();
            return View(patientAdmission);
        }

        // POST: PatientAdmissions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AdmissionDateTime,PatientId,DoctorId,IsUrgent")] PatientAdmission patientAdmission)
        {
            ViewBag.AdmissionState = null;
            if (ModelState.IsValid)
            {
                try
                {
                    patientAdmission.Id = new Guid();
                    await _patientAdmissionRepository.Add(patientAdmission);
                }
                catch (DbObjectNotFound e)
                {
                    ViewBag.AdmissionState = $"Patient Admission create operation failed: {e.Message}";
                }
                if (ViewBag.AdmissionState is null)
                {
                    return RedirectToAction("Details", "Patients", new { id = patientAdmission.PatientId });
                }
            }
            await LoadSelectLists(patientAdmission.DoctorId, patientAdmission.PatientId);
            return View(patientAdmission);
        }

        // GET: PatientAdmissions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var patientAdmission = await _patientAdmissionRepository.GetById(id.Value);
            if (patientAdmission is null)
            {
                return NotFound();
            }
            await LoadSelectLists(patientAdmission.DoctorId, patientAdmission.PatientId);
            return View(patientAdmission);
        }

        // POST: PatientAdmissions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,AdmissionDateTime,PatientId,DoctorId,IsUrgent")] PatientAdmission patientAdmission)
        {
            ViewBag.AdmissionState = null;
            if (id != patientAdmission.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _patientAdmissionRepository.Update(patientAdmission);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await _patientAdmissionRepository.Exists(patientAdmission.Id)))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch(DbObjectNotFound e)
                {
                    ViewBag.AdmissionState = $"Patient Admission update operation failed: {e.Message}";
                }
                if (ViewBag.AdmissionState is null)
                {
                    return RedirectToAction("Details", "Patients", new { id = patientAdmission.PatientId });
                }
            }
            await LoadSelectLists(patientAdmission.DoctorId, patientAdmission.PatientId);
            return View(patientAdmission);
        }

        // GET: PatientAdmissions/CancelAdmission?id=*&redirectToPatient=*
        public async Task<IActionResult> CancelAdmission(Guid id, bool redirectToPatient)
        {
            var patientAdmission = await _patientAdmissionRepository.GetById(id);
            if (patientAdmission is null)
            {
                return NotFound($"Patient Admission (id: {id}) not found, cancellation failed.");
            }

            patientAdmission.IsCancelled = true;
            try
            {
                await _patientAdmissionRepository.Update(patientAdmission);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await _patientAdmissionRepository.Exists(patientAdmission.Id)))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (DbObjectNotFound e)
            {
                return NotFound($"Patient Admission update operation failed: {e.Message}");
            }
            // when cancellation is done from Patient's page
            if (redirectToPatient)
            {
                return RedirectToAction("Details", "Patients", new { id = patientAdmission.PatientId });
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: PatientAdmissions/GeneratePdf?startDate=*&endDate=*&patientId=*
        public async Task<IActionResult> GeneratePdf(DateTime? startDate, DateTime? endDate, Guid? patientId)
        {
            var patientAdmissions = await _patientAdmissionRepository.GetAllDetailedPatientAdmissions(startDate, endDate, patientId);
            var admissionAndMedicalReportList = await _medicalReportRepository.GetAdmissionMedicalReportList(patientAdmissions);
            var x = _pdfGenerator.GetPdf(admissionAndMedicalReportList);
            return File(x, "application/pdf");
        }
        #endregion

        #region Helpers
        private async Task<List<SelectListItem>> GetSpecialistSelectList(Guid? selectedSpecialist)
        {
            var specialists = await _doctorRepository.FindBy(d => d.Title == DoctorTitle.Specialist && !d.IsDeleted);
            var selectList = new List<SelectListItem>();

            foreach(var specialist in specialists)
            {
                selectList.Add(new SelectListItem(text: specialist.FullName, value: specialist.Id.ToString(),
                                                  selected: IsSelected(specialist.Id, selectedSpecialist)));  
            }
            return selectList;
        }

        private async Task<List<SelectListItem>> GetPatientSelectList(Guid? selectedPatient)
        {
            var patients = await _patientRepository.FindBy(p => !p.IsDeleted);
            var selectList = new List<SelectListItem>();

            foreach (var patient in patients)
            {
                selectList.Add(new SelectListItem(text: patient.FullName, value: patient.Id.ToString(),
                                                  selected: IsSelected(patient.Id, selectedPatient)));
            }
            return selectList;
        }

        private static bool IsSelected(Guid currentGuid, Guid? selected)
        {
            return selected is not null && currentGuid.Equals(selected);
        }

        private async Task LoadSelectLists(Guid? selectedSpecialist = null, Guid? selectedPatient = null)
        {
            ViewBag.SpecialistId = await GetSpecialistSelectList(selectedSpecialist);
            ViewBag.PatientId = await GetPatientSelectList(selectedPatient);
        }
        #endregion 
    }
}
