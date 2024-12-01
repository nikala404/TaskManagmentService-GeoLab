
namespace TaskManagmentLibrary.Validations
{
    public static class TaskValidations
    {
        public static bool IsValidTitle(string title)
        {
            return !string.IsNullOrWhiteSpace(title) && title.Length >= 1 && title.Length <= 100;
        }

        public static bool IsValidDescription(string description)
        {
            return string.IsNullOrWhiteSpace(description) || description.Length <= 4000;
        }

        public static bool IsValidPriority(string priority)
        {
            var validPriorities = new[] { "Low", "Medium", "High" };
            return Array.Exists(validPriorities, p => p.Equals(priority, StringComparison.OrdinalIgnoreCase));
        }

        public static bool IsValidCompletionDate(DateTime? completionDate)
        {
            return !completionDate.HasValue || completionDate.Value.Date >= DateTime.Now.Date;
        }
    }
}
