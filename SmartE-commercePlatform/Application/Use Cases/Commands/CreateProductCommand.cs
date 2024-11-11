using Application.Errors;
using MediatR;

namespace Application.Use_Cases.Commands
{
    public class CreateProductCommand : CreateProductCommandBase, IRequest<Result<Guid, ProductError>>
    {

    }
}
