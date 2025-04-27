using Application.DTO.Patient;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.PatientCommands;

public record CreatePatientCommand(RequestPatientDto Dto) : IRequest<ResponsePatientDto>;

public class CreatePatientCommandHandler(IPatientRepository patientRepository, IMapper mapper, IBlobStorageService blobStorageService) : IRequestHandler<CreatePatientCommand, ResponsePatientDto>
{
    public async Task<ResponsePatientDto> Handle(CreatePatientCommand request, CancellationToken cancellationToken = default)
    {
        if (request.Dto is null) throw new DtoIsNullException();

        var patient = mapper.Map<Patient>(request.Dto);

        if (request.Dto.Photo is not null)
        {
            var photoUrl = await blobStorageService.UploadPhotoAsync(request.Dto.Photo);
            patient.PhotoUrl = photoUrl;
        }

        var createdPatient = await patientRepository.CreateAsync(patient, cancellationToken);

        return mapper.Map<ResponsePatientDto>(createdPatient);
    }
}