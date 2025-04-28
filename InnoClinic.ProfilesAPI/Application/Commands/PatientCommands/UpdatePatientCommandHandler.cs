using Application.DTO.Patient;
using Application.Exceptions.NotFoundExceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.PatientCommands;

public record UpdatePatientCommand(Guid Id, RequestPatientDto Dto) : IRequest<ResponsePatientDto>;

public class UpdatePatientCommandHandler(IPatientRepository patientRepository, IMapper mapper, IBlobStorageService blobStorageService) : IRequestHandler<UpdatePatientCommand, ResponsePatientDto>
{
    public async Task<ResponsePatientDto> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = await patientRepository.GetPatientByIdAsync(request.Id, cancellationToken);

        if (patient is null) throw new PatientNotFoundException(); 

        if (request.Dto.Photo is not null)
        {
            var photoUrl = await blobStorageService.UploadPhotoAsync(request.Dto.Photo);
            patient.PhotoUrl = photoUrl; 
        }

        var updatedPatient = await patientRepository.UpdatePatientAsync(patient, cancellationToken);

        return mapper.Map<ResponsePatientDto>(updatedPatient);
    }
}