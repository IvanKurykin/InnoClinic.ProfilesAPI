using Application.Exceptions.NotFoundExceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.ReceptionistCommands;

public record DeleteReceptionistCommand(Guid Id) : IRequest;

public class DeleteReceptionistCommandHandler(IReceptionistRepository receptionistRepository) : IRequestHandler<DeleteReceptionistCommand>
{
    public async Task Handle(DeleteReceptionistCommand request, CancellationToken cancellationToken)
    {
        var receptionist = await receptionistRepository.GetReceptionistByIdAsync(request.Id, cancellationToken);

        if (receptionist is null) throw new ReceptionistNotFoundException();

        await receptionistRepository.DeleteReceptionistAsync(receptionist, cancellationToken);
    }
}