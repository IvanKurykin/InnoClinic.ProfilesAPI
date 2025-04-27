using Microsoft.AspNetCore.Http;

namespace Application.DTO.Doctor;

public sealed class RequestDoctorDto : DoctorDto
{
    public IFormFile? Photo { get; set; }
}