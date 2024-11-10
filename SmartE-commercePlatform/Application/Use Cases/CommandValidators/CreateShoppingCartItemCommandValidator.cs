using Application.Use_Cases.Commands;
using FluentValidation;

namespace Application.Use_Cases.CommandValidators
{
    public class CreateShoppingCartItemCommandValidator : AbstractValidator<CreateShoppingCartItemCommand>
    {
        public CreateShoppingCartItemCommandValidator()
        {
            RuleFor(p => p.Cart_Id).NotEmpty();
            RuleFor(p => p.Product_Id).NotEmpty();
            //RuleFor(p => p.Quantity).GreaterThan(0);
            RuleFor(command => command.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0");
        }
    }
}
