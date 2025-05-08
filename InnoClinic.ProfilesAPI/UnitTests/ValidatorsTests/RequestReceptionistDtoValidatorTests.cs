using Application.DTO.Receptionist;
using Application.Validators;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Http;
using Moq;

namespace UnitTests.ValidatorsTests;

public class RequestReceptionistDtoValidatorTests
{
    private readonly RequestReceptionistDtoValidator _validator = new();
    private readonly Mock<IFormFile> _fileMock = new();
    private const string _id = "000cfb18-ca8e-4fdf-8992-32061d9e6ce2";

    [Theory]
    [InlineData(_id, true)]
    public void AccountIdValidation(string accountId, bool expectedValid)
    {
        var dto = new RequestReceptionistDto { AccountId = Guid.Parse(accountId) };
        var result = _validator.TestValidate(dto);

        if (expectedValid) result.ShouldNotHaveValidationErrorFor(x => x.AccountId);
        else result.ShouldHaveValidationErrorFor(x => x.AccountId);
    }

    [Theory]
    [InlineData(_id, true)]
    public void OfficeIdValidation(string officeId, bool expectedValid)
    {
        var dto = new RequestReceptionistDto { OfficeId = Guid.Parse(officeId) };
        var result = _validator.TestValidate(dto);

        if (expectedValid) result.ShouldNotHaveValidationErrorFor(x => x.OfficeId);
        else result.ShouldHaveValidationErrorFor(x => x.OfficeId);
    }

    [Theory]
    [InlineData(null, true)]
    [InlineData("image.jpg", true)]
    public void PhotoValidation(string? filename, bool expectedValid)
    {
        var dto = new RequestReceptionistDto();

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
    [InlineData("Reception A", true)]
    public void OfficeValidation(string office, bool expectedValid)
    {
        var dto = new RequestReceptionistDto { Office = office };
        var result = _validator.TestValidate(dto);

        if (expectedValid) result.ShouldNotHaveValidationErrorFor(x => x.Office);
        else result.ShouldHaveValidationErrorFor(x => x.Office);
    }
}