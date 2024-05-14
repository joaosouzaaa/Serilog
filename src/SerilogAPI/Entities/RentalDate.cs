namespace SerilogAPI.Entities;

public sealed class RentalDate
{
    public int Id { get; set; }
    public required DateTime StartTime { get; set; }
    public required DateTime EndTime { get; set; }

    public required int HouseId { get; set; }
    public required int RenterId { get; set; }
    public Person Renter { get; set; }
}
