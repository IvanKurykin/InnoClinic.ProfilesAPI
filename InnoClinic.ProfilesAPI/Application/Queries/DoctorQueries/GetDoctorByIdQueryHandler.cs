using Application.DTO.Doctor;
using Application.Exceptions.NotFoundExceptions;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Queries.DoctorQueries;

public record GetDoctorByIdQuery(Guid Id) : IRequest<ResponseDoctorDto>;

public class GetDoctorByIdQueryHandler(IDoctorRepository doctorRepository, IMapper mapper) : IRequestHandler<GetDoctorByIdQuery, ResponseDoctorDto>
{
    public async Task<ResponseDoctorDto> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
    {
        var doctor = await doctorRepository.GetDoctorByIdAsync(request.Id, cancellationToken);

        if (doctor is null) throw new DoctorNotFoundException();

        return mapper.Map<ResponseDoctorDto>(doctor);
    }
}