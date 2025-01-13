using Application.DTOs;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.QueryHandlers
{
    public class GetAllOrdersByUserIdQueryHandler(IOrderRepository repository, IMapper mapper) : IRequestHandler<GetAllOrdersByUserIdQuery, ErrorOr<IEnumerable<OrderDtoMinimal>>>
    {
        private readonly IOrderRepository repository = repository;
        private readonly IMapper mapper = mapper;

        public async Task<ErrorOr<IEnumerable<OrderDtoMinimal>>> Handle(GetAllOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            return (await repository.GetAllOrdersByUserIdAsync(request.UserId, cancellationToken))
                .Then(mapper.Map<IEnumerable<OrderDtoMinimal>>);
        }
    }
}
