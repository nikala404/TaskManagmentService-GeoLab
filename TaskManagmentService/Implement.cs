using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagmentLibrary.Models.Enums;
using TaskManagmentLibrary.Models;
using TaskManagmentLibrary.Services;
using TaskManagmentLibrary.Validations;

namespace TaskManagmentService
{
    public class Implement
    {
        private readonly IssueService _issueService;
        private readonly UserService _userService;

        public Implement(IssueService issueService, UserService userService)
        {
            _issueService = issueService;
            _userService = userService;
        }

        public void RegisterUser()
        {
            Console.Write("Enter name: ");
            var name = Console.ReadLine();
            if (!UserValidations.IsValidName(name))
            {
                Console.WriteLine("Name must be between 1 and 100 characters");
                return;
            }

            Console.Write("Enter email: ");
            var email = Console.ReadLine();
            if (!UserValidations.IsValidEmail(email))
            {
                Console.WriteLine("Invalid email format or length");
                return;
            }

            Console.Write("Enter password: ");
            var password = Console.ReadLine();
            if (!UserValidations.IsValidPassword(password))
            {
                Console.WriteLine("Password must be between 8 and 16 characters");
                return;
            }

            try
            {
                var user = new User { Name = name, Email = email, Password = password };
                _userService.RegisterUser(user);
                Console.WriteLine("UserID: " + user.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void AddIssue()
        {
            Console.Write("Enter title: ");
            var title = Console.ReadLine();
            if (!TaskValidations.IsValidTitle(title))
            {
                Console.WriteLine("Title must be between 1 and 100 characters");
                return;
            }

            Console.Write("Enter description (optional): ");
            var description = Console.ReadLine();
            if (!TaskValidations.IsValidDescription(description))
            {
                Console.WriteLine("Description length exceeds 4000 characters");
                return;
            }

            Console.Write("Enter priority (Low, Medium, High): ");
            var priorityInput = Console.ReadLine();
            if (!Enum.TryParse(priorityInput, out IssuePriority priority) || !TaskValidations.IsValidPriority(priorityInput))
            {
                Console.WriteLine("Priority Must be Low, Medium, or High");
                return;
            }

            Console.Write("Enter due date (yyyy-MM-dd) or leave blank: ");
            var dueDateInput = Console.ReadLine();
            DateTime? dueDate = null;
            if (!string.IsNullOrWhiteSpace(dueDateInput))
            {
                if (DateTime.TryParse(dueDateInput, out var parsedDate) && TaskValidations.IsValidCompletionDate(parsedDate))
                    dueDate = parsedDate;
                else
                {
                    Console.WriteLine("Invalid or past due date");
                    return;
                }
            }

            Console.Write("Assign to user (enter user ID or leave blank): ");
            var userIdInput = Console.ReadLine();
            User? assignedUser = null;
            if (!string.IsNullOrWhiteSpace(userIdInput) && int.TryParse(userIdInput, out var userId))
            {
                assignedUser = _userService.GetUserById(userId);
                if (assignedUser == null)
                {
                    Console.WriteLine("User not found");
                    return;
                }
            }

            try
            {
                var issue = new Issue
                {
                    Title = title,
                    Description = description,
                    Priority = priority,
                    DueDate = dueDate,
                    AssignedUser = assignedUser
                };
                _issueService.AddIssue(issue);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void UpdateIssue()
        {
            Console.Write("Enter Issue ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out var issueId))
            {
                Console.WriteLine("Invalid ID format");
                return;
            }

            var issue = _issueService.GetIssueById(issueId);
            if (issue == null)
            {
                Console.WriteLine("Issue not found");
                return;
            }

            Console.Write("Enter new title: ");
            var newTitle = Console.ReadLine();
            if (!TaskValidations.IsValidTitle(newTitle))
            {
                Console.WriteLine("Title must be between 1 and 100 characters");
                return;
            }
            issue.Title = newTitle;

            Console.Write("Enter new description (optional): ");
            var newDescription = Console.ReadLine();
            if (!TaskValidations.IsValidDescription(newDescription))
            {
                Console.WriteLine("Description length exceeds 4000 characters");
                return;
            }
            issue.Description = newDescription;

            Console.Write("Enter new priority (Low, Medium, High): ");
            var priorityInput = Console.ReadLine();
            if (Enum.TryParse(priorityInput, out IssuePriority priority) && TaskValidations.IsValidPriority(priorityInput))
                issue.Priority = priority;
            else
            {
                Console.WriteLine("Invalid priority");
                return;
            }

            _issueService.UpdateIssue(issue);
            Console.WriteLine("Issue updated successfully");
        }

        public void ChangeIssueStatus()
        {
            Console.Write("Enter Issue ID: ");
            if (!int.TryParse(Console.ReadLine(), out var issueId))
            {
                Console.WriteLine("Invalid ID forma");
                return;
            }

            Console.Write("Enter new status (ToDo, InProgress, Completed): ");
            var statusInput = Console.ReadLine();
            if (!Enum.TryParse(statusInput, out IssueStatus newStatus))
            {
                Console.WriteLine("Invalid status");
                return;
            }

            try
            {
                var issue = _issueService.GetIssueById(issueId);
                if (issue == null)
                {
                    Console.WriteLine("Issue not found");
                    return;
                }

                if (!StatusValidations.CanChangeStatus(issue.Status.ToString(), newStatus.ToString()))
                {
                    Console.WriteLine("Status change not allowed");
                    return;
                }

                issue.Status = newStatus;
                if (StatusValidations.IsFinalStatus(newStatus.ToString()))
                {
                    issue.CompletionDate = StatusValidations.SetCompletionDateIfFinished(newStatus.ToString());
                }

                _issueService.UpdateIssue(issue);
                Console.WriteLine("Issue status updated successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void SearchIssues()
        {
            Console.Write("Enter titlle keyword: ");
            var keyword = Console.ReadLine();

            var results = _issueService.SearchByTitle(keyword);
            foreach (var issue in results)
            {
                Console.WriteLine($"ID: {issue.Id}, Title: {issue.Title}, Priority: {issue.Priority}, Status: {issue.Status}, DueDate: {issue.DueDate}, Completion Date: {issue.CompletionDate}");
            }
        }

        public void FilterIssues()
        {
            Console.WriteLine("Filter by:");
            Console.WriteLine("1. Status");
            Console.WriteLine("2. Assigned User");
            Console.Write("Choose an option: ");
            var filterOption = Console.ReadLine();

            switch (filterOption)
            {
                case "1":
                    Console.Write("Enter status (ToDo, InProgress, Completed): ");
                    var statusInput = Console.ReadLine();
                    if (Enum.TryParse(statusInput, out IssueStatus status))
                    {
                        var issues = _issueService.FilterByStatus(status);
                        foreach (var issue in issues)
                        {
                            Console.WriteLine($"ID: {issue.Id}, Title: {issue.Title}, Priority: {issue.Priority}, Status: {issue.Status}, DueDate: {issue.DueDate}, Completion Date: {issue.CompletionDate}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid status");
                    }
                    break;

                case "2":
                    Console.Write("Enter user ID: ");
                    if (int.TryParse(Console.ReadLine(), out var userId))
                    {
                        var issues = _issueService.FilterByAssignedUser(userId);
                        foreach (var issue in issues)
                        {
                            Console.WriteLine($"ID: {issue.Id}, Title: {issue.Title}, Priority: {issue.Priority}, Assigned User: {issue.AssignedUser?.Name}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid user ID");
                    }
                    break;

                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }

        public void SortIssues()
        {
            Console.WriteLine("Sort by:");
            Console.WriteLine("1. Priority");
            Console.WriteLine("2. Due Date");
            Console.Write("Choose an option: ");
            var sortOption = Console.ReadLine();

            IEnumerable<Issue> sortedIssues = sortOption switch
            {
                "1" => _issueService.SortByPriority(),
                "2" => _issueService.SortByDueDate(),
                _ => Enumerable.Empty<Issue>()
            };

            foreach (var issue in sortedIssues)
            {
                Console.WriteLine($"ID: {issue.Id}, Title: {issue.Title}, Priority: {issue.Priority}, Status: {issue.Status}, DueDate: {issue.DueDate}, Completion Date: {issue.CompletionDate}");
            }
        }
    }
}

