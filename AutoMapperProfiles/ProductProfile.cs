using AutoMapper;
using MscApi.DataAccess.DTO;
using MscApi.Domain.Entities;

namespace MscApi.AutoMapperProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDTO>();
        CreateMap<Product, ProductCardDTO>();
    }
}
