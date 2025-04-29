using Application.DTO.Patient;
using Application.Exceptions.NotFoundExceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Constants;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.PatientCommands;

public record UpdatePatientCommand(Guid Id, RequestPatientDto Dto) : IRequest<ResponsePatientDto>;

public class UpdatePatientCommandHandler(IPatientRepository patientRepository, IMapper mapper, IBlobStorageService blobStorageService) : IRequestHandler<UpdatePatientCommand, ResponsePatientDto>
{
    public async Task<ResponsePatientDto> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        var existingPatient = await patientRepository.GetByIdAsync(request.Id, cancellationToken);

        if (existingPatient is null) throw new PatientNotFoundException(); 

        if (request.Dto.Photo is not null)
        {
            var photoUrl = await blobStorageService.UploadPhotoAsync(request.Dto.Photo);

            if (!string.IsNullOrEmpty(existingPatient.PhotoUrl))
            {
                await blobStorageService.DeletePhotoAsync(existingPatient.PhotoUrl);
            }

            existingPatient.PhotoUrl = photoUrl; 
        }

        mapper.Map(request.Dto, existingPatient);

        var updatedPatient = await patientRepository.UpdateAsync(existingPatient, cancellationToken);

        return mapper.Map<ResponsePatientDto>(updatedPatient);
    }
}