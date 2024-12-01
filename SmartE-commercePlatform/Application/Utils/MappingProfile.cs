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
            CreateMap<Product, ProductDtoMinimal>();
            CreateMap<ShoppingCart, ShoppingCartDtoMinimal>();
            CreateMap<Wishlist, WishlistDtoMinimal>();

            CreateMap<Product, ProductDto>();
            CreateMap<Wishlist, WishlistDto>();
            CreateMap<ShoppingCart, ShoppingCartDto>();

            CreateMap<CreateProductCommand, Product>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.ShoppingCarts, opt => opt.Ignore())
                    .ForMember(dest => dest.ShoppingCartProducts, opt => opt.Ignore())
                    .ForMember(dest => dest.Wishlists, opt => opt.Ignore());
            CreateMap<UpdateProductCommand, Product>()
                    .ForMember(dest => dest.ShoppingCarts, opt => opt.Ignore())
                    .ForMember(dest => dest.ShoppingCartProducts, opt => opt.Ignore())
                    .ForMember(dest => dest.Wishlists, opt => opt.Ignore());

            CreateMap<CreateShoppingCartCommand, ShoppingCart>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Products, opt => opt.Ignore())
                    .ForMember(dest => dest.ShoppingCartProducts, opt => opt.Ignore());
            CreateMap<UpdateShoppingCartCommand, ShoppingCart>()
                    .ForMember(dest => dest.Products, opt => opt.Ignore())
                    .ForMember(dest => dest.ShoppingCartProducts, opt => opt.Ignore());

            CreateMap<CreateWishlistCommand, Wishlist>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Products, opt => opt.Ignore());
            CreateMap<UpdateWishlistCommand, Wishlist>()
                    .ForMember(dest => dest.Products, opt => opt.Ignore());
        }
    }
}
