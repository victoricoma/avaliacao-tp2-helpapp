using AutoMapper;
using AppHelper.Application.DTOs;
using AppHelper.Application.Products.Commands;

namespace AppHelper.Application.Mappings;

public class DTOToCommandMappingProfile : Profile
{
    public DTOToCommandMappingProfile()
    {
        CreateMap<ProductDTO, ProductCreateCommand>();
        CreateMap<ProductDTO, ProductUpdateCommand>();
    }
}
