using Microsoft.AspNetCore.Mvc.Rendering;

namespace HealthcareApp.Utils
{
    public static class EnumExtension
    {
        public static IEnumerable<SelectListItem> ToSelectList(this Enum enumValue)
        {
            return from Enum e in Enum.GetValues(enumValue.GetType())
                   select new SelectListItem
                   {
                       Text = e.ToString(),
                       Value = e.ToString()
                   };
        }
    }
}
