using Application.Use_Cases.Commands;
using FluentValidation;

namespace Application.Use_Cases.CommandValidators
{
    public class CreateWishlistItemCommandValidator : AbstractValidator<CreateWishlistCommand>
    {
        public CreateWishlistItemCommandValidator()
        {
            RuleFor(command => command.Product_Id)
                .NotEmpty()
                .WithMessage("Product ID must not be empty.");

            RuleFor(command => command.List_Id)
                .NotEmpty()
                .WithMessage("List ID must not be empty.");
        }
    }
}
