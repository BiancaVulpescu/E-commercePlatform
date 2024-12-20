﻿using Application.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Queries
{
    public class GetProductsByTitleQuery : IRequest<ErrorOr<IEnumerable<ProductDtoMinimal>>>
    {
        public string Title { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }


}
