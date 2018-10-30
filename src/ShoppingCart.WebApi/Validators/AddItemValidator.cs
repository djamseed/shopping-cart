namespace ShoppingCart.WebApi.Validators
{
    using FluentValidation;
    using ShoppingCart.Application.Dtos.Requests;

    public sealed class AddItemValidator : AbstractValidator<AddItemRequest>
    {
        public AddItemValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("Item name is required").WithErrorCode("801");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required").WithErrorCode("802");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity should be greater than zero").WithErrorCode("803");
        }
    }
}
