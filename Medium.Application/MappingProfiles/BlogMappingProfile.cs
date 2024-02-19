using AutoMapper;
using Medium.Application.DTOs;
using Medium.Domain.Entities;

namespace Medium.Application.MappingProfiles;

public class BlogMappingProfile: Profile
{
    public BlogMappingProfile()
    {
        CreateMap<Blog, BlogDto>().ReverseMap();
    }
}