using HealthcareApp.Models.DataModels;

namespace HealthcareApp.Repository.Interface
{
    public interface IDoctorRepository: ICrudRepository<Doctor>
    {
        // reserved for class specific database operation methods
    }
}
