using Microsoft.AspNetCore.Http;

namespace Application.DTO.Receptionist;

public sealed class RequestReceptionistDto : ReceptionistDto
{
    public IFormFile? Photo { get; set; }
}