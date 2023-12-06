using StoreManagementApiCA.Application.CartItems.Commands.CreateCartItem;

namespace StoreManagementApiCA.Application.Products.Commands.CreateProduct;

public class CreateCartItemCommandValidator : AbstractValidator<CreateCartItemCommand>
{
    public CreateCartItemCommandValidator()
    {

        RuleFor(v => v.Quantity)
            .NotEmpty();

        RuleFor(v => v.Price)
            .NotEmpty();

        RuleFor(v => v.Active)
            .NotEmpty();

        RuleFor(v => v.ProductId)
            .NotEmpty();

        RuleFor(v => v.CartId)
            .NotEmpty();
    }
}
