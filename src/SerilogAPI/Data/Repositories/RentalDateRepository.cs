using Microsoft.EntityFrameworkCore;
using SerilogAPI.Data.DatabaseContexts;
using SerilogAPI.Data.Repositories.BaseRepositories;
using SerilogAPI.Entities;
using SerilogAPI.Interfaces.Repositories;

namespace SerilogAPI.Data.Repositories;

public sealed class RentalDateRepository : BaseRepository<RentalDate>, IRentalDateRepository
{
    public RentalDateRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public Task<bool> ExistsInTimeIntervalAsync(int houseId, DateTime startDate, DateTime endDate) =>
        DbContextSet.AsNoTracking()
                    .AnyAsync(
            r => r.HouseId == houseId &&
            (startDate >= r.StartTime && startDate <= r.EndTime) ||
            (endDate >= r.StartTime && endDate <= r.EndTime));

    public Task AddAsync(RentalDate rentalDate)
    {
        DbContextSet.Add(rentalDate);

        return _dbContext.SaveChangesAsync();
    }

    public Task ExistsAsync(int id) =>
        DbContextSet.AsNoTracking()
                    .AnyAsync(r => r.Id == id);

    public async Task DeleteAsync(int id)
    {
        var rentalDate = await DbContextSet.FirstOrDefaultAsync(r => r.Id == id);

        DbContextSet.Remove(rentalDate!);

        await _dbContext.SaveChangesAsync();
    }

    public Task<List<RentalDate>> GetAllByHouseIdAsync(int houseId) =>
        DbContextSet.AsNoTracking()
                    .Where(r => r.HouseId == houseId)
                    .ToListAsync();
}
