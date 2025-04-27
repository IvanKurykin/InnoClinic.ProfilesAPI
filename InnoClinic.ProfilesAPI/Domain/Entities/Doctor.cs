using Domain.Constants;

namespace Domain.Entities;

public sealed class Doctor : Person
{
    public required DateTime DateOfBirth { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public string Office { get; set; } = string.Empty;
    public string CareerStartYear { get; set; } = string.Empty;
    public string Status { get; set; } = DoctorStatuses.AtWork;
}