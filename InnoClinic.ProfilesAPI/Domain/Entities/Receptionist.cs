namespace Domain.Entities;

public sealed class Receptionist : Person
{
    public string Email { get; set; } = string.Empty;
    public string Office { get; set; } = string.Empty;
}