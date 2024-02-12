using HealthcareApp.Models.DataModels;
using HealthcareApp.Repository.Interface;

namespace HealthcareApp.Repository.Implementation
{
    public class DoctorRepository : CrudRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(HealthcareDbContext context) : base(context)
        {
        }

        public override async Task<List<Doctor>> GetAll()
        {
            return await base.FindBy(doctor => doctor.IsDeleted == false);
        }

        public override async Task<Doctor?> Delete(Guid id)
        {
            var doctor = await base.GetById(id);
            if (doctor is not null && !doctor.IsDeleted)
            {
               doctor.IsDeleted = true;
               await base.Update(doctor);
            }
            return doctor;
        }

        
    }
}
