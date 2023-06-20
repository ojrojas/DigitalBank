namespace DigitalBank.Infraestructure.Repositories;

/// <summary>
/// Generic Repository
/// </summary>
/// <typeparam name="T">Entity Model</typeparam>
public class GenericRepository<T> where T : class, IAggregateRoot
{
    /// <summary>
    /// Logger application
    /// </summary>
    private readonly ILogger<GenericRepository<T>> _logger;
    /// <summary>
    /// DbContext injected
    /// </summary>
    private readonly SqlConnection _connection;
    /// <summary>
    /// IConfiguration application 
    /// </summary>
    private readonly IConfiguration _configuration;

   /// <summary>
   /// Constructor GenericRepository
   /// </summary>
   /// <param name="logger">Logger application</param>
   /// <param name="configuration">Configuration application</param>
    public GenericRepository(ILogger<GenericRepository<T>> logger, IConfiguration configuration)

    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(configuration);

        _logger = logger;
        _configuration = configuration;
        var connectionString = _configuration.GetSection("Connection").Value;

        _connection = new SqlConnection(connectionString);
    }

    /// <summary>
    /// Sqlrawexecution 
    /// </summary>
    /// <param name="NameProcedureOrQueryString">Name procedure o query string</param>
    /// <param name="parameters">Parameters query</param>
    /// <param name="commandType">type command {Text, StoreProducre, Table}</param>
    /// <param name="cancellationToken">Cancellation token request</param>
    /// <returns>Single Response</returns>
    public async Task<T> SqlRawExecutionAsync(
        string nameProcedureOrQueryString, DynamicParameters parameters, CommandType commandType, CancellationToken cancellationToken)
    {
        using (_connection)
        {
            try
            {
                _logger.LogInformation("Execute sqlraw query {nameProcedureOrQueryString}", nameProcedureOrQueryString);
                var cmd = new CommandDefinition(nameProcedureOrQueryString, null, null, null, commandType);
                _logger.LogInformation("Open connection...");
                await _connection.OpenAsync();
                if (parameters is not null)
                    cmd = new CommandDefinition(nameProcedureOrQueryString, parameters, null, null, commandType);
                cancellationToken.ThrowIfCancellationRequested();
                _logger.LogInformation("Task execute sqlraw...");
                var response = await _connection.QueryFirstOrDefaultAsync<T>(cmd);
                _logger.LogInformation("Close connection...");
                _logger.LogInformation("Sending response");
                return response;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message, ex);
            }
        }
    }

    /// <summary>
    /// Sqlrawexecution multiple result
    /// </summary>
    /// <param name="NameProcedureOrQueryString">Name procedure o query string</param>
    /// <param name="parameters">Parameters query</param>
    /// <param name="commandType">type command {Text, StoreProducre, Table}</param>
    /// <param name="cancellationToken">Cancellation token request</param>
    /// <returns>Multiple response Response</returns>
    public async Task<IEnumerable<T>> SqlRawExecutionMultimeAsync(
        string nameProcedureOrQueryString, DynamicParameters parameters, CommandType commandType, CancellationToken cancellationToken)
    {
        using (_connection)
        {
            try
            {
                _logger.LogInformation("Execute sqlraw query {nameProcedureOrQueryString}", nameProcedureOrQueryString);
                var cmd = new CommandDefinition(nameProcedureOrQueryString, null, null, null, commandType);
                _logger.LogInformation("Open connection...");
                await _connection.OpenAsync();
                if (parameters is not null)
                    cmd = new CommandDefinition(nameProcedureOrQueryString, parameters, null, null, commandType);
                cancellationToken.ThrowIfCancellationRequested();
                _logger.LogInformation("Task execute sqlraw...");
                var response = await _connection.QueryAsync<T>(cmd);
                _logger.LogInformation("Close connection...");
                _logger.LogInformation("Sending response");
                return response;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message, ex);
            }
        }
    }
}