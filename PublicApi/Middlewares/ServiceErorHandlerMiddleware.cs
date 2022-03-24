using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessLogic.BusinessExceptions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace PublicApi.Middlewares
{
    public class ServiceErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ServiceErrorHandlerMiddleware(RequestDelegate next, ILogger<ServiceErrorHandlerMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BusinessException exception)
            {
                _logger.LogError(exception, "Some malformed request or something");
                await HandleBusinessException(context, exception);
            }
            catch (ValidationException exception)
            {
                _logger.LogError(exception, "Entity validation failed somewhere");
                await HandleValidationException(context, exception);
            }
        }

        private async Task HandleBusinessException(HttpContext context, BusinessException exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var responseModel = ApiResponse<string>.Fail(exception.Message);
            response.StatusCode = exception switch
            {
                ItemNotFoundException ex => (int)HttpStatusCode.NotFound,
                BusinessException ex => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };
            await response.WriteAsync(JsonSerializer.Serialize(responseModel));
        }

        private async Task HandleValidationException(HttpContext context, ValidationException exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var responseModel = ApiResponse<string>.Fail(exception.Message);
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            await response.WriteAsync(JsonSerializer.Serialize(responseModel));
        }

    }
}