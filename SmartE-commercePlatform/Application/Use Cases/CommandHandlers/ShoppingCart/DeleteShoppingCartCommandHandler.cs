using Application.Use_Cases.Commands;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class DeleteShoppingCartCommandHandler(IShoppingCartRepository repository) : IRequestHandler<DeleteShoppingCartCommand, ErrorOr<Deleted>>
    {
        private readonly IShoppingCartRepository repository = repository;

        public async Task<ErrorOr<Deleted>> Handle(DeleteShoppingCartCommand request, CancellationToken cancellationToken)
        {
            return await repository.DeleteAsync(request.Id, cancellationToken);
        }
    }
}
