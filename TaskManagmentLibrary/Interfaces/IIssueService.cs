using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagmentLibrary.Models.Enums;
using TaskManagmentLibrary.Models;

namespace TaskManagmentLibrary.Interfaces
{
    public interface IIssueService
    {
        void AddIssue(Issue issue);
        void UpdateIssue(Issue issue);
        void ChangeIssueStatus(int id, IssueStatus newStatus);
        IEnumerable<Issue> SearchByTitle(string keyword);
        IEnumerable<Issue> FilterByStatus(IssueStatus status);
        IEnumerable<Issue> SortByPriority();
        IEnumerable<Issue> SortByDueDate();
    }
}
