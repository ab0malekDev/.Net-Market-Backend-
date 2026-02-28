namespace BimMarket.Application.Auth;

public record LoginRequest(string Email, string Password);

public record AuthResponse(
    string AccessToken,
    string RefreshToken,
    long ExpiresIn,
    UserDto User);

public record UserDto(
    string Id,
    string Email,
    string FirstName,
    string LastName,
    List<string>? Roles);
