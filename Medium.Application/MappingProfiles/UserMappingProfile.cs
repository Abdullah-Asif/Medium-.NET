using AutoMapper;
using Medium.Application.DTOs;
using Medium.Domain.Entities;

namespace Medium.Application.MappingProfiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, SignUpDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
    }
}