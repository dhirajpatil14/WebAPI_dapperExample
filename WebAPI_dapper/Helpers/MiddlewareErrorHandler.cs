using System.Net;
using System.Text.Json;

namespace WebAPI_dapper.Helpers
{
    public class MiddlewareErrorHandler
    {
        private readonly ILogger _logger;

        public readonly RequestDelegate _errorHandler;

        public MiddlewareErrorHandler(ILogger<MiddlewareErrorHandler> logger, RequestDelegate errorHandler)
        {
            _logger = logger;
            _errorHandler = errorHandler;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _errorHandler(context);
            }
            catch (Exception ex)
            {
                var resp = context.Response;
                resp.ContentType = "application/json";

                switch(ex)
                {
                    case AppException e:
                        resp.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        resp.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default: 
                        _logger.LogError(ex, ex.Message);
                        resp.StatusCode = (int)(HttpStatusCode.InternalServerError);
                        break;
                }
                var outresult = JsonSerializer.Serialize(new {Message = ex.Message});
                await resp.WriteAsync(outresult);
            }
        }
    }
}
