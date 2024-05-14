namespace SerilogAPI.Entities;

public sealed class Person
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required int Age { get; set; }

    public List<House> Houses { get; set; }
}
