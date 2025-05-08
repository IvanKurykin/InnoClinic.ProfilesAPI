using Application.DTO.Patient;
using Application.Validators;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Http;
using Moq;

namespace UnitTests.ValidatorsTests;

public class RequestPatientDtoValidatorTests
{
    private readonly RequestPatientDtoValidator _validator = new();
    private readonly Mock<IFormFile> _fileMock = new();

    [Theory]
    [InlineData("000cfb18-ca8e-4fdf-8992-32061d9e6ce2", true)]
    public void AccountIdValidation(string accountId, bool expectedValid)
    {
        var dto = new RequestPatientDto { AccountId = Guid.Parse(accountId) };
        var result = _validator.TestValidate(dto);

        if (expectedValid) result.ShouldNotHaveValidationErrorFor(x => x.AccountId);
        else result.ShouldHaveValidationErrorFor(x => x.AccountId);
    }

    [Theory]
    [InlineData(null, true)]
    [InlineData("image.jpg", true)]
    public void PhotoValidation(string? filename, bool expectedValid)
    {
        var dto = new RequestPatientDto();

        if (filename is not null)
        {
            _fileMock.Setup(f => f.FileName).Returns(filename);
            dto.Photo = _fileMock.Object;
        }

        var result = _validator.TestValidate(dto);

        if (expectedValid) result.ShouldNotHaveValidationErrorFor(x => x.Photo);
        else result.ShouldHaveValidationErrorFor(x => x.Photo);
    }

    [Theory]
    [InlineData("", false)]
    [InlineData("+1234567890", true)]
    public void PhoneNumberValidation(string phone, bool expectedValid)
    {
        var dto = new RequestPatientDto { PhoneNumber = phone };
        var result = _validator.TestValidate(dto);

        if (expectedValid) result.ShouldNotHaveValidationErrorFor(x => x.PhoneNumber);
        else result.ShouldHaveValidationErrorFor(x => x.PhoneNumber);
    }

    [Theory]
    [InlineData("2000-01-01", true)]
    public void DateOfBirthValidation(DateTime dateOfBirth, bool expectedValid)
    {
        var dto = new RequestPatientDto { DateOfBirth = dateOfBirth };
        var result = _validator.TestValidate(dto);

        if (expectedValid) result.ShouldNotHaveValidationErrorFor(x => x.DateOfBirth);
        else result.ShouldHaveValidationErrorFor(x => x.DateOfBirth);
    }
}