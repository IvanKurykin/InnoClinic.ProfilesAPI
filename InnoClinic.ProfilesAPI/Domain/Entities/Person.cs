namespace Domain.Entities;

public abstract class Person
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string? PhotoUrl { get; set; }
}