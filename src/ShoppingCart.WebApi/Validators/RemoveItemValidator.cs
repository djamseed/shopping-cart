namespace ShoppingCart.WebApi.Validators
{
    using FluentValidation;
    using ShoppingCart.Application.Dtos.Requests;

    public sealed class RemoveItemValidator : AbstractValidator<RemoveItemRequest>
    {
        public RemoveItemValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("Item name is required").WithErrorCode("801");
        }
    }
}
