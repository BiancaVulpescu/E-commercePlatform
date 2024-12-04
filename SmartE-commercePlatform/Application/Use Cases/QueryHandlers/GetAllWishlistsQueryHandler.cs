using Application.DTOs;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.QueryHandlers;

public class GetAllWishlistsQueryHandler(IWishlistRepository repository, IMapper mapper) : IRequestHandler<GetAllWishlistsQuery, ErrorOr<IEnumerable<WishlistDtoMinimal>>>
{
    private readonly IWishlistRepository repository = repository;
    private readonly IMapper mapper = mapper;

    public async Task<ErrorOr<IEnumerable<WishlistDtoMinimal>>> Handle(GetAllWishlistsQuery request, CancellationToken cancellationToken)
    {
        return (await repository.GetAllAsync(cancellationToken)).Then(mapper.Map<IEnumerable<WishlistDtoMinimal>>);
    }
}
