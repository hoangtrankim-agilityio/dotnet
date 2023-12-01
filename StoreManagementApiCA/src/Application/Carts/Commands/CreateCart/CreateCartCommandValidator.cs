using StoreManagementApiCA.Application.Carts.Commands.CreateCart;

namespace StoreManagementApiCA.Application.Products.Commands.CreateProduct;

public class CreateCartCommandValidator : AbstractValidator<CreateCartCommand>
{
    public CreateCartCommandValidator()
    {

        RuleFor(v => v.Name)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(v => v.UserId)
            .NotEmpty();
    }
}
