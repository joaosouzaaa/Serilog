using SerilogAPI.Entities;

namespace SerilogAPI.Interfaces.Repositories;

public interface IRentalDateRepository
{
    Task<bool> ExistsInTimeIntervalAsync(int houseId, DateTime startDate, DateTime endDate);
    Task AddAsync(RentalDate rentalDate);
    Task ExistsAsync(int id);
    Task DeleteAsync(int id);
    Task<List<RentalDate>> GetAllByHouseIdAsync(int houseId);
}
