using Application.Exceptions.NotFoundExceptions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.ReceptionistCommands;

public record DeleteReceptionistCommand(Guid Id) : IRequest;

public class DeleteReceptionistCommandHandler(IReceptionistRepository receptionistRepository, IBlobStorageService blobStorageService) : IRequestHandler<DeleteReceptionistCommand>
{
    public async Task Handle(DeleteReceptionistCommand request, CancellationToken cancellationToken)
    {
        var receptionist = await receptionistRepository.GetByIdAsync(request.Id, cancellationToken);

        if (receptionist is null) throw new ReceptionistNotFoundException();

        if (receptionist.PhotoUrl is not null)
        {
            await blobStorageService.DeletePhotoAsync(receptionist.PhotoUrl);
        }

        await receptionistRepository.DeleteAsync(receptionist, cancellationToken);
    }
}