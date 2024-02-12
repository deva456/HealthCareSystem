using HealthcareApp.Models.DataModels;
using HealthcareApp.Models.ViewModels;

namespace HealthcareApp.Repository.Interface
{
    public interface IMedicalReportRepository : ICrudRepository<MedicalReport>
    {
        public Task<List<MedicalReport>> GetAllDetailedMedicalReports();
        public Task<MedicalReport?> GetDetailedMedicalReport(Guid id);
        public Task<List<AdmissionMedicalReport>> GetAdmissionMedicalReportList(List<PatientAdmission> admissions);

    }
}
