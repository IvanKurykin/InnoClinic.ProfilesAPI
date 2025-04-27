using Application.DTO.Patient;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.PatientCommands;

public record UpdatePatientCommand(RequestPatientDto Dto) : IRequest<ResponsePatientDto>;

public class UpdatePatientCommandHandler(IPatientRepository patientRepository, IMapper mapper, IBlobStorageService blobStorageService) : IRequestHandler<UpdatePatientCommand, ResponsePatientDto>
{
    public async Task<ResponsePatientDto> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        if (request.Dto is null) throw new DtoIsNullException();

        var patient = mapper.Map<Patient>(request.Dto);

        if (request.Dto.Photo is not null)
        {
            var photoUrl = await blobStorageService.UploadPhotoAsync(request.Dto.Photo);
            patient.PhotoUrl = photoUrl; 
        }

        var updatedPatient = await patientRepository.UpdateAsync(patient, cancellationToken);

        return mapper.Map<ResponsePatientDto>(updatedPatient);
    }
}