using Application.DTOs;
using Application.Use_Cases.Authentication.DTOs;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;


namespace Application.Utils
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<ShoppingCartProduct, ShoppingCartProductDtoP>();
			CreateMap<ShoppingCartProduct, ShoppingCartProductDtoSC>();
			CreateMap<OrderProduct, OrderProductDtoP>();
			CreateMap<OrderProduct, OrderProductDtoO>();

			CreateMap<Category, CategoryDtoMinimal>();
			CreateMap<Product, ProductDtoMinimal>();
			CreateMap<ShoppingCart, ShoppingCartDtoMinimal>();
			CreateMap<Wishlist, WishlistDtoMinimal>();
            CreateMap<Order, OrderDtoMinimal>();

            CreateMap<Category, CategoryDto>();
			CreateMap<Product, ProductDto>();
			CreateMap<Wishlist, WishlistDto>();
			CreateMap<ShoppingCart, ShoppingCartDto>();
			CreateMap<Order, OrderDto>();


			CreateMap<CreateProductCommand, Product>()
					.ForMember(dest => dest.Id, opt => opt.Ignore())
					.ForMember(dest => dest.Category, opt => opt.Ignore())
					.ForMember(dest => dest.ShoppingCarts, opt => opt.Ignore())
					.ForMember(dest => dest.ShoppingCartProducts, opt => opt.Ignore())
					.ForMember(dest => dest.Wishlists, opt => opt.Ignore())
					.ForMember(dest => dest.Orders, opt => opt.Ignore())
					.ForMember(dest => dest.OrderProducts, opt => opt.Ignore());
			CreateMap<UpdateProductCommand, Product>()
					.ForMember(dest => dest.Category, opt => opt.Ignore())
					.ForMember(dest => dest.ShoppingCarts, opt => opt.Ignore())
					.ForMember(dest => dest.ShoppingCartProducts, opt => opt.Ignore())
					.ForMember(dest => dest.Wishlists, opt => opt.Ignore())
					.ForMember(dest => dest.Category, opt => opt.Ignore())
					.ForMember(dest => dest.Orders, opt => opt.Ignore())
					.ForMember(dest => dest.OrderProducts, opt => opt.Ignore());

			CreateMap<CreateCategoryCommand, Category>()
					.ForMember(dest => dest.Id, opt => opt.Ignore())
					.ForMember(dest => dest.ParentCategoryId, opt => opt.Ignore())
					.ForMember(dest => dest.ParentCategory, opt => opt.Ignore())
					.ForMember(dest => dest.Subcategories, opt => opt.Ignore());
			CreateMap<UpdateCategoryCommand, Category>()
					.ForMember(dest => dest.ParentCategoryId, opt => opt.Ignore())
					.ForMember(dest => dest.ParentCategory, opt => opt.Ignore())
					.ForMember(dest => dest.Subcategories, opt => opt.Ignore());

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

			CreateMap<CreateOrderCommand, Order>()
					.ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.UserId, opt => opt.Ignore())
                    .ForMember(dest => dest.Products, opt => opt.Ignore())
					.ForMember(dest => dest.OrderProducts, opt => opt.Ignore());
			CreateMap<UpdateOrderCommand, Order>()
					.ForMember(dest => dest.UserId, opt => opt.Ignore())
                    .ForMember(dest => dest.Products, opt => opt.Ignore())
                    .ForMember(dest => dest.OrderProducts, opt => opt.Ignore());

			CreateMap<LoginResponse, LoginResponseDto>().ReverseMap();
			CreateMap<RefreshResponse, RefreshResponseDto>().ReverseMap();
		}
	}
}
