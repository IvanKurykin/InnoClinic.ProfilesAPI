using Application.Exceptions.NotFoundExceptions;
using Application.Interfaces;
using Domain.Constants;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.DoctorCommands;

public record DeleteDoctorCommand(Guid Id) : IRequest;

public class DeleteDoctorCommandHandler(IDoctorRepository doctorRepository, IBlobStorageService blobStorageService) : IRequestHandler<DeleteDoctorCommand>
{
    public async Task Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
    {
        var doctor = await doctorRepository.GetByIdAsync(request.Id, TrackChanges.Track, cancellationToken);

        if (doctor is null) throw new DoctorNotFoundException();

        if (doctor.PhotoUrl is not null)
        {
            await blobStorageService.DeletePhotoAsync(doctor.PhotoUrl);
        }

        await doctorRepository.DeleteAsync(doctor, cancellationToken);
    }
}