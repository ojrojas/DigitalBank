using Redis.OM;

namespace DigitalBank.Api.Infraestructure;

/// <summary>
/// Redis hosted services
/// </summary>
public class RedisService : IHostedService
{
    private readonly ILogger<RedisService> _logger;
    private readonly RedisConnectionProvider _provider;

    public RedisService(ILogger<RedisService> logger, RedisConnectionProvider provider)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _provider = provider ?? throw new ArgumentNullException(nameof(provider));
    }

    /// <summary>
    /// initial model 
    /// </summary>
    /// <param name="cancellationToken">cancellation token</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">On Exception invalid connection</exception>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
        _logger.LogInformation("Create index model");
        await _provider.Connection.CreateIndexAsync(typeof(LogApplication));
                
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message, ex);
        }

    }

    /// <summary>
    /// Stop services
    /// </summary>
    /// <param name="cancellationToken">Cancellation token request</param>
    /// <returns></returns>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Finish connection");
        return Task.CompletedTask;
    }
}

