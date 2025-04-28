using Application.DTO.Patient;
using FluentValidation;

namespace Application.Validators;

public class RequestPatientDtoValidator : PersonWithPhotoValidator<RequestPatientDto>
{
    public RequestPatientDtoValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?\d{10,15}$").WithMessage("Phone number must be valid and contain 10 to 15 digits.");

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateTime.Today).WithMessage("Date of birth must be in the past.");
    }
}
