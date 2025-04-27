namespace Domain.Entities;

public sealed class Patient
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public required DateTime DateOfBirth { get; set; }
    public string? PhotoUrl { get; set; } 
}