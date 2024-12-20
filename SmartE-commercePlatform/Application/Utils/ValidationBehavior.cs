﻿using ErrorOr;
using FluentValidation;
using MediatR;

namespace Application.Utils
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
    {
        private readonly IEnumerable<IValidator<TRequest>> validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();
            if (failures.Count != 0)
            {
                var errors = failures.ConvertAll(
                    error => Error.Validation(
                        code: error.PropertyName,
                        description: error.ErrorMessage
                        )
                    );
                return (dynamic)errors;
            }

            return await next();
        }
    }
}
