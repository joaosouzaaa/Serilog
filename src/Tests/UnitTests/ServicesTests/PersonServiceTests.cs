using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;
using SerilogAPI.DataTransferObjects.Person;
using SerilogAPI.Entities;
using SerilogAPI.Interfaces.Mappers;
using SerilogAPI.Interfaces.Repositories;
using SerilogAPI.Interfaces.Settings;
using SerilogAPI.Services;
using UnitTests.TestBuilders;

namespace UnitTests.ServicesTests;

public sealed class PersonServiceTests
{
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly Mock<IPersonMapper> _personMapperMock;
    private readonly Mock<IValidator<Person>> _validatorMock;
    private readonly Mock<INotificationHandler> _notificationHandlerMock;
    private readonly Mock<ILogger<PersonService>> _loggerMock;
    private readonly PersonService _personService;

    public PersonServiceTests()
    {
        _personRepositoryMock = new Mock<IPersonRepository>();
        _personMapperMock = new Mock<IPersonMapper>();
        _validatorMock = new Mock<IValidator<Person>>();
        _notificationHandlerMock = new Mock<INotificationHandler>();
        _loggerMock = new Mock<ILogger<PersonService>>();
        _personService = new PersonService(_personRepositoryMock.Object, _personMapperMock.Object, _validatorMock.Object,
            _notificationHandlerMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task AddAsync_SuccessfulScenario_CallsAddAsync()
    {
        // A
        var personSave = PersonBuilder.NewObject().SaveBuild();

        var person = PersonBuilder.NewObject().DomainBuild();
        _personMapperMock.Setup(p => p.SaveToDomain(It.IsAny<PersonSave>()))
            .Returns(person);

        var validationResult = new ValidationResult();
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Person>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _personRepositoryMock.Setup(p => p.AddAsync(It.IsAny<Person>()));

        _loggerMock.Setup(logger => logger.Log(
            It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((@object, @type) => true),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()));

        // A
        await _personService.AddAsync(personSave);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _loggerMock.Verify(logger => logger.Log(
            It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((@object, @type) => true),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Never());
        _personRepositoryMock.Verify(p => p.AddAsync(It.IsAny<Person>()), Times.Once());
        _loggerMock.Verify(logger => logger.Log(
            It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((@object, @type) => true),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once());
    }

    [Fact]
    public async Task AddAsync_InvalidEntity_CallsAddNotification()
    {
        // A
        var personSave = PersonBuilder.NewObject().SaveBuild();

        var person = PersonBuilder.NewObject().DomainBuild();
        _personMapperMock.Setup(p => p.SaveToDomain(It.IsAny<PersonSave>()))
            .Returns(person);

        var validationFailureList = new List<ValidationFailure>()
        {
            new("test", "test"),
            new("test", "test"),
            new("test", "test")
        };
        var validationResult = new ValidationResult()
        {
            Errors = validationFailureList
        };
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Person>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _loggerMock.Setup(logger => logger.Log(
            It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((@object, @type) => true),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()));

        // A
        await _personService.AddAsync(personSave);

        // A
        var errorsCount = validationResult.Errors.Count;
        const int invalidPersonLogCalls = 1;

        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(validationResult.Errors.Count));
        _loggerMock.Verify(logger => logger.Log(
            It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((@object, @type) => true),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Exactly(invalidPersonLogCalls + errorsCount));
        _personRepositoryMock.Verify(p => p.AddAsync(It.IsAny<Person>()), Times.Never());
        _loggerMock.Verify(logger => logger.Log(
            It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((@object, @type) => true),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Never());
    }

    [Fact]
    public async Task UpdateAsync_SuccessfulScenario_CallsUpdateAsync()
    {
        // A
        var personUpdate = PersonBuilder.NewObject().UpdateBuild();

        var person = PersonBuilder.NewObject().DomainBuild();
        _personRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<int>(), false))
            .ReturnsAsync(person);

        _personMapperMock.Setup(p => p.UpdateToDomain(It.IsAny<PersonUpdate>(), It.IsAny<Person>()));

        var validationResult = new ValidationResult();
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Person>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _personRepositoryMock.Setup(p => p.UpdateAsync(It.IsAny<Person>()));

        _loggerMock.Setup(logger => logger.Log(
            It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((@object, @type) => true),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()));

        // A
        await _personService.UpdateAsync(personUpdate);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _loggerMock.Verify(logger => logger.Log(
            It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((@object, @type) => true),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Never());
        _personMapperMock.Verify(p => p.UpdateToDomain(It.IsAny<PersonUpdate>(), It.IsAny<Person>()), Times.Once());
        _validatorMock.Verify(v => v.ValidateAsync(It.IsAny<Person>(), It.IsAny<CancellationToken>()), Times.Once());
        _personRepositoryMock.Verify(p => p.UpdateAsync(It.IsAny<Person>()), Times.Once());
        _loggerMock.Verify(logger => logger.Log(
            It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((@object, @type) => true),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once());
    }

    [Fact]
    public async Task UpdateAsync_PersonDoesNotExist_CallsAddNotification()
    {
        // A
        var personUpdate = PersonBuilder.NewObject().UpdateBuild();

        _personRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<int>(), false))
            .Returns(Task.FromResult<Person?>(null));

        _loggerMock.Setup(logger => logger.Log(
            It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((@object, @type) => true),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()));

        // A
        await _personService.UpdateAsync(personUpdate);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        _loggerMock.Verify(logger => logger.Log(
            It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((@object, @type) => true),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once());
        _personMapperMock.Verify(p => p.UpdateToDomain(It.IsAny<PersonUpdate>(), It.IsAny<Person>()), Times.Never());
        _validatorMock.Verify(v => v.ValidateAsync(It.IsAny<Person>(), It.IsAny<CancellationToken>()), Times.Never());
        _personRepositoryMock.Verify(p => p.UpdateAsync(It.IsAny<Person>()), Times.Never());
        _loggerMock.Verify(logger => logger.Log(
            It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((@object, @type) => true),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Never());
    }

    [Fact]
    public async Task UpdateAsync_InvalidPerson_CallsAddNotification()
    {
        // A
        var personUpdate = PersonBuilder.NewObject().UpdateBuild();

        var person = PersonBuilder.NewObject().DomainBuild();
        _personRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<int>(), false))
            .ReturnsAsync(person);

        _personMapperMock.Setup(p => p.UpdateToDomain(It.IsAny<PersonUpdate>(), It.IsAny<Person>()));

        var validationFailureList = new List<ValidationFailure>()
        {
            new("test", "test"),
            new("test", "test"),
            new("test", "test")
        };
        var validationResult = new ValidationResult()
        {
            Errors = validationFailureList
        };
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Person>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _loggerMock.Setup(logger => logger.Log(
            It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((@object, @type) => true),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()));

        // A
        await _personService.UpdateAsync(personUpdate);

        // A
        var errorsCount = validationResult.Errors.Count;
        const int invalidPersonLogCalls = 1;

        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(errorsCount));
        _loggerMock.Verify(logger => logger.Log(
            It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((@object, @type) => true),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Exactly(invalidPersonLogCalls + errorsCount));
        _personMapperMock.Verify(p => p.UpdateToDomain(It.IsAny<PersonUpdate>(), It.IsAny<Person>()), Times.Once());
        _validatorMock.Verify(v => v.ValidateAsync(It.IsAny<Person>(), It.IsAny<CancellationToken>()), Times.Once());
        _personRepositoryMock.Verify(p => p.UpdateAsync(It.IsAny<Person>()), Times.Never());
        _loggerMock.Verify(logger => logger.Log(
            It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((@object, @type) => true),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Never());
    }

    [Fact]
    public async Task DeleteAsync_SuccessfulScenario_CallsDeleteAsync()
    {
        // A
        var id = 123;

        _personRepositoryMock.Setup(p => p.ExistsAsync(It.IsAny<int>()))
            .ReturnsAsync(true);

        _personRepositoryMock.Setup(p => p.DeleteAsync(It.IsAny<int>()));

        _loggerMock.Setup(logger => logger.Log(
            It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((@object, @type) => true),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()));

        // A
        await _personService.DeleteAsync(id);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _loggerMock.Verify(logger => logger.Log(
            It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((@object, @type) => true),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Never());
        _personRepositoryMock.Verify(p => p.DeleteAsync(It.IsAny<int>()), Times.Once());
        _loggerMock.Verify(logger => logger.Log(
            It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((@object, @type) => true),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once());
    }

    [Fact]
    public async Task DeleteAsync_PersonDoesNotExist_CallsAddNotification()
    {
        // A
        var id = 123;

        _personRepositoryMock.Setup(p => p.ExistsAsync(It.IsAny<int>()))
            .ReturnsAsync(false);

        _loggerMock.Setup(logger => logger.Log(
            It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((@object, @type) => true),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()));

        // A
        await _personService.DeleteAsync(id);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        _loggerMock.Verify(logger => logger.Log(
            It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((@object, @type) => true),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once());
        _personRepositoryMock.Verify(p => p.DeleteAsync(It.IsAny<int>()), Times.Never());
        _loggerMock.Verify(logger => logger.Log(
            It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((@object, @type) => true),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Never());
    }

    [Fact]
    public async Task GetByIdAsync_SuccessfulScenario_ReturnsResponseEntity()
    {
        // A
        var id = 123;

        var person = PersonBuilder.NewObject().DomainBuild();
        _personRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<int>(), It.IsAny<bool>()))
            .ReturnsAsync(person);

        _loggerMock.Setup(logger => logger.Log(
            It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((@object, @type) => true),
            It.IsAny<Exception>(),

            It.IsAny<Func<It.IsAnyType, Exception?, string>>()));
        var personResponse = PersonBuilder.NewObject().ResponseBuild();
        _personMapperMock.Setup(p => p.DomainToResponse(It.IsAny<Person>()))
            .Returns(personResponse);

        // A
        var personResponseResult = await _personService.GetByIdAsync(id);

        // A
        _loggerMock.Verify(logger => logger.Log(
            It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((@object, @type) => true),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Never());
        _loggerMock.Verify(logger => logger.Log(
            It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((@object, @type) => true),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once());
        _personMapperMock.Verify(p => p.DomainToResponse(It.IsAny<Person>()), Times.Once());

        Assert.NotNull(personResponseResult);
    }

    [Fact]
    public async Task GetByIdAsync_PersonNotFound_ReturnsNull()
    {
        // A
        var id = 123;

        _personRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<int>(), It.IsAny<bool>()))
            .Returns(Task.FromResult<Person?>(null));

        _loggerMock.Setup(logger => logger.Log(
            It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((@object, @type) => true),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()));

        var personResponse = PersonBuilder.NewObject().ResponseBuild();

        // A
        var personResponseResult = await _personService.GetByIdAsync(id);

        // A
        _loggerMock.Verify(logger => logger.Log(
            It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((@object, @type) => true),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once());
        _loggerMock.Verify(logger => logger.Log(
            It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((@object, @type) => true),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Never());
        _personMapperMock.Verify(p => p.DomainToResponse(It.IsAny<Person>()), Times.Never());

        Assert.Null(personResponseResult);
    }

    [Fact]
    public async Task GetAllAsync_SuccessfulScenario_ReturnsEntityList()
    {
        // A
        var personList = new List<Person>()
        {
            PersonBuilder.NewObject().DomainBuild(),
            PersonBuilder.NewObject().DomainBuild(),
            PersonBuilder.NewObject().DomainBuild()
        };
        _personRepositoryMock.Setup(p => p.GetAllAsync())
            .ReturnsAsync(personList);

        _loggerMock.Setup(logger => logger.Log(
            It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((@object, @type) => true),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()));

        var personResponseList = new List<PersonResponse>()
        {
            PersonBuilder.NewObject().ResponseBuild(),
            PersonBuilder.NewObject().ResponseBuild(),
            PersonBuilder.NewObject().ResponseBuild()
        };
        _personMapperMock.Setup(p => p.DomainListToResponseList(It.IsAny<List<Person>>()))
            .Returns(personResponseList);

        // A
        var personResponseListResult = await _personService.GetAllAsync();

        // A
        Assert.Equal(personResponseListResult.Count, personResponseList.Count);
    }
}
