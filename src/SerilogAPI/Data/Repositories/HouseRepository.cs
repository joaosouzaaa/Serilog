using Microsoft.EntityFrameworkCore;
using SerilogAPI.Data.DatabaseContexts;
using SerilogAPI.Data.Repositories.BaseRepositories;
using SerilogAPI.Entities;
using SerilogAPI.Interfaces.Repositories;

namespace SerilogAPI.Data.Repositories;

public sealed class HouseRepository : BaseRepository<House>, IHouseRepository
{
    public HouseRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public Task AddAsync(House house)
    {
        DbContextSet.Add(house);

        return _dbContext.SaveChangesAsync();
    }

    public Task UpdateAsync(House house)
    {
        _dbContext.Entry(house).State = EntityState.Modified;

        return _dbContext.SaveChangesAsync();
    }

    public Task<bool> ExistsAsync(int id) =>
        DbContextSet.AsNoTracking()
                    .AnyAsync(h => h.Id == id);

    public async Task DeleteAsync(int id)
    {
        var house = await DbContextSet.FirstOrDefaultAsync(h => h.Id == id);

        DbContextSet.Remove(house!);

        await _dbContext.SaveChangesAsync();
    }

    public Task<List<House>> GetByOwnerIdAsync(int ownerId) =>
        DbContextSet.Where(h => h.OwnerId == ownerId)
                    .ToListAsync();
}
