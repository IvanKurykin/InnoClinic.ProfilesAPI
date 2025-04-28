using Application.DTO.Doctor;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.DoctorCommands;

public record CreateDoctorCommand(RequestDoctorDto Dto) : IRequest<ResponseDoctorDto>;

public class CreateDoctorCommandHandler(IDoctorRepository doctorRepository, IMapper mapper, IBlobStorageService blobStorageService) : IRequestHandler<CreateDoctorCommand, ResponseDoctorDto>
{
    public async Task<ResponseDoctorDto> Handle(CreateDoctorCommand request, CancellationToken cancellationToken = default)
    {
        if (request.Dto is null) throw new DtoIsNullException(); 

        var doctor = mapper.Map<Doctor>(request.Dto);

        if (request.Dto.Photo is not null)
        {
            var photoUrl = await blobStorageService.UploadPhotoAsync(request.Dto.Photo);
            doctor.PhotoUrl = photoUrl;
        }

        var createdDoctor = await doctorRepository.CreateAsync(doctor, cancellationToken);

        return mapper.Map<ResponseDoctorDto>(createdDoctor);
    }
}