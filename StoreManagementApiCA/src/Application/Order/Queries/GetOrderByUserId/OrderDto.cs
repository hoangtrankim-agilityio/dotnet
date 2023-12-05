using StoreManagementApiCA.Domain.Entities;
using StoreManagementApiCA.Domain.Enums;

namespace StoreManagementApiCA.Application.Orders.Queries.GetOrderByUserId;

public class OrderDto
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public float TotalPrice { get; set; }
    public float Discount { get; set; }
    public float Tax { get; set; }

    public float Shipping { get; set; }

    public string? ShippingAddress { get; set; }
    public string? Status { get; set; }
    public OrderDto()
    {
        OrderItems = Array.Empty<OrderItem>();
    }

    public IReadOnlyCollection<OrderItem> OrderItems { get; init; }
    private class Mapping : Profile
    {
        public Mapping()
        {
            // CreateMap<Order, OrderDto>().ForMember(des => des.Status,
            //      opt => opt.MapFrom(source => Enum.GetName(typeof(OrderStatus), source.Status)));
            CreateMap<Order, OrderDto>();
        }
    }
}
