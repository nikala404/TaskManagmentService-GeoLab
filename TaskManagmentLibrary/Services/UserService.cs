
using TaskManagmentLibrary.Interfaces;
using TaskManagmentLibrary.Models;

namespace TaskManagmentLibrary.Services
{
   public class UserService : IUserService
{
    private readonly IRepository<User> _userRepository;

    public UserService(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public void RegisterUser(User user) => _userRepository.Add(user);

    public User? GetUserById(int id) => _userRepository.GetById(id);
}
}
