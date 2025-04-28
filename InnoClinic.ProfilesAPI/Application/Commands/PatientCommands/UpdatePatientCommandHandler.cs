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
        var existedPatient = await patientRepository.GetByIdAsync(request.Id, cancellationToken);

        if (existedPatient is null) throw new PatientNotFoundException(); 

        if (request.Dto.Photo is not null)
        {
            var photoUrl = await blobStorageService.UploadPhotoAsync(request.Dto.Photo);

            if (!string.IsNullOrEmpty(existedPatient.PhotoUrl))
            {
                await blobStorageService.DeletePhotoAsync(existedPatient.PhotoUrl);
            }

            existedPatient.PhotoUrl = photoUrl; 
        }

        mapper.Map(request.Dto, existedPatient);

        var updatedPatient = await patientRepository.UpdateAsync(existedPatient, cancellationToken);

        return mapper.Map<ResponsePatientDto>(updatedPatient);
    }
}