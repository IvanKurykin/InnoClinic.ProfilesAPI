using System.Diagnostics.CodeAnalysis;
using Application.DTO.Receptionist;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers;

[ExcludeFromCodeCoverage]
public class ReceptionistMapperProfile : Profile
{
    public ReceptionistMapperProfile()
    {
        CreateMap<RequestReceptionistDto, Receptionist>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PhotoUrl, opt => opt.Ignore());

        CreateMap<Receptionist, ResponseReceptionistDto>();
    }
}