using FluentValidation;
using StoreManagement.Core.Models;

namespace StoreManagement.Core.Validation;
public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        // Check Name is not null, empty and is between 1 and 250 characters
        RuleFor(product => product.Name).NotNull().NotEmpty().Length(1,250);

        // Check Title is not null, empty and is between 1 and 250 characters
        RuleFor(product => product.Title).NotNull().NotEmpty().Length(1,250);

        // Validate Price with a custom error message
        RuleFor(product => product.Price).NotEmpty().WithMessage("Please add a Price");

        RuleFor(product => product.Quantity).NotNull();
    }
}