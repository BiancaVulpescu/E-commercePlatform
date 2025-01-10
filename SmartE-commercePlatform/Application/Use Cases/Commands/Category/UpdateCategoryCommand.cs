using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Commands
{
    public class UpdateCategoryCommand : CreateCategoryCommandBase, IRequest<ErrorOr<Updated>>
    {
        public Guid Id { get; set; }
    }
}
