using Application.Use_Cases.Commands;
using FluentValidation;

namespace Application.Use_Cases.CommandValidators
{
    public class UpdateWishlistItemCommandValidator : AbstractValidator<UpdateWishlistCommand>
    {
        public UpdateWishlistItemCommandValidator()
        {
            RuleFor(command => command.Id)
                .NotEmpty()
                .WithMessage("The wishlist item ID must not be empty.");

            RuleFor(command => command.Product_Id)
                .NotEmpty()
                .WithMessage("Product ID must not be empty.");

            RuleFor(command => command.List_Id)
                .NotEmpty()
                .WithMessage("List ID must not be empty.");
        }
    }
}
