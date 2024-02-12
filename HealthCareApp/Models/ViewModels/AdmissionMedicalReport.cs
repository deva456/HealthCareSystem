using HealthcareApp.Models.DataModels;

namespace HealthcareApp.Models.ViewModels
{
    public class AdmissionMedicalReport
    {
        public PatientAdmission PatientAdmission { get; set; } = null!;
        public MedicalReport? MedicalReport { get; set; }
    }
}
