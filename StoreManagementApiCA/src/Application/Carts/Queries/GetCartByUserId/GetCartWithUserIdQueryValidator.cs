namespace StoreManagementApiCA.Application.Carts.Queries.GetCartByUserId;

public class GetCartByUserIdQueryValidator : AbstractValidator<GetCartWithUserIdQuery>
{
    public GetCartByUserIdQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");
    }
}
