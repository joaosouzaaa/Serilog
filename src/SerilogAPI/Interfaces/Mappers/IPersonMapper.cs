using SerilogAPI.DataTransferObjects.Person;
using SerilogAPI.Entities;

namespace SerilogAPI.Interfaces.Mappers;

public interface IPersonMapper
{
    Person SaveToDomain(PersonSave personSave);
    void UpdateToDomain(PersonUpdate personUpdate, Person person);
    PersonResponse DomainToResponse(Person person);
    List<PersonResponse> DomainListToResponseList(List<Person> personList);
}
