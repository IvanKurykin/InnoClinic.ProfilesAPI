namespace Domain.Entities;

public sealed class Doctor
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;        
    public string? MiddleName { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public string Office { get; set; } = string.Empty;
    public string CareerStartYear { get; set; } = string.Empty;
    public string Status { get; set; } = "At work";
    public string? PhotoUrl { get; set; }
}