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
        var doctor = await doctorRepository.GetDoctorByIdAsync(request.Id, cancellationToken);

        if (doctor is null) throw new DoctorNotFoundException();

        if (request.Dto.Photo is not null)
        {
            var photoUrl = await blobStorageService.UploadPhotoAsync(request.Dto.Photo);
            doctor.PhotoUrl = photoUrl;  
        }

        var updatedDoctor = await doctorRepository.UpdateAsync(doctor, cancellationToken);

        return mapper.Map<ResponseDoctorDto>(updatedDoctor);
    }
}