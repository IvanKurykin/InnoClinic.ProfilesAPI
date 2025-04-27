namespace Domain.Entities;

public sealed class Patient : Person
{
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public required DateTime DateOfBirth { get; set; }
}