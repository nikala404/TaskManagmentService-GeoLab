using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TaskManagmentLibrary.Validations
{
    public static class StatusValidations
    {
        public static bool CanChangeStatus(string currentStatus, string newStatus)
        {
            if (currentStatus == "ToDo" && newStatus == "InProgress")
                return true;
            if (currentStatus == "InProgress" && newStatus == "Completed")
                return true;
            return false;
        }

        public static bool IsFinalStatus(string status)
        {
            return status == "Completed";
        }

        public static DateTime? SetCompletionDateIfFinished(string status)
        {
            return status == "Completed" ? DateTime.Now : (DateTime?)null;
        }
    }
}
