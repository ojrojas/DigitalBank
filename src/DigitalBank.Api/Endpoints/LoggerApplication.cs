namespace DigitalBank.Api.Endpoints;


[Authorize]
[Route("api/[controller]")]
public class LoggerApplication : ControllerBase
{
    private readonly RedisCollection<LogApplication> _people;
    private readonly RedisConnectionProvider _provider;

    public LoggerApplication(RedisConnectionProvider provider)
    {
        _provider = provider;
        _people = (RedisCollection<LogApplication>)provider.RedisCollection<LogApplication>();
    }

    [HttpPost]
    public async ValueTask<LogApplication> Create(LogApplication application)
    {
        await _people.InsertAsync(application);
        return application;
    }
}

