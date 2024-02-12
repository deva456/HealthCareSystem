using HealthcareApp.Models.DataModels;

namespace HealthcareApp.Repository.Interface
{
    public interface IPatientAdmissionRepository : ICrudRepository<PatientAdmission>
    {
        Task<List<PatientAdmission>> GetAllDetailedPatientAdmissions(DateTime? startDate, DateTime? endDate, Guid? patientId);
        Task<PatientAdmission?> GetDetailedPatientAdmission(Guid id);
    }
}
