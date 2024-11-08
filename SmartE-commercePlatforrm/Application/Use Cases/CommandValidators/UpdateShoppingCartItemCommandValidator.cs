using Application.Use_Cases.Commands;
using FluentValidation;

namespace Application.Use_Cases.CommandValidators
{
    public class UpdateShoppingCartItemCommandValidator : AbstractValidator<UpdateShoppingCartItemCommand>
    {
        public UpdateShoppingCartItemCommandValidator()
        {
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0");
        }
    }
}
