namespace DigitalBank.Web.Exceptions;

/// <summary>
/// GrpcException interceptos
/// </summary>
public class GrpcExceptionInterceptor : Interceptor
{
    /// <summary>
    /// logger application
    /// </summary>
    private readonly ILogger<GrpcExceptionInterceptor> _logger;

    /// <summary>
    /// Grpc exception interceptor
    /// </summary>
    /// <param name="logger">logger application</param>
    public GrpcExceptionInterceptor(ILogger<GrpcExceptionInterceptor> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Async call unary call
    /// </summary>
    /// <typeparam name="TRequest">request grpc</typeparam>
    /// <typeparam name="TResponse">response grpc</typeparam>
    /// <param name="request">request invoke web to grpc</param>
    /// <param name="context">Context call server</param>
    /// <param name="continuation">Continue action request</param>
    /// <returns>Context action or exception</returns>
    public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
        TRequest request,
        ClientInterceptorContext<TRequest, TResponse> context,
        AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
    {
        var call = continuation(request, context);

        return new AsyncUnaryCall<TResponse>(HandleResponse(call.ResponseAsync), call.ResponseHeadersAsync, call.GetStatus, call.GetTrailers, call.Dispose);
    }

    /// <summary>
    /// Hanlde response
    /// </summary>
    /// <typeparam name="TResponse">Response grpc</typeparam>
    /// <param name="task">Task execution</param>
    /// <returns>Response grpc</returns>
    private async Task<TResponse> HandleResponse<TResponse>(Task<TResponse> task)
    {
        try
        {
            var response = await task;
            return response;
        }
        catch (RpcException e)
        {
            _logger.LogError("Error calling via grpc: {Status} - {Message}", e.Status, e.Message);
            return default;
        }
    }
}

