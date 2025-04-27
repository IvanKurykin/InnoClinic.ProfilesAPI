using Application.DTO.Receptionist;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.ReceptionistCommands;

public record UpdateReceptionistCommand(RequestReceptionistDto Dto) : IRequest<ResponseReceptionistDto>;

public class UpdateReceptionistCommandHandler(IReceptionistRepository receptionistRepository, IMapper mapper, IBlobStorageService blobStorageService) : IRequestHandler<UpdateReceptionistCommand, ResponseReceptionistDto>
{
    public async Task<ResponseReceptionistDto> Handle(UpdateReceptionistCommand request, CancellationToken cancellationToken)
    {
        if (request.Dto is null) throw new DtoIsNullException();

        var receptionist = mapper.Map<Receptionist>(request.Dto);

        if (request.Dto.Photo is not null)
        {
            var photoUrl = await blobStorageService.UploadPhotoAsync(request.Dto.Photo);
            receptionist.PhotoUrl = photoUrl;  
        }

        var updatedReceptionist = await receptionistRepository.UpdateReceptionistAsync(receptionist, cancellationToken);

        return mapper.Map<ResponseReceptionistDto>(updatedReceptionist);
    }
}