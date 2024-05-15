namespace SerilogAPI.DataTransferObjects.Person;

public sealed record PersonResponse(
    int Id,
    string Name,
    int Age);
