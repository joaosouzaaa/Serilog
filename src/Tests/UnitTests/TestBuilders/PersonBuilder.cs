using SerilogAPI.DataTransferObjects.Person;
using SerilogAPI.Entities;

namespace UnitTests.TestBuilders;

public sealed class PersonBuilder
{
    private int _age = 123;
    private readonly int _id = 1;
    private string _name = "test";

    public static PersonBuilder NewObject() =>
        new();

    public Person DomainBuild() =>
        new()
        {
            Age = _age,
            Houses = [],
            Id = _id,
            Name = _name
        };

    public PersonSave SaveBuild() =>
        new(_name,
            _age);

    public PersonBuilder WithAge(int age)
    {
        _age = age;

        return this;
    }

    public PersonBuilder WithName(string name)
    {
        _name = name;

        return this;
    }
}
