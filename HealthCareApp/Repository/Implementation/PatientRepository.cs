using HealthcareApp.Models.DataModels;
using HealthcareApp.Repository.Interface;

namespace HealthcareApp.Repository.Implementation
{
    public class PatientRepository : CrudRepository<Patient>, IPatientRepository
    {
        public PatientRepository(HealthcareDbContext context) : base(context)
        {
        }

        public override async Task<List<Patient>> GetAll()
        {
            return await base.FindBy(patient => patient.IsDeleted == false);
        }

        public override async Task<Patient?> Delete(Guid id)
        {
            var patient = await base.GetById(id);
            if (patient is not null && !patient.IsDeleted)
            {
                patient.IsDeleted = true;
                await base.Update(patient);
            }
            return patient;
        }
    }
}
