using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagmentLibrary.Models.Enums;

namespace TaskManagmentLibrary.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public User? AssignedUser { get; set; }
        public IssuePriority Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public IssueStatus Status { get; set; } = IssueStatus.ToDo;

      
    }

}
