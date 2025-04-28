using Application.DTO.Doctor;
using Application.Exceptions.NotFoundExceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Constants;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.DoctorCommands;

public record UpdateDoctorCommand(Guid Id, RequestDoctorDto Dto) : IRequest<ResponseDoctorDto>;

public class UpdateDoctorCommandHandler(IDoctorRepository doctorRepository, IMapper mapper, IBlobStorageService blobStorageService) : IRequestHandler<UpdateDoctorCommand, ResponseDoctorDto>
{
    public async Task<ResponseDoctorDto> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
    {
        var existingDoctor = await doctorRepository.GetByIdAsync(request.Id, TrackChanges.Track, cancellationToken);

        if (existingDoctor is null) throw new DoctorNotFoundException();

        if (request.Dto.Photo is not null)
        {
            var photoUrl = await blobStorageService.UploadPhotoAsync(request.Dto.Photo);

            if (!string.IsNullOrEmpty(existingDoctor.PhotoUrl))
            {
                await blobStorageService.DeletePhotoAsync(existingDoctor.PhotoUrl);
            }

            existingDoctor.PhotoUrl = photoUrl;  
        }

        mapper.Map(request.Dto, existingDoctor);

        var updatedDoctor = await doctorRepository.UpdateAsync(existingDoctor, cancellationToken);

        return mapper.Map<ResponseDoctorDto>(updatedDoctor);
    }
}