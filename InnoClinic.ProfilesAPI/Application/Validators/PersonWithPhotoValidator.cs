using Application.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.Validators;

public abstract class PersonWithPhotoValidator<T> : AbstractValidator<T> where T : class
{
    protected PersonWithPhotoValidator()
    {
        RuleFor(x => ValidationHelper.GetPropertyValue<Guid>(x, "AccountId"))
            .NotEmpty().WithMessage("Account id is required.");

        RuleFor(x => ValidationHelper.GetPropertyValue<string>(x, "FirstName"))
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

        RuleFor(x => ValidationHelper.GetPropertyValue<string>(x, "LastName"))
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

        RuleFor(x => ValidationHelper.GetPropertyValue<string?>(x, "MiddleName"))
            .MaximumLength(50).WithMessage("Middle name cannot exceed 50 characters.");

        RuleFor(x => ValidationHelper.GetPropertyValue<string>(x, "Email"))
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => ValidationHelper.GetPropertyValue<IFormFile?>(x, "Photo"))
            .Must(ValidationHelper.BeAValidImage).When(x => ValidationHelper.GetPropertyValue<IFormFile?>(x, "Photo") is not null)
            .WithMessage("Invalid image format.");
    }
}