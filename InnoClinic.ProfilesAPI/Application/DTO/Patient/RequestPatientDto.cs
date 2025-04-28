using Microsoft.AspNetCore.Http;

namespace Application.DTO.Patient;

public sealed class RequestPatientDto : PatientDto
{
    public IFormFile? Photo { get; set; }   
}