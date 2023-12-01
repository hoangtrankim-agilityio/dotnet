namespace StoreManagementApiCA.Domain.Enums;
public enum OrderStatus
{
    NewOrder = 1,
    Checkout = 2,
    Paid = 3,
    Failed = 4,
    Shipped = 5,
    Delivered = 6,
    Returned = 7,
    Complete = 8
}
