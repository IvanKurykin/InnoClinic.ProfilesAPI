using Application.Helpers;
using Microsoft.AspNetCore.Http;
using Moq;

namespace UnitTests.HelpersTests;

public class ValidationHelperTests
{
    private readonly Mock<IFormFile> _fileMock = new();

    [Theory]
    [InlineData("test.jpg", true)]
    [InlineData("test.png", true)]
    [InlineData("test.bmp", false)]
    [InlineData("test.pdf", false)]
    public void BeAValidImageShouldValidateExtensions(string filename, bool expectedValid)
    {
        if (filename != null)
        {
            _fileMock.Setup(f => f.FileName).Returns(filename);
        }

        var result = ValidationHelper.BeAValidImage(filename != null ? _fileMock.Object : null);

        Assert.Equal(expectedValid, result);
    }

    [Theory]
    [InlineData("2020", true)]
    [InlineData("1901", true)]
    [InlineData("3000", false)]
    [InlineData("1800", false)]
    [InlineData("not-a-year", false)]
    [InlineData("", false)]
    public void BeAValidYearShouldValidateYears(string yearStr, bool expectedValid)
    {
        var result = ValidationHelper.BeAValidYear(yearStr);

        Assert.Equal(expectedValid, result);
    }

    [Fact]
    public void GetPropertyValueShouldReturnCorrectValue()
    {
        var testObj = new { Name = "Test", Age = 25 };

        Assert.Equal("Test", ValidationHelper.GetPropertyValue<string>(testObj, "Name"));
        Assert.Equal(25, ValidationHelper.GetPropertyValue<int>(testObj, "Age"));
    }

    [Fact]
    public void GetPropertyValue_InvalidProperty_ShouldThrow()
    {
        var testObj = new { Name = "Test" };

        Assert.Throws<ArgumentException>(() => ValidationHelper.GetPropertyValue<string>(testObj, "InvalidProperty"));
    }
}