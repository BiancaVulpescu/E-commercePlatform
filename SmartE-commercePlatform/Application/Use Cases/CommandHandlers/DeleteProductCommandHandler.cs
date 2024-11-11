using Application.Errors;
using Application.Use_Cases.Commands;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<Unit, ProductError>>
    {
        private readonly IProductRepository productRepository;
        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<Result<Unit, ProductError>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await productRepository.GetByIdAsync(request.Id);
                if (product is null)
                {
                    return Result<Unit, ProductError>.Err(ProductError.NotFound(request.Id));
                }
                await productRepository.DeleteAsync(product.Id);
                return Result<Unit, ProductError>.Ok(Unit.Value);
            }
            catch (Exception e)
            {
                return Result<Unit, ProductError>.Err(ProductError.DeleteProductFailed(e.Message));
            }
        }
    }
}
