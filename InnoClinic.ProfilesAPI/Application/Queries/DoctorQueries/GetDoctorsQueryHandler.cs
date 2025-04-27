using Application.DTO.Doctor;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Queries.DoctorQueries;

public record GetDoctorsQuery : IRequest<List<ResponseDoctorDto>>;

public class GetDoctorsQueryHandler(IDoctorRepository doctorRepository, IMapper mapper) : IRequestHandler<GetDoctorsQuery, List<ResponseDoctorDto>>
{
    public async Task<List<ResponseDoctorDto>> Handle(GetDoctorsQuery request, CancellationToken cancellationToken)
    {
        var doctors = await doctorRepository.GetDoctorsAsync(cancellationToken);

        return mapper.Map<List<ResponseDoctorDto>>(doctors);
    }
}