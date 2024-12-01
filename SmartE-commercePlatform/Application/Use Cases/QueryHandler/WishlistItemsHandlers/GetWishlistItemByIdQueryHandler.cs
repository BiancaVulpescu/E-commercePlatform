using Application.DTOs;
using Application.Errors;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.QueryHandlers;

public class GetWishlistItemByIdQueryHandler : IRequestHandler<GetWishlistItemByIdQuery, Result<WishlistItemDto, WishlistItemError>>
{
    private readonly IWishlistItemRepository _wishlistItemRepository;
    private readonly IMapper _mapper;

    public GetWishlistItemByIdQueryHandler(IWishlistItemRepository wishlistItemRepository, IMapper mapper)
    {
        _wishlistItemRepository = wishlistItemRepository;
        _mapper = mapper;
    }

    public async Task<Result<WishlistItemDto, WishlistItemError>> Handle(GetWishlistItemByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _wishlistItemRepository.GetByIdAsync(request.Id);
            return product is null
                ? Result<WishlistItemDto, WishlistItemError>.Err(WishlistItemError.NotFound(request.Id))
                : Result<WishlistItemDto, WishlistItemError>.Ok(_mapper.Map<WishlistItemDto>(product));
        }
        catch (Exception e)
        {
            return Result<WishlistItemDto, WishlistItemError>.Err(WishlistItemError.GetWishlistItemFailed(e.Message));
        }
    }
}