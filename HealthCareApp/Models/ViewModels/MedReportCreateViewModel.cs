using HealthcareApp.Models.DataModels;

namespace HealthcareApp.Models.ViewModels
{
    public class MedReportCreateViewModel
    {
        public MedicalReport MedicalReport { get; set; } = null!;
        public PatientAdmission PatientAdmission { get; set; } = null!;
    }
}
