using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagmentLibrary.Interfaces;
using TaskManagmentLibrary.Models.Enums;
using TaskManagmentLibrary.Models;

namespace TaskManagmentLibrary.Services
{
    public class IssueService : IIssueService
    {
        private readonly IRepository<Issue> _issueRepository;

        public IssueService(IRepository<Issue> issueRepository)
        {
            _issueRepository = issueRepository;
        }

        public void AddIssue(Issue issue)
        {
            issue.Status = IssueStatus.ToDo;
            _issueRepository.Add(issue);
        }

        public void UpdateIssue(Issue issue) => _issueRepository.Update(issue);

        public void ChangeIssueStatus(int id, IssueStatus newStatus)
        {
            var issue = _issueRepository.GetById(id);
            if (issue != null)
            {
                issue.Status = newStatus;
                _issueRepository.Update(issue);
            }
        }

        public IEnumerable<Issue> SearchByTitle(string keyword) =>
            _issueRepository.GetAll().Where(issue => issue.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase));

        public IEnumerable<Issue> FilterByStatus(IssueStatus status) =>
            _issueRepository.GetAll().Where(issue => issue.Status == status);

        public IEnumerable<Issue> SortByPriority() =>
            _issueRepository.GetAll().OrderBy(issue => issue.Priority);

        public IEnumerable<Issue> SortByDueDate() =>
            _issueRepository.GetAll().OrderBy(issue => issue.DueDate);

        public Issue GetIssueById(int issueId)
        {
            return _issueRepository.GetById(issueId);
        }

        public IEnumerable<Issue> FilterByAssignedUser(int userId)
        {
           return _issueRepository.GetAll().Where(user => user.AssignedUser.Id == userId);
        }
    }
}
