using Application.Use_Cases.Authentication.Commands;
using Application.Use_Cases.Authentication.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Authentication.CommandHandlers
{
    public class RefreshAccessCommandHandler(IUserRepository repository, IMapper mapper) : IRequestHandler<RefreshAccessCommand, ErrorOr<RefreshResponseDto>>
    {
        private readonly IUserRepository repository = repository;
        private readonly IMapper mapper = mapper;

        public async Task<ErrorOr<RefreshResponseDto>> Handle(RefreshAccessCommand request, CancellationToken cancellationToken)
        {
            return (await repository.Refresh(request.TokenId, request.RefreshSecret, cancellationToken))
                .Then(mapper.Map<RefreshResponse, RefreshResponseDto>);
        }
    }
}
