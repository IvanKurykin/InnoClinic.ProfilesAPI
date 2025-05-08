namespace Application.DTO.Receptionist;

public class ReceptionistDto
{
    public Guid AccountId { get; set; }
    public Guid OfficeId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Office { get; set; } = string.Empty;
}