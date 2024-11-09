using Application.DTOs;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;


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
