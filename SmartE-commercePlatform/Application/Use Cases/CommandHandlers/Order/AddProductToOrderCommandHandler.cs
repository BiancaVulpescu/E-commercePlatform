using Application.Use_Cases.Commands;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class AddProductToOrderCommandHandler(IOrderRepository repository) : IRequestHandler<AddProductToOrderCommand, ErrorOr<Updated>>
    {
        private readonly IOrderRepository repository = repository;

        public async Task<ErrorOr<Updated>> Handle(AddProductToOrderCommand request, CancellationToken cancellationToken)
        {
            return await repository.AddProductToOrderAsync(request.OrderId, request.ProductId, request.Quantity, cancellationToken);
        }
    }
}
