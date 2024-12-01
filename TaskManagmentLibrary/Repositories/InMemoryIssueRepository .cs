using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagmentLibrary.Interfaces;
using TaskManagmentLibrary.Models;

namespace TaskManagmentLibrary.Repositories
{
    public class InMemoryIssueRepository : IRepository<Issue>
{
    private readonly List<Issue> _issues = new();
    private int _idCounter = 1;

    public void Add(Issue entity)
    {
        entity.Id = _idCounter++;
        _issues.Add(entity);
    }

    public void Delete(int id) => _issues.RemoveAll(x => x.Id == id);

    public IEnumerable<Issue> GetAll() => _issues;

    public Issue? GetById(int id) => _issues.FirstOrDefault(x => x.Id == id);

    public void Update(Issue entity)
    {
        var existing = GetById(entity.Id);
        if (existing != null)
        {
            _issues.Remove(existing);
            _issues.Add(entity);
        }
    }
}

}
