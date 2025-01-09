using Application.DTOs;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;

namespace SmartE_commercePlatform.UnitTests
{
    public class AutoMapperTests
    {
        //teste la automapper pentru mapare category
        [Fact]
        public void MapFrom_Product_To_ProductDtoMinimal_IsValid()
        {
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDtoMinimal>());
            configuration.AssertConfigurationIsValid();
        }
        [Fact]
        public void MapFrom_ShoppingCart_To_ShoppingCartDtoMinimal_IsValid()
        {
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<ShoppingCart, ShoppingCartDtoMinimal>());
            configuration.AssertConfigurationIsValid();
        }
        [Fact]
        public void MapFrom_Wishlist_To_WishlistDtoMinimal_IsValid()
        {
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Wishlist, WishlistDtoMinimal>());
            configuration.AssertConfigurationIsValid();
        }
        [Fact]
        public void MapFrom_Product_To_ProductDto_IsValid()
        {
            var configuration = new MapperConfiguration(cfg => {
                cfg.CreateMap<Product, ProductDtoMinimal>();
                cfg.CreateMap<ShoppingCart, ShoppingCartDtoMinimal>();
                cfg.CreateMap<Wishlist, WishlistDtoMinimal>();
                cfg.CreateMap<Product, ProductDto>();
            });
            configuration.AssertConfigurationIsValid();
        }
        [Fact]
        public void MapFrom_ShoppingCart_To_ShoppingCartDto_IsValid()
        {
            var configuration = new MapperConfiguration(cfg => {
                cfg.CreateMap<Product, ProductDtoMinimal>();
                cfg.CreateMap<ShoppingCart, ShoppingCartDtoMinimal>();
                cfg.CreateMap<Wishlist, WishlistDtoMinimal>();
                cfg.CreateMap<ShoppingCart, ShoppingCartDto>();
            });
            configuration.AssertConfigurationIsValid();
        }
        [Fact]
        public void MapFrom_Wishlist_To_WishlistDto_IsValid()
        {
            var configuration = new MapperConfiguration(cfg => {
                cfg.CreateMap<Product, ProductDtoMinimal>();
                cfg.CreateMap<ShoppingCart, ShoppingCartDtoMinimal>();
                cfg.CreateMap<Wishlist, WishlistDtoMinimal>();
                cfg.CreateMap<Wishlist, WishlistDto>();
            });
            configuration.AssertConfigurationIsValid();
        }
        [Fact]
        public void MapFrom_CreateProductCommand_To_Product_IsValid()
        {
            var configuration = new MapperConfiguration(cfg => 
                cfg.CreateMap<CreateProductCommand, Product>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.ShoppingCarts, opt => opt.Ignore())
                    .ForMember(dest => dest.ShoppingCartProducts, opt => opt.Ignore())
                    .ForMember(dest => dest.Wishlists, opt => opt.Ignore())
            );
            configuration.AssertConfigurationIsValid();
        }
        [Fact]
        public void MapFrom_UpdateProductCommand_To_Product_IsValid()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<UpdateProductCommand, Product>()
                    .ForMember(dest => dest.ShoppingCarts, opt => opt.Ignore())
                    .ForMember(dest => dest.ShoppingCartProducts, opt => opt.Ignore())
                    .ForMember(dest => dest.Wishlists, opt => opt.Ignore())
            );
            configuration.AssertConfigurationIsValid();
        }
        [Fact]
        public void MapFrom_CreateShoppingCartCommand_To_ShoppingCart_IsValid()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<CreateShoppingCartCommand, ShoppingCart>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Products, opt => opt.Ignore())
                    .ForMember(dest => dest.ShoppingCartProducts, opt => opt.Ignore())
            );
            configuration.AssertConfigurationIsValid();
        }
        [Fact]
        public void MapFrom_UpdateShoppingCartCommand_To_ShoppingCart_IsValid()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<UpdateShoppingCartCommand, ShoppingCart>()
                    .ForMember(dest => dest.Products, opt => opt.Ignore())
                    .ForMember(dest => dest.ShoppingCartProducts, opt => opt.Ignore())
            );
            configuration.AssertConfigurationIsValid();
        }
        [Fact]
        public void MapFrom_CreateWishlistCommand_To_Wishlist_IsValid()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<CreateWishlistCommand, Wishlist>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Products, opt => opt.Ignore())
            );
            configuration.AssertConfigurationIsValid();
        }
        [Fact]
        public void MapFrom_UpdateWishlistCommand_To_Wishlist_IsValid()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<UpdateWishlistCommand, Wishlist>()
                    .ForMember(dest => dest.Products, opt => opt.Ignore())
            );
            configuration.AssertConfigurationIsValid();
        }
    }
}
