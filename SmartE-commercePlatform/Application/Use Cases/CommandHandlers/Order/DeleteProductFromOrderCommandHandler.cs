using Application.Use_Cases.Commands;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class DeleteProductFromOrderCommandHandler(IOrderRepository repository) : IRequestHandler<DeleteProductFromOrderCommand, ErrorOr<Deleted>>
    {
        private readonly IOrderRepository repository = repository;

        public async Task<ErrorOr<Deleted>> Handle(DeleteProductFromOrderCommand request, CancellationToken cancellationToken)
        {
            return await repository.DeleteProductFromOrderAsync(request.OrderId, request.ProductId, cancellationToken);
        }
    }
}
