using SerilogAPI.DataTransferObjects.Person;
using SerilogAPI.Entities;
using SerilogAPI.Interfaces.Mappers;

namespace SerilogAPI.Mappers;

public sealed class PersonMapper : IPersonMapper
{
    public Person SaveToDomain(PersonSave personSave) =>
        new()
        {
            Name = personSave.Name,
            Age = personSave.Age
        };

    public void UpdateToDomain(PersonUpdate personUpdate, Person person)
    {
        person.Name = personUpdate.Name;
        person.Age = personUpdate.Age;
    }

    public PersonResponse DomainToResponse(Person person) =>
        new(person.Id,
            person.Name,
            person.Age);

    public List<PersonResponse> DomainListToResponseList(List<Person> personList) =>
        personList.Select(DomainToResponse).ToList();
}
