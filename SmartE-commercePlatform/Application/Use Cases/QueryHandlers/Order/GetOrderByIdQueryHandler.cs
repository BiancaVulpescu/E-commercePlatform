using Application.DTOs;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.QueryHandlers
{
    public class GetOrderByIdQueryHandler(IOrderRepository repository, IMapper mapper) : IRequestHandler<GetOrderByIdQuery, ErrorOr<OrderDto>>
    {
        private readonly IOrderRepository repository = repository;
        private readonly IMapper mapper = mapper;

        public async Task<ErrorOr<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            return (await repository.GetByIdAsync(request.Id, cancellationToken)).Then(mapper.Map<OrderDto>);
        }
    }
}
