using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthcareApp.Models.DataModels;
using HealthcareApp.Repository;
using HealthcareApp.Repository.Interface;
using HealthcareApp.Models.ViewModels;

namespace HealthcareApp.Controllers
{
    public class MedicalReportsController : Controller
    {
        private readonly IMedicalReportRepository _medicalReportRepository;
        private readonly IPatientAdmissionRepository _patientAdmissionRepository;

        public MedicalReportsController(IMedicalReportRepository medicalReportRepository, IPatientAdmissionRepository patientAdmissionRepository)
        {
            _medicalReportRepository = medicalReportRepository;
            _patientAdmissionRepository = patientAdmissionRepository;
        }

        // GET: MedicalReports
        public async Task<IActionResult> Index()
        {
            return View(await _medicalReportRepository.GetAllDetailedMedicalReports());
        }

        // GET: MedicalReports/Partial/{Id}
        public async Task<IActionResult> Partial(Guid admissionId)
        {
            var medicalReport = (await _medicalReportRepository.FindBy(m => m.PatientAdmissionId == admissionId)).FirstOrDefault();
            var admission = await _patientAdmissionRepository.GetById(admissionId);
            var partViewModel = new MedicalReportPartialViewModel() { MedicalReport = medicalReport, AdmissionId = admissionId, 
                                                                      IsCancelled = (admission is not null && admission.IsCancelled) ? true : null};
            
            return PartialView("MedicalRecordPartial", partViewModel);
        }

        // GET: MedicalReports/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var medicalReport = await _medicalReportRepository.GetDetailedMedicalReport(id.Value);
            if (medicalReport is null)
            {
                return NotFound();
            }

            return View(medicalReport);
        }

        // GET: MedicalReports/Create
        public async Task<IActionResult> Create(Guid id)
        {
            var admission = await _patientAdmissionRepository.GetDetailedPatientAdmission(id);
            if (admission is null)
            {
                return NotFound($"Admission with id {id} not found.");
            }
            ViewBag.PatientAdmission = admission;
            var medicalReport = new MedicalReport() { PatientAdmissionId = id };
            return View(medicalReport);
        }

        // POST: MedicalReports/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,DateCreated,PatientAdmissionId")] MedicalReport medicalReport)
        {
            var admission = await _patientAdmissionRepository.GetDetailedPatientAdmission(medicalReport.PatientAdmissionId);
            if (ModelState.IsValid)
            {
                await _medicalReportRepository.Add(medicalReport);
                return RedirectToAction("Details", "Patients", new { id = admission.PatientId });
            }
            ViewBag.PatientAdmission = admission;
            return View(medicalReport);
        }

        // GET: MedicalReports/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var medicalReport = await _medicalReportRepository.GetDetailedMedicalReport(id.Value);
            if (medicalReport is null)
            {
                return NotFound();
            }

            var admission = await _patientAdmissionRepository.GetDetailedPatientAdmission(medicalReport.PatientAdmissionId);
            ViewBag.PatientAdmission = admission;
            return View(medicalReport);
        }

        // POST: MedicalReports/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Description,DateCreated,PatientAdmissionId")] MedicalReport medicalReport)
        {
            if (id != medicalReport.Id)
            {
                return NotFound();
            }
            var admission = await _patientAdmissionRepository.GetDetailedPatientAdmission(medicalReport.PatientAdmissionId);
            if (ModelState.IsValid)
            {
                try
                {
                   await _medicalReportRepository.Update(medicalReport);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await _medicalReportRepository.Exists(medicalReport.Id)))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Patients", new { id = admission.PatientId });
            }
            ViewBag.PatientAdmission = admission;
            return View(medicalReport);
        }
    }
}
