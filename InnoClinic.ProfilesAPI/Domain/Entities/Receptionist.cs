namespace Domain.Entities;

public sealed class Receptionist
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Office { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; }
}