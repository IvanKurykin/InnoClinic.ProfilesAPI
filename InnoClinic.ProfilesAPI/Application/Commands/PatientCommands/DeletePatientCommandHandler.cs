using Application.Exceptions.NotFoundExceptions;
using Application.Interfaces;
using Domain.Constants;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.PatientCommands;

public record DeletePatientCommand(Guid Id) : IRequest;

public class DeletePatientCommandHandler(IPatientRepository patientRepository, IBlobStorageService blobStorageService) : IRequestHandler<DeletePatientCommand>
{
    public async Task Handle(DeletePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = await patientRepository.GetByIdAsync(request.Id, TrackChanges.Track, cancellationToken);

        if (patient is null) throw new PatientNotFoundException();

        if (patient.PhotoUrl is not null)
        {
            await blobStorageService.DeletePhotoAsync(patient.PhotoUrl);
        }

        await patientRepository.DeleteAsync(patient, cancellationToken);
    }
}