namespace Domain.Entities;

public sealed class Receptionist : Person
{
    public Guid AccountId { get; set; }
    public Guid OfficeId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Office { get; set; } = string.Empty;
}