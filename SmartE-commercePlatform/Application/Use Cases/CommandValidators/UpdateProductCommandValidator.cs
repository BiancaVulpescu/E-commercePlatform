using Application.Use_Cases.Commands;
using FluentValidation;

namespace Application.Use_Cases.CommandValidators
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Product Title must not be empty and has a maximum dimension of 100 charachters.");


            RuleFor(p => p.Category)
                .NotEmpty()
                .MaximumLength(200)
                .WithMessage("Product Category must not be empty and has a maximum dimension of 200 charachters.");

            RuleFor(p => p.Description)
                .NotEmpty()
                .MaximumLength(200)
                .WithMessage("Product Description must not be empty and has a maximum dimension of 200 charachters.");
        }
    }
}
