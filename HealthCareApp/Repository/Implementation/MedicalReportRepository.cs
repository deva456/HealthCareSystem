using HealthcareApp.Models.DataModels;
using HealthcareApp.Models.ViewModels;
using HealthcareApp.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApp.Repository.Implementation
{
    public class MedicalReportRepository : CrudRepository<MedicalReport>, IMedicalReportRepository
    {
        public MedicalReportRepository(HealthcareDbContext context) : base(context)
        {
        }

        public override async Task Add(MedicalReport entity)
        {
            entity.DateCreated = DateTime.Now;
            await base.Add(entity);
        }

        public async Task<List<MedicalReport>> GetAllDetailedMedicalReports()
        {
            return await _context.MedicalReports.Include(p => p.PatientAdmission).ToListAsync();
        }

        public async Task<MedicalReport?> GetDetailedMedicalReport(Guid id)
        {
            return await _context.MedicalReports.Include(p => p.PatientAdmission).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<AdmissionMedicalReport>> GetAdmissionMedicalReportList (List<PatientAdmission> admissions)
        {
            var admissionReportList = new List<AdmissionMedicalReport>();
            foreach (PatientAdmission admission in admissions)
            {
                var medicalReport = (await base.FindBy(m => m.PatientAdmissionId == admission.Id)).FirstOrDefault();
                var admissionReportItem = new AdmissionMedicalReport() { PatientAdmission = admission, MedicalReport = medicalReport };
                admissionReportList.Add(admissionReportItem);
            }
            return admissionReportList;
        }
    }
}
