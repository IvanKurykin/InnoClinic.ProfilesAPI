using Domain.Constants;

namespace Application.DTO.Doctor;

public class DoctorDto
{
    public Guid AccountId { get; set; }
    public Guid OfficeId { get; set; }
    public Guid SpecializationId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public string Office { get; set; } = string.Empty;
    public string CareerStartYear { get; set; } = string.Empty;
    public string Status { get; set; } = DoctorStatuses.AtWork;
}