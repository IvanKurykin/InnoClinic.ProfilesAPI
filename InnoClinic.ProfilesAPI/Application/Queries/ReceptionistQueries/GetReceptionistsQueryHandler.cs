using Application.DTO.Receptionist;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Queries.ReceptionistQueries;

public record GetReceptionistsQuery : IRequest<List<ResponseReceptionistDto>>;

public class GetReceptionistsQueryHandler(IReceptionistRepository receptionistRepository, IMapper mapper) : IRequestHandler<GetReceptionistsQuery, List<ResponseReceptionistDto>>
{
    public async Task<List<ResponseReceptionistDto>> Handle(GetReceptionistsQuery request, CancellationToken cancellationToken)
    {
        var receptionists = await receptionistRepository.GetAllAsync(cancellationToken);

        return mapper.Map<List<ResponseReceptionistDto>>(receptionists);
    }
}