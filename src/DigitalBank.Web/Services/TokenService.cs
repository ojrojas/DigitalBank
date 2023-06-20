

namespace DigitalBank.Web.Services;

/// <summary>
/// Service token
/// </summary>
public class TokenService : ITokenService
{
    /// <summary>
    /// logger application
    /// </summary>
    private readonly ILogger<TokenService> _logger;
    /// <summary>
    /// options 
    /// </summary>
    private readonly OptionToken _options;

    /// <summary>
    /// Token services
    /// </summary>
    /// <param name="logger">logger application</param>
    /// <param name="options">options application</param>
    /// <exception cref="ArgumentNullException">Argument exception</exception>
    public TokenService(ILogger<TokenService> logger, IOptions<OptionToken> options)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = options.Value ?? throw new ArgumentNullException(nameof(options));
    }

    /// <summary>
    /// Get Token jwt
    /// </summary>
    /// <param name="user">User login application</param>
    /// <returns>jwt string token</returns>
    public async ValueTask<string> GetTokenAsync()
    {
        _logger.LogInformation("Created pass jwt");
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_options.SecretPhrase);
        var claims = new List<Claim> {
       new Claim("digitalbank",Guid.NewGuid().ToString()),
    };

        await Task.CompletedTask;

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims.ToArray()),
            Expires = DateTime.UtcNow.AddHours(12),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

        _logger.LogInformation("Jwt signed...");

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

