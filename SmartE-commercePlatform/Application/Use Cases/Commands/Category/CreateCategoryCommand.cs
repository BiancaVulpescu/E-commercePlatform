using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Commands
{
    public class CreateCategoryCommand : CreateCategoryCommandBase, IRequest<ErrorOr<Guid>>
    {
    }
}
