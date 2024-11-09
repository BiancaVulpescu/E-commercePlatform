using Application.DTOs;
using Application.Use_Cases.Commands;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Application.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<CreateProductCommand, Product>().ReverseMap();
            CreateMap<UpdateProductCommand, Product>().ReverseMap();
            CreateMap<WishlistItem, WishlistItemDto>().ReverseMap();
            CreateMap<CreateWishlistItemCommand, WishlistItem>().ReverseMap();
            CreateMap<UpdateWishlistItemCommand, WishlistItem>().ReverseMap();
            CreateMap<ShoppingCartItem, ShoppingCartItemDto>().ReverseMap();
            CreateMap<CreateShoppingCartItemCommand, ShoppingCartItem>().ReverseMap();
            CreateMap<UpdateShoppingCartItemCommand, ShoppingCartItem>().ReverseMap();
        }
    }
}
