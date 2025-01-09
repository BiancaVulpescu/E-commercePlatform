using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class UpdateWishlistCommandHandler(IWishlistRepository repository, IMapper mapper) : IRequestHandler<UpdateWishlistCommand, ErrorOr<Updated>>
    {
        private readonly IWishlistRepository repository = repository;
        private readonly IMapper mapper = mapper;

        public async Task<ErrorOr<Updated>> Handle(UpdateWishlistCommand request, CancellationToken cancellationToken)
        {
            return await repository.UpdateAsync(mapper.Map<Wishlist>(request), cancellationToken);
        }
    }
}
