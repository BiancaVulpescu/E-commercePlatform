﻿using Application.DTOs;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.QueryHandlers;

public class GetAllCategoiresQueryHandler(ICategoryRepository repository, IMapper mapper) : IRequestHandler<GetAllCategoriesQuery, ErrorOr<IEnumerable<CategoryDtoMinimal>>>
{
    private readonly ICategoryRepository repository = repository;
    private readonly IMapper mapper = mapper;

    public async Task<ErrorOr<IEnumerable<CategoryDtoMinimal>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        return (await repository.GetAllAsync(cancellationToken)).Then(mapper.Map<IEnumerable<CategoryDtoMinimal>>);
    }
}
