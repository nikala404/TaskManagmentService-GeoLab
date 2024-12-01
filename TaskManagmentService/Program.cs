using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagmentLibrary.Repositories;
using TaskManagmentLibrary.Services;
using TaskManagmentService;

class Program
{
    static void Main(string[] args)
    {
        var issueRepository = new InMemoryIssueRepository();
        var userRepository = new InMemoryUserRepository();

        var issueService = new IssueService(issueRepository);
        var userService = new UserService(userRepository);

        var implement = new Implement(issueService, userService);

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome to Issue Management System");
            Console.WriteLine("1. Register User");
            Console.WriteLine("2. Add Issue");
            Console.WriteLine("3. Update Issue");
            Console.WriteLine("4. Change Issue Status");
            Console.WriteLine("5. Search Issues by Title");
            Console.WriteLine("6. Filter Issues");
            Console.WriteLine("7. Sort Issues");
            Console.WriteLine("8. Exit");
            Console.Write("Choose an option: ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    implement.RegisterUser();
                    break;
                case "2":
                    implement.AddIssue();
                    break;
                case "3":
                    implement.UpdateIssue();
                    break;
                case "4":
                    implement.ChangeIssueStatus();
                    break;
                case "5":
                    implement.SearchIssues();
                    break;
                case "6":
                    implement.FilterIssues();
                    break;
                case "7":
                    implement.SortIssues();
                    break;
                case "8":
                    return;
                default:
                    Console.WriteLine("Invalid option Try again");
                    break;
            }

            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
        }
    }
}