using Application.Use_Cases.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Use_Cases.CommandValidators
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(p => p.Title).NotEmpty().MaximumLength(100);
            RuleFor(p => p.Category).NotEmpty().MaximumLength(200);
            RuleFor(p => p.Description).NotEmpty().MaximumLength(200);
        }
    }
}
