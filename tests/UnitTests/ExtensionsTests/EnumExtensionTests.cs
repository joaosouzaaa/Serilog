using SerilogAPI.Enums;
using SerilogAPI.Extensions;

namespace UnitTests.ExtensionsTests;

public sealed class EnumExtensionTests
{
    [Fact]
    public void Description_SuccessfulScenario_ReturnsEnumDescription()
    {
        // A
        var logs = ELogs.Invalid;

        const string expectedDescription = "{0} is Invalid.";

        // A
        var descriptionResult = logs.Description();

        // A
        Assert.Equal(expectedDescription, descriptionResult);
    }
}
