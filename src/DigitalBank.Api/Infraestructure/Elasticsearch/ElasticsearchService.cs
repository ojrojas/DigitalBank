namespace DigitalBank.Api.Infraestructure;

public class ElasticsearchService : IElasticsearchService
{
    private ElasticsearchClient _client;
    private ElasticsearchClientSettings _settigns;
    private readonly ElasticsearchConfiguration _configuration;
    private ILogger<ElasticsearchService> _logger;
    private readonly string _indexName;

    public ElasticsearchService(IOptions<ElasticsearchConfiguration> options, ILogger<ElasticsearchService> logger)
    {
        try
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = options.Value ?? throw new ArgumentNullException(nameof(options));
            _settigns = new ElasticsearchClientSettings(new Uri(_configuration.UrlElasticsearch))
                .Authentication(new BasicAuthentication(_configuration.UserName, _configuration.Password))
                .DisableDirectStreaming();
            _client = new ElasticsearchClient(_settigns);
            _indexName = typeof(LogApplication).Name.ToLower();
            var ping = _client.Ping();
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            throw new Exception(ex.Message, ex);
        }
    }

    public async ValueTask<LogApplication> CreateAsync(LogApplication entity, CancellationToken cancellationToken)
    {
        _ = entity ?? throw new InvalidOperationException(nameof(entity));
        var id = entity.GetType().GetProperties().FirstOrDefault(prop => prop.Name.Equals("Id", StringComparison.Ordinal)).GetValue(entity);
        var created = await _client.IndexAsync(entity, req => req.Index(_indexName));
        if (cancellationToken.IsCancellationRequested) cancellationToken.ThrowIfCancellationRequested();
        _logger.LogInformation($"Create record type of {typeof(LogApplication)} successfull");

        return await GetByIdAsync(Guid.Parse(id.ToString()), cancellationToken);
    }

    public async ValueTask<LogApplication> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _client.GetAsync<LogApplication>(id.ToString(), req => req.Index(_indexName), cancellationToken);
            if (cancellationToken.IsCancellationRequested) cancellationToken.ThrowIfCancellationRequested();
            if (!response.IsSuccess())
                throw new InvalidOperationException($"do not found values with key value id: {id}");
            _logger.LogInformation($"Get object by key:{id}, values: {response.Source}");
            return response.Source;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message, ex);
        }
    }
}

