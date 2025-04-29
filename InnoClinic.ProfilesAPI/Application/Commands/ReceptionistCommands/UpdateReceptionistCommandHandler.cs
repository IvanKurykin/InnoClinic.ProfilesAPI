using Application.DTO.Receptionist;
using Application.Exceptions.NotFoundExceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Constants;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.ReceptionistCommands;

public record UpdateReceptionistCommand(Guid Id, RequestReceptionistDto Dto) : IRequest<ResponseReceptionistDto>;

public class UpdateReceptionistCommandHandler(IReceptionistRepository receptionistRepository, IMapper mapper, IBlobStorageService blobStorageService) : IRequestHandler<UpdateReceptionistCommand, ResponseReceptionistDto>
{
    public async Task<ResponseReceptionistDto> Handle(UpdateReceptionistCommand request, CancellationToken cancellationToken)
    {
        var existingReceptionist = await receptionistRepository.GetByIdAsync(request.Id, TrackChanges.Track, cancellationToken);

        if (existingReceptionist is null) throw new ReceptionistNotFoundException();

        if (request.Dto.Photo is not null)
        {
            var photoUrl = await blobStorageService.UploadPhotoAsync(request.Dto.Photo);

            if (!string.IsNullOrEmpty(existingReceptionist.PhotoUrl))
            {
                await blobStorageService.DeletePhotoAsync(existingReceptionist.PhotoUrl);
            }

            existingReceptionist.PhotoUrl = photoUrl;  
        }

        mapper.Map(request.Dto, existingReceptionist);

        var updatedReceptionist = await receptionistRepository.UpdateAsync(existingReceptionist, cancellationToken);

        return mapper.Map<ResponseReceptionistDto>(updatedReceptionist);
    }
}