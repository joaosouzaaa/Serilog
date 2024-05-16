using SerilogAPI.DataTransferObjects.Person;

namespace SerilogAPI.Interfaces.Services;

public interface IPersonService
{
    Task AddAsync(PersonSave personSave);
    Task UpdateAsync(PersonUpdate personUpdate);
    Task DeleteAsync(int id);
    Task<PersonResponse?> GetByIdAsync(int id);
    Task<List<PersonResponse>> GetAllAsync();
}
