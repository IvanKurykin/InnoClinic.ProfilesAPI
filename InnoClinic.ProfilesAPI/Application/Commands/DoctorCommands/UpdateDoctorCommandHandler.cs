using Application.DTO.Doctor;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.DoctorCommands;

public record UpdateDoctorCommand(RequestDoctorDto Dto) : IRequest<ResponseDoctorDto>;

public class UpdateDoctorCommandHandler(IDoctorRepository doctorRepository, IMapper mapper, IBlobStorageService blobStorageService) : IRequestHandler<UpdateDoctorCommand, ResponseDoctorDto>
{
    public async Task<ResponseDoctorDto> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
    {
        if (request.Dto is null) throw new DtoIsNullException();

        var doctor = mapper.Map<Doctor>(request.Dto);

        if (request.Dto.Photo is not null)
        {
            var photoUrl = await blobStorageService.UploadPhotoAsync(request.Dto.Photo);
            doctor.PhotoUrl = photoUrl;  
        }

        var updatedDoctor = await doctorRepository.UpdateDoctorAsync(doctor, cancellationToken);

        return mapper.Map<ResponseDoctorDto>(updatedDoctor);
    }
}