using Application.Use_Cases.Commands;
using FluentValidation;

namespace Application.Use_Cases.CommandValidators
{
    public class UpdateShoppingCartItemCommandValidator : AbstractValidator<UpdateShoppingCartCommand>
    {
        public UpdateShoppingCartItemCommandValidator()
        {
            RuleFor(command => command.Id)
                .NotEmpty()
                .WithMessage("The shopping cart item ID must not be empty.");

            RuleFor(command => command.Product_Id)
                .NotEmpty()
                .WithMessage("Product ID must not be empty.");

            RuleFor(command => command.Cart_Id)
                .NotEmpty()
                .WithMessage("Cart ID must not be empty.");
            RuleFor(command => command.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0");
        }
    }
}
