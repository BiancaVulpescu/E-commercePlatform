using Application.Use_Cases.Authentication.Commands;
using Application.Use_Cases.Authentication.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Use_Cases.Authentication.CommandHandlers
{
    public class LoginUserCommandHandler(IUserRepository repository, IMapper mapper) : IRequestHandler<LoginUserCommand, ErrorOr<LoginResponseDto>>
    {
        private readonly IUserRepository repository = repository;
        private readonly IMapper mapper = mapper;
        public async Task<ErrorOr<LoginResponseDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Email = request.Email,
                PasswordHash = request.Password
            };
            var token = await repository.Login(user, cancellationToken);
            return token.Then(mapper.Map<LoginResponse, LoginResponseDto>);
        }
    }

}
