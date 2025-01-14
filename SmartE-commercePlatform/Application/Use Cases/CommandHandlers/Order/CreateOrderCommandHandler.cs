using Application.Use_Cases.Authentication.Commands;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ErrorOr<Guid>>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, IUserRepository userRepository)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task<ErrorOr<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            var result = await userRepository.GetUserId(request.TokenId, cancellationToken);
            if (result.IsError)
            {
                return result;
            }

            var order = mapper.Map<Order>(request);
            order.UserId = result.Value;

            return await orderRepository.AddAsync(order, cancellationToken);
        }
    }

}
