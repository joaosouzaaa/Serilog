using SerilogAPI.Entities;

namespace SerilogAPI.Interfaces.Repositories;

public interface IHouseRepository
{
    Task AddAsync(House house);
    Task UpdateAsync(House house);
    Task<bool> ExistsAsync(int id);
    Task DeleteAsync(int id);
    Task<List<House>> GetByOwnerIdAsync(int ownerId);
}
