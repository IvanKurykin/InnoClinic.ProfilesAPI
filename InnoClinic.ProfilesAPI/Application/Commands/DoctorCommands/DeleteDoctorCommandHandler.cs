using Application.Exceptions.NotFoundExceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.DoctorCommands;

public record DeleteDoctorCommand(Guid Id) : IRequest;

public class DeleteDoctorCommandHandler(IDoctorRepository doctorRepository) : IRequestHandler<DeleteDoctorCommand>
{
    public async Task Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
    {
        var doctor = await doctorRepository.GetDoctorByIdAsync(request.Id, cancellationToken);

        if (doctor is null) throw new DoctorNotFoundException();

        await doctorRepository.DeleteDoctorAsync(doctor, cancellationToken);
    }
}