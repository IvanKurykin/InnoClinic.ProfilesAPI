using Domain.Constants;

namespace Domain.Entities;

public sealed class Doctor : Person
{
    public Guid AccountId { get; set; }
    public Guid OfficeId { get; set; }
    public Guid SpecializationId { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public string Office { get; set; } = string.Empty;
    public string CareerStartYear { get; set; } = string.Empty;
    public string Status { get; set; } = DoctorStatuses.AtWork;
}