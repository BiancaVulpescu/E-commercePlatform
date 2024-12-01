using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Commands
{
    public class UpdateShoppingCartCommand : CreateShoppingCartCommandBase, IRequest<ErrorOr<Updated>>
    {
        public Guid Id { get; set; }
    }
}
