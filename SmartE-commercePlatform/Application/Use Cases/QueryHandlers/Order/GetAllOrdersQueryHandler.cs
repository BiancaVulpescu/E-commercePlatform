using Application.DTOs;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.QueryHandlers
{
    public class GetAllOrdersQueryHandler(IOrderRepository repository, IMapper mapper) : IRequestHandler<GetAllOrdersQuery, ErrorOr<IEnumerable<OrderDtoMinimal>>>
    {
        private readonly IOrderRepository repository = repository;
        private readonly IMapper mapper = mapper;

        public async Task<ErrorOr<IEnumerable<OrderDtoMinimal>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            return (await repository.GetAllAsync(cancellationToken))
                .Then(mapper.Map<IEnumerable<OrderDtoMinimal>>);
        }
    }
}
