using HealthcareApp.Models.DataModels;

namespace HealthcareApp.Models.ViewModels
{
    public class MedicalReportPartialViewModel
    {
        public MedicalReport? MedicalReport { get; set; }
        public Guid AdmissionId { get; set; }
        public bool? IsCancelled { get; set; }
    }
}
