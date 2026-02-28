namespace BimMarket.Infrastructure.Auth;

public class JwtSettings
{
    public const string SectionName = "Jwt";
    public string Secret { get; set; } = "";
    public string Issuer { get; set; } = "BimMarket";
    public string Audience { get; set; } = "BimMarket";
    public int ExpirationMinutes { get; set; } = 60;
}
