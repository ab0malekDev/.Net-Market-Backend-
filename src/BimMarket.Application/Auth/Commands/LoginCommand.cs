using BimMarket.Application.Auth;
using BimMarket.Application.Common.Abstractions;
using MediatR;

namespace BimMarket.Application.Auth.Commands;

public record LoginCommand(string Email, string Password) : IRequest<AuthResponse?>;

public class LoginCommandHandler(IAuthService authService) : IRequestHandler<LoginCommand, AuthResponse?>
{
    public async Task<AuthResponse?> Handle(LoginCommand request, CancellationToken cancellationToken) =>
        await authService.LoginAsync(request.Email, request.Password, cancellationToken);
}
