using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagmentLibrary.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Delete(int id);
        IEnumerable<T> GetAll();
        T? GetById(int id);
        void Update(T entity);
    }
}
