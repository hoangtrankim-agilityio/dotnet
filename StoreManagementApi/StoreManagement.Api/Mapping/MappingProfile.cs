using AutoMapper;
using StoreManagement.Api.Resources;
using StoreManagement.Core.Models;

namespace StoreManagement.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            CreateMap<User, UserResource>();
            CreateMap<Cart, CartResource>();
            CreateMap<Product, ProductResource>();
            CreateMap<Order, OrderResource>();
            CreateMap<OrderItem, OrderItemResource>();
            CreateMap<CartItem, CartItemResource>();

            // Resource to Domain
            CreateMap<UserResource, User>();
            CreateMap<CartResource, Cart>();
            CreateMap<ProductResource, Product>();
            CreateMap<OrderResource, Order>();
            CreateMap<OrderItemResource, OrderItem>();
            CreateMap<CartItemResource, CartItem>();
        }
    }
}