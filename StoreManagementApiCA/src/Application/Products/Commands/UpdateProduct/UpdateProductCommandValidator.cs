namespace StoreManagementApiCA.Application.Products.Commands.UpdateTodoItem;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(v => v.Name)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(v => v.Price)
            .NotEmpty();

        RuleFor(v => v.Quantity)
            .NotEmpty();
    }
}
