namespace SerilogAPI.Entities;

public sealed class House
{
    public int Id { get; set; }
    public required string Address { get; set; }
    public required decimal Price { get; set; }
    public required int Number { get; set; }
    public required int NumberOfRooms { get; set; }

    public required int OwnerId { get; set; }
    public Person Owner { get; set; }
    public required List<RentalDate> RentalDates { get; set; }
}
