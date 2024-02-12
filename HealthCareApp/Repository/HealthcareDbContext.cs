using HealthcareApp.Models.DataModels;
using HealthcareApp.Models.Shared;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApp.Repository
{
    public class HealthcareDbContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; } = null!;
        public DbSet<Patient> Patients { get; set; } = null!;
        public DbSet<MedicalReport> MedicalReports { get; set; } = null!;
        public DbSet<PatientAdmission> PatientAdmissions { get; set; } = null!;

        public HealthcareDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { Id = new("6a2e6559-3442-416c-afda-e35232824ce4"), Firstname = "James", 
                             Lastname = "Smith", Title = Models.Shared.DoctorTitle.Specialist, Code = "DC1" },
                new Doctor { Id = new("7b2e6559-3442-416c-afda-e35232824ce4"), Firstname = "Kevin", 
                             Lastname = "May", Title = Models.Shared.DoctorTitle.Specialist, Code = "DC2" },
                new Doctor { Id = new("8c2e6559-3442-416c-afda-e35232824ce4"), Firstname = "Jane", 
                             Lastname = "Johnson", Title = Models.Shared.DoctorTitle.Resident, Code = "RES1" },
                new Doctor { Id = new("9d2e6559-3442-416c-afda-e35232824ce4"), Firstname = "Michael", 
                             Lastname = "Abrams", Title = Models.Shared.DoctorTitle.Resident, Code = "RES2" },
                new Doctor { Id = new("1e2e6559-3442-416c-afda-e35232824ce4"), Firstname = "Ken", 
                             Lastname = "Richards", Title = Models.Shared.DoctorTitle.Nurse, Code = "NS1" },
                new Doctor { Id = new("2f2e6559-3442-416c-afda-e35232824ce4"), Firstname = "Melina", 
                             Lastname = "Diericks", Title = Models.Shared.DoctorTitle.Nurse, Code = "NS2" }
            );

            
            var startDate = new DateTime(1970, 1, 1);
            var endDate = new DateTime(2001, 12, 31);

            modelBuilder.Entity<Patient>().HasData(
                new Patient { Id = new("1200a45e-8914-47bf-9035-e85aaad2b261"), Firstname = "Michels", 
                              Lastname = "Jones", DateOfBirth = GenerateRandomDate(startDate, endDate), Gender = Gender.Male, Address = "Address 1", TelephoneNumber = "00023323" },
                new Patient { Id = new("b226f2a3-8f68-4c85-b162-4b2204f8665d"), Firstname = "Jenna", 
                              Lastname = "Lynn", DateOfBirth = GenerateRandomDate(startDate, endDate), Gender = Gender.Female },
                new Patient { Id = new("78eecc5e-5e7c-46d7-b472-3a1bddf289ba"), Firstname = "Wolf", 
                              Lastname = "Warren", DateOfBirth = GenerateRandomDate(startDate, endDate), Gender = Gender.Male, Address = "Address 2", TelephoneNumber = "555555" },
                new Patient { Id = new("73776586-b4db-4769-9228-f103e8499d4f"), Firstname = "Nick", 
                              Lastname = "Oakenfold", DateOfBirth = GenerateRandomDate(startDate, endDate), Gender = Gender.Male, Address = "Address 3", TelephoneNumber = "6666666" },
                new Patient { Id = new("2b99eca2-1421-4789-a3e1-00da3d953abe"), Firstname = "Hannah", 
                              Lastname = "Brown", DateOfBirth = GenerateRandomDate(startDate, endDate), Gender = Gender.Female, Address = "Address 4", TelephoneNumber = "7777777" },
                new Patient { Id = new("211a94b3-78cd-4a6b-babd-eb8c3a18cfea"), Firstname = "Brock", 
                              Lastname = "Wallace", DateOfBirth = GenerateRandomDate(startDate, endDate), Gender = Gender.Male, Address = "Address 5", TelephoneNumber = "1111122222" }
            );

            startDate = new(2020, 1, 1);
            endDate = DateTime.Now;
            modelBuilder.Entity<PatientAdmission>().HasData( 
                new PatientAdmission { Id = new("81aea768-8434-4468-9cd7-8034a105f31a"), AdmissionDateTime = GenerateRandomDate(startDate, endDate), DoctorId = new("7b2e6559-3442-416c-afda-e35232824ce4"), PatientId = new("1200a45e-8914-47bf-9035-e85aaad2b261") },
                new PatientAdmission { Id = new("11aea768-8434-4468-9cd7-8034a105f31a"), AdmissionDateTime = GenerateRandomDate(startDate, endDate), DoctorId = new("7b2e6559-3442-416c-afda-e35232824ce4"), PatientId = new("b226f2a3-8f68-4c85-b162-4b2204f8665d") },
                new PatientAdmission { Id = new("21aea768-8434-4468-9cd7-8034a105f31a"), AdmissionDateTime = GenerateRandomDate(startDate, endDate), DoctorId = new("7b2e6559-3442-416c-afda-e35232824ce4"), PatientId = new("78eecc5e-5e7c-46d7-b472-3a1bddf289ba") },
                new PatientAdmission { Id = new("31aea768-8434-4468-9cd7-8034a105f31a"), AdmissionDateTime = GenerateRandomDate(startDate, endDate), DoctorId = new("6a2e6559-3442-416c-afda-e35232824ce4"), PatientId = new("73776586-b4db-4769-9228-f103e8499d4f") },
                new PatientAdmission { Id = new("41aea768-8434-4468-9cd7-8034a105f31a"), AdmissionDateTime = GenerateRandomDate(startDate, endDate), DoctorId = new("6a2e6559-3442-416c-afda-e35232824ce4"), PatientId = new("2b99eca2-1421-4789-a3e1-00da3d953abe") },
                new PatientAdmission { Id = new("51aea768-8434-4468-9cd7-8034a105f31a"), AdmissionDateTime = GenerateRandomDate(startDate, endDate), DoctorId = new("6a2e6559-3442-416c-afda-e35232824ce4"), PatientId = new("211a94b3-78cd-4a6b-babd-eb8c3a18cfea") }
             );

            modelBuilder.Entity<MedicalReport>().HasData(
                new MedicalReport { Id = new("93024ef8-c1e7-4fea-b4e3-abf161b88196"), PatientAdmissionId = new("81aea768-8434-4468-9cd7-8034a105f31a"), Description = "Description 1", DateCreated = DateTime.Now },
                new MedicalReport { Id = new("33024ef8-c1e7-4fea-b4e3-abf161b88196"), PatientAdmissionId = new("11aea768-8434-4468-9cd7-8034a105f31a"), Description = "Description 2", DateCreated = DateTime.Now },
                new MedicalReport { Id = new("53024ef8-c1e7-4fea-b4e3-abf161b88196"), PatientAdmissionId = new("21aea768-8434-4468-9cd7-8034a105f31a"), Description = "Description 3", DateCreated = DateTime.Now },
                new MedicalReport { Id = new("13024ef8-c1e7-4fea-b4e3-abf161b88196"), PatientAdmissionId = new("31aea768-8434-4468-9cd7-8034a105f31a"), Description = "Description 4", DateCreated = DateTime.Now },
                new MedicalReport { Id = new("23024ef8-c1e7-4fea-b4e3-abf161b88196"), PatientAdmissionId = new("41aea768-8434-4468-9cd7-8034a105f31a"), Description = "Description 5", DateCreated = DateTime.Now },
                new MedicalReport { Id = new("63024ef8-c1e7-4fea-b4e3-abf161b88196"), PatientAdmissionId = new("51aea768-8434-4468-9cd7-8034a105f31a"), Description = "Description 6", DateCreated = DateTime.Now }
             );
        }

        private static DateTime GenerateRandomDate(DateTime startDate, DateTime endDate)
        {
            int range = (endDate - startDate).Days;
            var random = new Random();
            return startDate.AddDays(random.Next(range));
        }
    }
}
