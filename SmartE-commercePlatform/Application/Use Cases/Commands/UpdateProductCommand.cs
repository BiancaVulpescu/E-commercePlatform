using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Commands
{
    public class UpdateProductCommand : CreateProductCommandBase, IRequest<ErrorOr<Updated>>
    {
        public Guid Id { get; set; }
    }
}
