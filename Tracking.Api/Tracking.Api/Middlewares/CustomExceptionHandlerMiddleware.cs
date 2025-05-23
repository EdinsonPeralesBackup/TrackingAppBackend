using Newtonsoft.Json;
using Tracking.Api.Utils;
using Tracking.Application.Common.Dtos;
using Tracking.Application.Common.Exceptions;
using Tracking.Application.Common.Settings;

namespace Tracking.Api.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;
        private readonly CustomJsonResolver _jsonResolver;
        private readonly AppSettings _appSettings;

        public CustomExceptionHandlerMiddleware(RequestDelegate next, IWebHostEnvironment env, ILogger<CustomExceptionHandlerMiddleware> logger,
            AppSettings settings, CustomJsonResolver jsonResolver)
        {
            _next = next;
            _env = env;
            _logger = logger;
            _appSettings = settings;
            _jsonResolver = jsonResolver;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var settings = new JsonSerializerSettings { ContractResolver = _jsonResolver };
            var statusCode = StatusCodes.Status500InternalServerError;
            var error = new RespuestaErrorDTO();
            error.Mensaje = _appSettings.GeneralErrorMessage;

            var typeError = string.Empty;

            switch (exception)
            {
                case BadRequestException customException:
                    statusCode = StatusCodes.Status400BadRequest;
                    error.Mensaje = customException.MensajeUsuario;
                    typeError = typeof(BadRequestException).Name;
                    break;
                case NotFoundException customException:
                    statusCode = StatusCodes.Status404NotFound;
                    error.Mensaje = customException.MensajeUsuario;
                    typeError = typeof(NotFoundException).Name;
                    break;
                case DeleteFailureException customException:
                    statusCode = StatusCodes.Status400BadRequest;
                    error.Mensaje = customException.MensajeUsuario;
                    typeError = typeof(DeleteFailureException).Name;
                    break;
                case HttpException customException:
                    statusCode = customException.StatusCode;
                    error.Mensaje = customException.MensajeUsuario;
                    error.Errores = customException.Errores;
                    typeError = typeof(HttpException).Name;
                    break;
                default:
                    break;
            }

            if (statusCode == StatusCodes.Status500InternalServerError)
            {
                _logger.LogError("{@Exception}", exception);
            }
            else
            {
                _logger.LogWarning("{typeError} {@error}", typeError, error);
            }

            context.Response.ContentType = Constants.ContentTypeJson;
            context.Response.StatusCode = statusCode;

            if (_env.IsDevelopment())
            {
                error.InternalException = exception;
            }

            return context.Response.WriteAsync(JsonConvert.SerializeObject(error, settings));
        }

    }
}
