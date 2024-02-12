using HealthcareApp.Models.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthcareApp.Models.DataModels
{
    public class Doctor : BaseDataModel
    {
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Firstname { get; set; } = null!;

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Lastname { get; set; } = null!;

        public bool IsDeleted { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public DoctorTitle Title { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Code { get; set; } = null!;

        public string FullName
        {
            get => Firstname + " " + Lastname; 
        }

        public string FullNameAndCode
        {
            get => FullName + " - " + Code; 
        }

        public ICollection<PatientAdmission> PatientAdmissions { get; } = new List<PatientAdmission>();
    }
}
