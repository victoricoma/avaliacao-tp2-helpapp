using AutoMapper;
using AppHelper.Application.DTOs;
using AppHelper.Domain.Entities;

namespace AppHelper.Application.Mappings;

public class DomainToDTOMappingProfile : Profile
{
    public DomainToDTOMappingProfile()
    {
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<Product, ProductDTO>().ReverseMap();
    }
}
