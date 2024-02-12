using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthcareApp.Models.DataModels;
using HealthcareApp.Repository.Interface;
using HealthcareApp.Models.ViewModels;

namespace HealthcareApp.Controllers
{
    public class PatientsController : Controller
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IPatientAdmissionRepository _patientAdmissionRepository;

        public PatientsController(IPatientRepository patientRepository, IPatientAdmissionRepository patientAdmissionRepository)
        {
            _patientRepository = patientRepository;
            _patientAdmissionRepository = patientAdmissionRepository;
        }

        // GET: Patients
        public async Task<IActionResult> Index()
        {
            return View(await _patientRepository.GetAll());
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var patient = await _patientRepository.GetById(id.Value);
            if (patient == null)
            {
                return NotFound();
            }
            var patientAdmissions = await _patientAdmissionRepository.GetAllDetailedPatientAdmissions(null, null, patient.Id);
            ViewBag.PatientAdmissionsViewModel = new PatientAdmissionViewModel() { PatientAdmissions = patientAdmissions};
            ViewBag.PatientDetailId = id.Value;
            return View(patient);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Firstname,Lastname,DateOfBirth,Gender,Address,TelephoneNumber")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                await _patientRepository.Add(patient);
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var patient = await _patientRepository.GetById(id.Value);
            if (patient is null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Firstname,Lastname,DateOfBirth,Gender,Address,TelephoneNumber")] Patient patient)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _patientRepository.Update(patient);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await _patientRepository.Exists(patient.Id)))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var patient = await _patientRepository.GetById(id.Value);
            if (patient is null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var patient = await _patientRepository.GetById(id);
            if (patient is not null)
            {
                await _patientRepository.Delete(patient.Id);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
