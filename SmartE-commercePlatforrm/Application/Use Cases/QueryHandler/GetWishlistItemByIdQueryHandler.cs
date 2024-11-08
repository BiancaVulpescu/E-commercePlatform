using Application.DTOs;
using Application.Errors;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.QueryHandlers;

public class GetWishlistItemByIdQueryHandler : IRequestHandler<GetWishlistItemByIdQuery, Result<WishlistItemDto>>
{
    private readonly IWishlistItemRepository _wishlistItemRepository;
    private readonly IMapper _mapper;

    public GetWishlistItemByIdQueryHandler(IWishlistItemRepository wishlistItemRepository, IMapper mapper)
    {
        _wishlistItemRepository = wishlistItemRepository;
        _mapper = mapper;
    }

    public async Task<Result<WishlistItemDto>> Handle(GetWishlistItemByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _wishlistItemRepository.GetByIdAsync(request.Id);
            return product is null
                ? Result<WishlistItemDto>.Failure(WishlistItemErrors.NotFound(request.Id))
                : Result<WishlistItemDto>.Success(_mapper.Map<WishlistItemDto>(product));
        }
        catch (Exception e)
        {
            return Result<WishlistItemDto>.Failure(WishlistItemErrors.GetWishlistItemFailed(e.Message));
        }
    }
}