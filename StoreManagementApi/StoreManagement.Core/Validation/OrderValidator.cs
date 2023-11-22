using FluentValidation;
using StoreManagement.Core.Models;

namespace StoreManagement.Core.Validation;
public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        RuleFor(order => order.ShippingAddress).NotNull().NotEmpty().Length(1,250);
        RuleFor(order => order.Shipping).NotNull().NotEmpty();
        RuleFor(order => order.TotalPrice).NotNull().NotEmpty();
        RuleFor(order => order.Discount).NotNull().NotEmpty();
        RuleFor(order => order.Tax).NotNull().NotEmpty();
    }
}