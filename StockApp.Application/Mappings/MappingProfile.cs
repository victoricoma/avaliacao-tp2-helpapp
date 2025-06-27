using AutoMapper;
using StockApp.Application.DTOs;
using StockApp.Domain.Entities;

namespace StockApp.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Stock, StockDTO>().ReverseMap();
            CreateMap<UpdateStockDTO, Stock>()
                .ForMember(dest => dest.LastUpdate, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}