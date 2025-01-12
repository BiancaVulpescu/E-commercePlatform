using Application.DTOs;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.QueryHandlers
{
    public class GetAllProductsByOrderIdQueryHandler(IOrderRepository repository, IMapper mapper) : IRequestHandler<GetAllProductsByOrderIdQuery, ErrorOr<IEnumerable<OrderProductDtoP>>>
    {
        private readonly IOrderRepository repository = repository;
        private readonly IMapper mapper = mapper;

        public async Task<ErrorOr<IEnumerable<OrderProductDtoP>>> Handle(GetAllProductsByOrderIdQuery request, CancellationToken cancellationToken)
        {
            return (await repository.GetAllProductsByOrderIdAsync(request.Id, cancellationToken))
                .Then(mapper.Map<IEnumerable<OrderProductDtoP>>);
        }
    }
}
