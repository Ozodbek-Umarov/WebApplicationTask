using AutoMapper;
using WebApplicationTask.Application.DTOs.CategoryDtos;
using WebApplicationTask.Application.DTOs.OrderDtos;
using WebApplicationTask.Application.DTOs.ProductDtos;
using WebApplicationTask.Data.Entities;

namespace WebApplicationTask.Application.Common.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ImagePath));

        CreateMap<AddProductDto, Product>();

        CreateMap<Category, CategoryDto>();
        CreateMap<AddCategoryDto, Category>();

        CreateMap<Order, OrderDto>()
        .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
        CreateMap<CreateOrderDto, Order>();
    }
}