using System;
using System.Net;

namespace DigitalBank.Web.Services;

/// <summary>
/// Logservice
/// </summary>
public class LogService : ILogService
{
    /// <summary>
    /// Token services
    /// </summary>
	private readonly ITokenService _tokenService;
    /// <summary>
    /// Configuration application
    /// </summary>
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Log service
    /// </summary>
    /// <param name="tokenService">token service</param>
    /// <param name="configuration">configuration application</param>
    /// <exception cref="ArgumentNullException">Exception null arguments</exception>
    public LogService(ITokenService tokenService, IConfiguration configuration)
    {
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    /// <summary>
    /// Create log application
    /// </summary>
    /// <param name="logApplication">model log application</param>
    /// <returns>Log application created</returns>
    public async ValueTask<LogApplication> CreateLogAsync(LogApplication logApplication)
    {
        try
        {
            var token = await _tokenService.GetTokenAsync();
            var client = CreateHttpClient(token);
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(logApplication));
            var url = _configuration["UrlApi"];
            HttpResponseMessage response = await client.PostAsync(url, content);
            await HandlerResponse(response);
            string serialize = await response.Content.ReadAsStringAsync();
            var result = System.Text.Json.JsonSerializer.Deserialize<LogApplication>(serialize);
            return result;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message, ex);
        }
        
    }

    /// <summary>
    /// Create client https
    /// </summary>
    /// <param name="token">token</param>
    /// <returns>Http client provider</returns>
    private HttpClient CreateHttpClient(string token)
    {
        HttpClient _client = null;
        if (!string.IsNullOrEmpty(token))
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErros) => true
            };

            _client = new HttpClient(handler)
            {
                DefaultRequestVersion = HttpVersion.Version20,
                DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrLower
            };

            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        return _client;
    }

    /// <summary>
    /// Manage response result
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="HttpRequestException"></exception>
    private async Task HandlerResponse(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden ||
                response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new InvalidOperationException(nameof(content));
            }

            throw new HttpRequestException(content);
        }
    }
}

