using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class UpdateCategoryCommandHandler(IProductRepository repository, IMapper mapper) : IRequestHandler<UpdateCategoryCommand, ErrorOr<Updated>>
    {
        private readonly IProductRepository repository = repository;
        private readonly IMapper mapper = mapper;

        public async Task<ErrorOr<Updated>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            return await repository.UpdateAsync(mapper.Map<Product>(request), cancellationToken);
        }
    }
}