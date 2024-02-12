using HealthcareApp.Models.Shared;
using HealthcareApp.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthcareApp.Models.DataModels
{
    public class Patient : BaseDataModel
    {
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Firstname { get; set; } = null!;

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Lastname { get; set; } = null!;

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DateNotInFuture(ErrorMessage = "Date of birth cannot be in future.")]
        public DateTime DateOfBirth { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public Gender Gender { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string? Address { get; set; }

        [Display(Name = "Telephone Number")]
        [StringLength(25, MinimumLength = 6)]
        public string? TelephoneNumber { get; set; }

        public bool IsDeleted { get; set; }

        public string FullName
        {
            get { return Firstname + " " + Lastname; }
        }

        public ICollection<PatientAdmission> PatientAdmissions { get; } = new List<PatientAdmission>();

    }
}
