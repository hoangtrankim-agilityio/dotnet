using FluentValidation;
using StoreManagement.Core.Models;

namespace StoreManagement.Core.Validation;
public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        // Check User is not null, empty and is between 1 and 250 characters
        RuleFor(user => user.UserName).NotNull().NotEmpty().Length(1,250);

        RuleFor(user => user.Email).EmailAddress();
        RuleFor(user => user.PhoneNumber).NotNull().Length(10);
    }
}