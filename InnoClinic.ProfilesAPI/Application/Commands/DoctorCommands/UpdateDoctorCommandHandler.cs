using Application.DTO.Doctor;
using Application.Exceptions.NotFoundExceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.DoctorCommands;

public record UpdateDoctorCommand(Guid Id, RequestDoctorDto Dto) : IRequest<ResponseDoctorDto>;

public class UpdateDoctorCommandHandler(IDoctorRepository doctorRepository, IMapper mapper, IBlobStorageService blobStorageService) : IRequestHandler<UpdateDoctorCommand, ResponseDoctorDto>
{
    public async Task<ResponseDoctorDto> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
    {
        var existedDoctor = await doctorRepository.GetByIdAsync(request.Id, cancellationToken);

        if (existedDoctor is null) throw new DoctorNotFoundException();

        if (request.Dto.Photo is not null)
        {
            var photoUrl = await blobStorageService.UploadPhotoAsync(request.Dto.Photo);

            if (!string.IsNullOrEmpty(existedDoctor.PhotoUrl))
            {
                await blobStorageService.DeletePhotoAsync(existedDoctor.PhotoUrl);
            }

            existedDoctor.PhotoUrl = photoUrl;  
        }

        mapper.Map(request.Dto, existedDoctor);

        var updatedDoctor = await doctorRepository.UpdateAsync(existedDoctor, cancellationToken);

        return mapper.Map<ResponseDoctorDto>(updatedDoctor);
    }
}