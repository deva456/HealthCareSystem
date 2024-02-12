using HealthcareApp.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthcareApp.Models.DataModels
{
    public class PatientAdmission : BaseDataModel
    {
        [Display(Name = "Admission Date/Time")]
        [DateNotInPast(ErrorMessage = "Admission date must not be in the past.")]
        public DateTime AdmissionDateTime { get; set; }

        [Display(Name = "Patient")]
        public Guid PatientId { get; set; }

        [Display(Name = "Doctor")]
        public Guid DoctorId { get; set; }

        [Display(Name = "Urgent")]
        public bool IsUrgent { get; set; }

        [Display(Name = "Cancelled")]
        public bool IsCancelled { get; set; }

        [ForeignKey(nameof(PatientId))]
        public Patient? Patient { get; init; }

        [ForeignKey(nameof(DoctorId))]
        public Doctor? Doctor { get; init; }

    }
}
