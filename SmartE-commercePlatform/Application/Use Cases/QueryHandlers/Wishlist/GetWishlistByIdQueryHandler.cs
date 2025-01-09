using Application.DTOs;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.QueryHandlers;

public class GetWishlistByIdQueryHandler(IWishlistRepository repository, IMapper mapper) : IRequestHandler<GetWishlistByIdQuery, ErrorOr<WishlistDto>>
{
    private readonly IWishlistRepository repository = repository;
    private readonly IMapper mapper = mapper;

    public async Task<ErrorOr<WishlistDto>> Handle(GetWishlistByIdQuery request, CancellationToken cancellationToken)
    {
        return (await repository.GetByIdAsync(request.Id, cancellationToken)).Then(mapper.Map<WishlistDto>);
    }
}