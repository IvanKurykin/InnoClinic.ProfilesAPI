using Application.DTO.Receptionist;
using Application.Exceptions.NotFoundExceptions;
using AutoMapper;
using Domain.Constants;
using Domain.Interfaces;
using MediatR;

namespace Application.Queries.ReceptionistQueries;

public record GetReceptionistByIdQuery(Guid Id) : IRequest<ResponseReceptionistDto>;

public class GetReceptionistByIdQueryHandler(IReceptionistRepository receptionistRepository, IMapper mapper) : IRequestHandler<GetReceptionistByIdQuery, ResponseReceptionistDto>
{
    public async Task<ResponseReceptionistDto> Handle(GetReceptionistByIdQuery request, CancellationToken cancellationToken)
    {
        var receptionist = await receptionistRepository.GetByIdAsync(request.Id, TrackChanges.UnTrace, cancellationToken);

        if (receptionist is null) throw new ReceptionistNotFoundException();

        return mapper.Map<ResponseReceptionistDto>(receptionist);
    }
}