using Application.DTO.Doctor;
using Application.Validators;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Http;
using Moq;

namespace UnitTests.ValidatorsTests;

public class RequestDoctorDtoValidatorTests
{
    private readonly RequestDoctorDtoValidator _validator = new();
    private readonly Mock<IFormFile> _fileMock = new();
    private const string _id = "000cfb18-ca8e-4fdf-8992-32061d9e6ce2";

    [Theory]
    [InlineData(_id, true)]
    public void AccountIdValidation(string accountId, bool expectedValid)
    {
        var dto = new RequestDoctorDto { AccountId = Guid.Parse(accountId) };
        var result = _validator.TestValidate(dto);

        if (expectedValid) result.ShouldNotHaveValidationErrorFor(x => x.AccountId);
        else result.ShouldHaveValidationErrorFor(x => x.AccountId);
    }

    [Theory]
    [InlineData(_id, true)]
    public void OfficeIdValidation(string officeId, bool expectedValid)
    {
        var dto = new RequestDoctorDto { OfficeId = Guid.Parse(officeId) };
        var result = _validator.TestValidate(dto);

        if (expectedValid) result.ShouldNotHaveValidationErrorFor(x => x.OfficeId);
        else result.ShouldHaveValidationErrorFor(x => x.OfficeId);
    }

    [Theory]
    [InlineData(_id, true)]
    public void SpecializationIdValidation(string specializationId, bool expectedValid)
    {
        var dto = new RequestDoctorDto { SpecializationId = Guid.Parse(specializationId) };
        var result = _validator.TestValidate(dto);

        if (expectedValid) result.ShouldNotHaveValidationErrorFor(x => x.SpecializationId);
        else result.ShouldHaveValidationErrorFor(x => x.SpecializationId);
    }

    [Theory]
    [InlineData(null, true)]
    [InlineData("image.jpg", true)]
    [InlineData("image.png", true)]
    public void PhotoValidation(string? filename, bool expectedValid)
    {
        var dto = new RequestDoctorDto();

        if (filename is not null)
        {
            _fileMock.Setup(f => f.FileName).Returns(filename);
            dto.Photo = _fileMock.Object;
        }

        var result = _validator.TestValidate(dto);

        if (expectedValid) result.ShouldNotHaveValidationErrorFor(x => x.Photo);
        else result.ShouldHaveValidationErrorFor(x => x.Photo).WithErrorMessage("Invalid image format.");
    }

    [Theory]
    [InlineData("John", true)]
    public void FirstNameValidation(string firstName, bool expectedValid)
    {
        var dto = new RequestDoctorDto { FirstName = firstName };
        var result = _validator.TestValidate(dto);

        if (expectedValid) result.ShouldNotHaveValidationErrorFor(x => x.FirstName);
        else result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [Theory]
    [InlineData("Doe", true)]
    public void LastNameValidation(string lastName, bool expectedValid)
    {
        var dto = new RequestDoctorDto { LastName = lastName };
        var result = _validator.TestValidate(dto);

        if (expectedValid) result.ShouldNotHaveValidationErrorFor(x => x.LastName);
        else result.ShouldHaveValidationErrorFor(x => x.LastName);
    }

    [Theory]
    [InlineData("", true)]
    [InlineData("Middle", true)]
    public void MiddleNameValidation(string middleName, bool expectedValid)
    {
        var dto = new RequestDoctorDto { MiddleName = middleName };
        var result = _validator.TestValidate(dto);

        if (expectedValid) result.ShouldNotHaveValidationErrorFor(x => x.MiddleName);
        else result.ShouldHaveValidationErrorFor(x => x.MiddleName);
    }

    [Theory]
    [InlineData("valid@email.com", true)]
    public void EmailValidation(string email, bool expectedValid)
    {
        var dto = new RequestDoctorDto { Email = email };
        var result = _validator.TestValidate(dto);

        if (expectedValid) result.ShouldNotHaveValidationErrorFor(x => x.Email);
        else result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Theory]
    [InlineData("2000-01-01", true)]
    public void DateOfBirthValidation(DateTime dateOfBirth, bool expectedValid)
    {
        var dto = new RequestDoctorDto { DateOfBirth = dateOfBirth };
        var result = _validator.TestValidate(dto);

        if (expectedValid) result.ShouldNotHaveValidationErrorFor(x => x.DateOfBirth);
        else result.ShouldHaveValidationErrorFor(x => x.DateOfBirth);
    }

    [Theory]
    [InlineData("", false)]
    [InlineData("Cardiology", true)]
    public void SpecializationValidation(string specialization, bool expectedValid)
    {
        var dto = new RequestDoctorDto { Specialization = specialization };
        var result = _validator.TestValidate(dto);

        if (expectedValid) result.ShouldNotHaveValidationErrorFor(x => x.Specialization);
        else result.ShouldHaveValidationErrorFor(x => x.Specialization);
    }

    [Theory]
    [InlineData("", false)]
    [InlineData("101", true)]
    public void OfficeValidation(string office, bool expectedValid)
    {
        var dto = new RequestDoctorDto { Office = office };
        var result = _validator.TestValidate(dto);

        if (expectedValid) result.ShouldNotHaveValidationErrorFor(x => x.Office);
        else result.ShouldHaveValidationErrorFor(x => x.Office);
    }

    [Theory]
    [InlineData("", false)]
    [InlineData("2020", true)]
    [InlineData("1800", false)]
    [InlineData("3000", false)]
    public void CareerStartYearValidation(string year, bool expectedValid)
    {
        var dto = new RequestDoctorDto { CareerStartYear = year };
        var result = _validator.TestValidate(dto);

        if (expectedValid) result.ShouldNotHaveValidationErrorFor(x => x.CareerStartYear);
        else result.ShouldHaveValidationErrorFor(x => x.CareerStartYear);
    }

    [Theory]
    [InlineData("", false)]
    [InlineData("Active", true)]
    public void StatusValidation(string status, bool expectedValid)
    {
        var dto = new RequestDoctorDto { Status = status };
        var result = _validator.TestValidate(dto);

        if (expectedValid) result.ShouldNotHaveValidationErrorFor(x => x.Status);
        else result.ShouldHaveValidationErrorFor(x => x.Status);
    }
}