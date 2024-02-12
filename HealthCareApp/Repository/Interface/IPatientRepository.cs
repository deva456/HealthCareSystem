using HealthcareApp.Models.DataModels;

namespace HealthcareApp.Repository.Interface
{
    public interface IPatientRepository : ICrudRepository<Patient>
    {
        // reserved for class specific database operation methods
    }
}
