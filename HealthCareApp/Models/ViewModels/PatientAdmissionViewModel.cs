using HealthcareApp.Models.DataModels;
using System.ComponentModel.DataAnnotations;

namespace HealthcareApp.Models.ViewModels
{
    public class PatientAdmissionViewModel
    {
        public IEnumerable<PatientAdmission> PatientAdmissions { get; set; } = new List<PatientAdmission>();
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }
    }
}
