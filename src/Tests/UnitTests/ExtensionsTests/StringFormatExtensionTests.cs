using SerilogAPI.Extensions;

namespace UnitTests.ExtensionsTests;

public sealed class StringFormatExtensionTests
{
    [Fact]
    public void FormatTo_SuccessfulScenario_ReturnsFormattedString()
    {
        // A
        const string stringToFormat = "{0} meu nome é {1}";

        // A
        var formattedString = stringToFormat.FormatTo("oi", "joao");

        // A
        Assert.Equal("oi meu nome é joao", formattedString);
    }
}
