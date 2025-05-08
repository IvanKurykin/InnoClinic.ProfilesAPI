using Application.DTO.Doctor;
using Application.Helpers;
using FluentValidation;

namespace Application.Validators;

public class RequestDoctorDtoValidator : PersonWithPhotoValidator<RequestDoctorDto>
{
    public RequestDoctorDtoValidator()
    {
        RuleFor(x => x.AccountId).NotEmpty().WithMessage("Account id is required.");

        RuleFor(x => x.OfficeId).NotEmpty().WithMessage("Office id is required.");

        RuleFor(x => x.SpecializationId).NotEmpty().WithMessage("Specialization id is required.");

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateTime.Today).WithMessage("Date of birth must be in the past.");

        RuleFor(x => x.Specialization)
            .NotEmpty().WithMessage("Specialization is required.")
            .MaximumLength(100).WithMessage("Specialization cannot exceed 100 characters.");

        RuleFor(x => x.Office)
            .NotEmpty().WithMessage("Office is required.")
            .MaximumLength(100).WithMessage("Office cannot exceed 100 characters.");

        RuleFor(x => x.CareerStartYear)
            .NotEmpty().WithMessage("Career start year is required.")
            .Matches(@"^\d{4}$").WithMessage("Career start year must be a valid 4-digit year.")
            .Must(ValidationHelper.BeAValidYear).WithMessage("Career start year must be a valid past year.");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required.");

    }
}