using UrlShortener.Shared.Infrastructure.Services;
using Xunit;

public class UrlValidatorTests
{
    [Theory]
    [InlineData("https://google.com", true)]
    [InlineData("http://localhost", true)]
    [InlineData("invalidurl", false)]
    public void IsValidUrl_Works_Correctly(string url, bool expected)
    {
        var validator = new UrlValidator();
        Assert.Equal(expected, validator.IsValidUrl(url));
    }
}