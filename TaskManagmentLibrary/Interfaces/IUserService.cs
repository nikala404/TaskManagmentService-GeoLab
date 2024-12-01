using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagmentLibrary.Models;

namespace TaskManagmentLibrary.Interfaces
{
    public interface IUserService
    {
        void RegisterUser(User user);
        User? GetUserById(int id);
    }
}
