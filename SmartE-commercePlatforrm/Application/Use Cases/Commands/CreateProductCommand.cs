using MediatR;

namespace Application.Use_Cases.Commands
{
    public class CreateProductCommand : CreateCommandBase, IRequest<Result<Guid>>
    {
    }
}
