namespace SerilogAPI.DataTransferObjects.Person;

public sealed record PersonUpdate(
    int Id,
    string Name,
    int Age);
