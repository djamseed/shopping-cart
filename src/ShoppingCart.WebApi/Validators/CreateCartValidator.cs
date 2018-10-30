namespace ShoppingCart.WebApi.Validators
{
    using FluentValidation;
    using ShoppingCart.Application.Dtos.Requests;

    public sealed class CreateCartValidator : AbstractValidator<CreateCartRequest>
    {
        public CreateCartValidator()
        {
            RuleFor(m => m.CustomerId).NotEmpty().WithMessage("Customer Id should be in a valid format. e.g. fbea6379-b76c-478b-8f86-4f1626fb8acf").WithErrorCode("800");
        }
    }
}
