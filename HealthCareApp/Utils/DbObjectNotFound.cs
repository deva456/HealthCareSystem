namespace HealthcareApp.Utils
{
    public class DbObjectNotFound : Exception
    {
        public DbObjectNotFound(string? message) : base(message)
        {
        }
    }
}
