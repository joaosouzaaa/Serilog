using SerilogAPI.Entities;
using SerilogAPI.Mappers;
using UnitTests.TestBuilders;

namespace UnitTests.MappersTests;

public sealed class PersonMapperTests
{
    private readonly PersonMapper _personMapper;

    public PersonMapperTests()
    {
        _personMapper = new PersonMapper();
    }

    [Fact]
    public void SaveToDomain_SuccessfulScenario_ReturnsDomainObject()
    {
        // A
        var personSave = PersonBuilder.NewObject().SaveBuild();

        // A
        var personResult = _personMapper.SaveToDomain(personSave);

        // A
        Assert.Equal(personResult.Name, personSave.Name);
        Assert.Equal(personResult.Age, personSave.Age);
    }

    [Fact]
    public void UpdateToDomain_SuccessfulScenario_AssignPropertiesAccordingly()
    {
        // A
        var personUpdate = PersonBuilder.NewObject().UpdateBuild();
        var personResult = PersonBuilder.NewObject().DomainBuild();

        // A
        _personMapper.UpdateToDomain(personUpdate, personResult);

        // A
        Assert.Equal(personResult.Name, personUpdate.Name);
        Assert.Equal(personResult.Age, personUpdate.Age);
    }

    [Fact]
    public void DomainToResponse_SuccessfulScenario_ReturnsResponseObject()
    {
        // A
        var person = PersonBuilder.NewObject().DomainBuild();

        // A
        var personResponseResult = _personMapper.DomainToResponse(person);

        // A
        Assert.Equal(personResponseResult.Id, person.Id);
        Assert.Equal(personResponseResult.Name, person.Name);
        Assert.Equal(personResponseResult.Age, person.Age);
    }

    [Fact]
    public void DomainListToResponseList_SuccessfulScenario_ReturnsPersonList()
    {
        // A
        var personList = new List<Person>()
        {
            PersonBuilder.NewObject().DomainBuild(),
            PersonBuilder.NewObject().DomainBuild(),
            PersonBuilder.NewObject().DomainBuild()
        };

        // A
        var personResponseListResult = _personMapper.DomainListToResponseList(personList);

        // A
        Assert.Equal(personResponseListResult.Count, personList.Count);
    }
}
