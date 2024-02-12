using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthcareApp.Models.DataModels
{
    public class MedicalReport : BaseDataModel
    {
        [Required]
        [StringLength(1024)]
        public string Description { get; set; } = null!;

        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Display(Name = "Patient Admission Id")]
        public Guid PatientAdmissionId { get; set; }

        [Display(Name = "Patient Admission")]
        [ForeignKey(nameof(PatientAdmissionId))]
        public PatientAdmission? PatientAdmission { get; init; }

    }
}
