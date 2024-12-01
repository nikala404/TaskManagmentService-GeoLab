using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagmentLibrary.Interfaces;
using TaskManagmentLibrary.Models;

namespace TaskManagmentLibrary.Repositories
{
    public class InMemoryUserRepository : IRepository<User>
    {
        private readonly List<User> _users = new();
        private int _idCounter = 1;

        public void Add(User entity)
        {
            entity.Id = _idCounter++;
            _users.Add(entity);
        }

        public void Delete(int id) => _users.RemoveAll(x => x.Id == id);

        public IEnumerable<User> GetAll() => _users;

        public User? GetById(int id) => _users.FirstOrDefault(x => x.Id == id);

        public void Update(User entity)
        {
            var existing = GetById(entity.Id);
            if (existing != null)
            {
                _users.Remove(existing);
                _users.Add(entity);
            }
        }
    }
}
