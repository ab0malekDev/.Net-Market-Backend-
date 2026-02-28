using BimMarket.Application.Auth;

namespace BimMarket.Application.Common.Abstractions;

public interface IAuthService
{
    Task<AuthResponse?> LoginAsync(string email, string password, CancellationToken ct = default);
}
