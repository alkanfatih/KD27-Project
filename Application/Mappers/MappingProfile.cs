using Application.DTOs.AdressDTOs;
using Application.DTOs.CartItemDTOs;
using Application.DTOs.CategoryDTOs;
using Application.DTOs.CustomerDTOs;
using Application.DTOs.OrderDTOs;
using Application.DTOs.ProductDetailsDTOs;
using Application.DTOs.ProductDTOs;
using Application.DTOs.ProductImageDTOs;
using Application.DTOs.UserDTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Category ↔ DTO eşleşmeleri
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();

            CreateMap<Category, CategorySummaryDto>();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<CreateCategoryDto, Category>();

            // Product eşleşmeleri
            CreateMap<Product, ProductDto>();

            CreateMap<Product, ProductSummaryDto>();
            CreateMap<Product, UpdateProductDto>()
                .ForMember(dest => dest.CurrentImageUrl, opt => opt.MapFrom(src => src.ImageUrl));

            CreateMap<UpdateProductDto, Product>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore()); // özel olarak controller'da set edeceğiz

            CreateMap<CreateProductDto, Product>()
                 .ForMember(dest => dest.ImageUrl, opt => opt.Ignore()); // image daha sonra set edilir

            CreateMap<Product, RecentProductDto>();
            CreateMap<Product, ProductListDto>();

            CreateMap<Product, ProductDetailDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Detail != null ? src.Detail.Description : null))
                .ForMember(dest => dest.Features, opt => opt.MapFrom(src => src.Detail != null ? src.Detail.Features : null))
                .ForMember(dest => dest.Usage, opt => opt.MapFrom(src => src.Detail != null ? src.Detail.Usage : null))
                .ForMember(dest => dest.Warranty, opt => opt.MapFrom(src => src.Detail != null ? src.Detail.Warranty : null))
                .ForMember(dest => dest.GalleryImages, opt => opt.MapFrom(src => src.Images.Select(i => i.ImageUrl).ToList()));
            CreateMap<ProductDetail, UpdateProductDetailDto>().ReverseMap();



            // User
            CreateMap<Customer, UserDto>();
            CreateMap<UpdateUserDto, Customer>().ReverseMap();
            CreateMap<CreateUserDto, Customer>();

            // Address
            CreateMap<Address, AddressDto>();
            CreateMap<CreateAddressDto, Address>();
            CreateMap<UpdateAddressDto, Address>().ReverseMap();

            // ProductImage
            CreateMap<ProductImage, ProductImageDto>();
            CreateMap<CreateProductImageDto, ProductImage>();
            CreateMap<UpdateProductImageDto, ProductImage>().ReverseMap();

            // Order
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.AddressSummary, opt => opt.MapFrom(src => src.Address.FullAddress))
                .ForMember(dest => dest.CustomerFullName, opt => opt.MapFrom(src => src.Customer.FullName));
            CreateMap<OrderItem, OrderItemDto>();

            //Agency
            CreateMap<AgencyApplication, AgencyApplicationDto>().ReverseMap();
            CreateMap<AgencyApplication, MyAgencyApplicationDto>();

            // CartItem
            CreateMap<CartItem, CartItemDto>();
        }
    }
}
