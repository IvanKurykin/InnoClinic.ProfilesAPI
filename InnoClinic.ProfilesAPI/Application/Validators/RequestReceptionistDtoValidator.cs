using Application.DTO.Receptionist;
using FluentValidation;

namespace Application.Validators;

public class RequestReceptionistDtoValidator : PersonWithPhotoValidator<RequestReceptionistDto>
{
    public RequestReceptionistDtoValidator()
    {
        RuleFor(x => x.OfficeId).NotEmpty().WithMessage("Office id is required.");

        RuleFor(x => x.Office)
            .NotEmpty().WithMessage("Office is required.")
            .MaximumLength(100).WithMessage("Office cannot exceed 100 characters.");
    }
}