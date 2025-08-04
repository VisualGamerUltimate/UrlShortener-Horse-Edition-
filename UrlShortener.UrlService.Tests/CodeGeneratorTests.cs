using UrlShortener.Shared.Infrastructure.Services;
using Xunit;
using FluentAssertions;

public class CodeGeneratorTests
{
    [Fact]
    public void GenerateShortCode_Returns_CorrectLength()
    {
        var gen = new CodeGenerator();
        var code = gen.GenerateShortCode(8);
        code.Should().HaveLength(8);
    }
}