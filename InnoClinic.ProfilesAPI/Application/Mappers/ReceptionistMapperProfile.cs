using Application.DTO.Receptionist;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers;

public class ReceptionistMapperProfile : Profile
{
    ReceptionistMapperProfile()
    {
        CreateMap<RequestReceptionistDto, Receptionist>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.PhotoUrl, opt => opt.Ignore());

        CreateMap<Receptionist, ResponseReceptionistDto>();
    }
}