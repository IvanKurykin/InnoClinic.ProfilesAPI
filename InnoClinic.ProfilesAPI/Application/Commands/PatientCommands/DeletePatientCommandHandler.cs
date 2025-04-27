using Application.Exceptions.NotFoundExceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.PatientCommands;

public record DeletePatientCommand(Guid Id) : IRequest;

public class DeletePatientCommandHandler(IPatientRepository patientRepository) : IRequestHandler<DeletePatientCommand>
{
    public async Task Handle(DeletePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = await patientRepository.GetPatientByIdAsync(request.Id, cancellationToken);

        if (patient is null) throw new PatientNotFoundException();

        await patientRepository.DeletePatientAsync(patient, cancellationToken);
    }
}