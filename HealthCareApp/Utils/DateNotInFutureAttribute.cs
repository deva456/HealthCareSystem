using System.ComponentModel.DataAnnotations;

namespace HealthcareApp.Utils
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class DateNotInFutureAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime selectedDateTime)
            {
                return selectedDateTime <= DateTime.Now;
            }
            return true;
        }
    }
}
