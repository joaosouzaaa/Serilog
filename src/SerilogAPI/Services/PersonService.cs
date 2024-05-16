using FluentValidation;
using SerilogAPI.DataTransferObjects.Person;
using SerilogAPI.Entities;
using SerilogAPI.Enums;
using SerilogAPI.Extensions;
using SerilogAPI.Interfaces.Mappers;
using SerilogAPI.Interfaces.Repositories;
using SerilogAPI.Interfaces.Services;
using SerilogAPI.Interfaces.Settings;

namespace SerilogAPI.Services;

public sealed class PersonService(
    IPersonRepository personRepository,
    IPersonMapper personMapper,
    IValidator<Person> validator,
    INotificationHandler notificationHandler,
    ILogger<PersonService> logger)
    : IPersonService
{
    public async Task AddAsync(PersonSave personSave)
    {
        var person = personMapper.SaveToDomain(personSave);

        if (!await IsValidAsync(person))
        {
            logger.LogError(ELogs.Invalid.Description().FormatTo("Person"));

            return;
        }

        await personRepository.AddAsync(person);

        logger.LogInformation("Person was inserted successfully");
    }

    public async Task UpdateAsync(PersonUpdate personUpdate)
    {
        var person = await personRepository.GetByIdAsync(personUpdate.Id, false);

        if (person is null)
        {
            notificationHandler.AddNotification(nameof(ELogs.NotFound), ELogs.NotFound.Description().FormatTo("Person"));

            logger.LogError(ELogs.NotFound.Description().FormatTo($"Person: {personUpdate.Id}"));

            return;
        }

        personMapper.UpdateToDomain(personUpdate, person);

        if (!await IsValidAsync(person))
        {
            logger.LogError(ELogs.Invalid.Description().FormatTo("Person"));

            return;
        }

        await personRepository.UpdateAsync(person);

        logger.LogInformation("Person was updated successfully: {@person.Id}", person.Id);
    }

    public async Task DeleteAsync(int id)
    {
        if (!await personRepository.ExistsAsync(id))
        {
            notificationHandler.AddNotification(nameof(ELogs.NotFound), ELogs.NotFound.Description().FormatTo("Person"));

            logger.LogError(ELogs.NotFound.Description().FormatTo($"Person: {id}"));

            return;
        }

        await personRepository.DeleteAsync(id);

        logger.LogInformation("Person was deleted: {@id}", id);
    }

    public async Task<PersonResponse?> GetByIdAsync(int id)
    {
        var person = await personRepository.GetByIdAsync(id, true);

        if ((person is null))
        {
            logger.LogError(ELogs.NotFound.Description().FormatTo($"Person: {id}"));

            return null;
        }

        logger.LogInformation("Person was found: {@id}", id);

        return personMapper.DomainToResponse(person);
    }

    public async Task<List<PersonResponse>> GetAllAsync()
    {
        var personList = await personRepository.GetAllAsync();

        logger.LogInformation("Person list found with {@personList.Count} records", personList.Count);

        return personMapper.DomainListToResponseList(personList);
    }

    private async Task<bool> IsValidAsync(Person person)
    {
        var validationResult = await validator.ValidateAsync(person);

        if (validationResult.IsValid)
        {
            return true;
        }

        foreach (var error in validationResult.Errors)
        {
            notificationHandler.AddNotification(error.PropertyName, error.ErrorMessage);

            logger.LogError(ELogs.Invalid.Description().FormatTo(error.PropertyName));
        }

        return false;
    }
}
