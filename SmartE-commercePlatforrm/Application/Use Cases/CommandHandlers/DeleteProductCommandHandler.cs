using Application.Errors;
using Application.Use_Cases.Commands;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<Unit>>
    {
        private readonly IProductRepository productRepository;
        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<Result<Unit>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await productRepository.GetByIdAsync(request.Id);
                if (product is null)
                {
                    return Result<Unit>.Failure(ProductErrors.NotFound(request.Id));
                }
                await productRepository.DeleteAsync(product.Id);
                return Result<Unit>.Success(Unit.Value);
            }
            catch (Exception e)
            {
                return Result<Unit>.Failure(ProductErrors.DeleteProductFailed(e.Message));
            }
        }
    }
}
