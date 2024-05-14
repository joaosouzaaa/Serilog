using SerilogAPI.Entities;

namespace SerilogAPI.Interfaces.Repositories;

public interface IPersonRepository
{
    Task AddAsync(Person person);
    Task UpdateAsync(Person person);
    Task ExistsAsync(int id);
    Task DeleteAsync(int id);
    Task<Person?> GetByIdAsync(int id, bool asNoTracking);
    Task<List<Person>> GetAllAsync();
}
