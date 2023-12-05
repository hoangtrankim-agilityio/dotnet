namespace StoreManagementApiCA.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(v => v.ShippingAddress)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(v => v.TotalPrice)
            .NotEmpty();

        RuleFor(v => v.UserId)
            .NotEmpty();
    }
}
