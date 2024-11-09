using MediatR;

namespace Application.Use_Cases.Commands
{
    public class UpdateProductCommand : CreateProductCommandBase, IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
    }
}
