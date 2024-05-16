using SerilogAPI.Validators;
using UnitTests.TestBuilders;

namespace UnitTests.ValidatorsTests;

public sealed class PersonValidatorTests
{
    private readonly PersonValidator _personValidator;

    public PersonValidatorTests()
    {
        _personValidator = new PersonValidator();
    }

    [Fact]
    public async Task ValidateAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var personToValidate = PersonBuilder.NewObject().DomainBuild();

        // A
        var validationResult = await _personValidator.ValidateAsync(personToValidate);

        // A
        Assert.True(validationResult.IsValid);
    }

    [Theory]
    [MemberData(nameof(InvalidAgeParameters))]
    public async Task ValidateAsync_InvalidAge_ReturnsFalse(int age)
    {
        // A
        var personWithInvalidAge = PersonBuilder.NewObject().WithAge(age).DomainBuild();

        // A
        var validationResult = await _personValidator.ValidateAsync(personWithInvalidAge);

        // A
        Assert.False(validationResult.IsValid);
    }

    public static TheoryData<int> InvalidAgeParameters() =>
        new()
        {
            0,
            -1
        };

    [Theory]
    [MemberData(nameof(InvalidNameParameters))]
    public async Task ValidateAsync_InvalidName_ReturnsFalse(string name)
    {
        // A
        var personWithInvalidName = PersonBuilder.NewObject().WithName(name).DomainBuild();

        // A
        var validationResult = await _personValidator.ValidateAsync(personWithInvalidName);

        // A
        Assert.False(validationResult.IsValid);
    }

    public static TheoryData<string> InvalidNameParameters() =>
        new()
        {
            "",
            "a",
            new string('a', 102)
        };
}
