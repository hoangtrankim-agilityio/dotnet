namespace StoreManagementApiCA.Application.CartItems.Commands.UpdateCartItem;

public class UpdateCartItemCommandValidator : AbstractValidator<UpdateCartItemCommand>
{
    public UpdateCartItemCommandValidator()
    {

        RuleFor(v => v.Quantity)
            .NotEmpty();

        RuleFor(v => v.Price)
            .NotEmpty();

        RuleFor(v => v.Active)
            .NotEmpty();
    }
}
