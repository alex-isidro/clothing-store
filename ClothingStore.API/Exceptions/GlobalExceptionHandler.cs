using ClothingStore.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.API.Exceptions;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly IHostEnvironment _environment;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IHostEnvironment environment)
    {
        _logger = logger;
        _environment = environment;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var traceId = System.Diagnostics.Activity.Current?.Id ?? httpContext.TraceIdentifier;

        _logger.LogError(
            exception,
            "Erro não tratado na requisição {Method} {Path}. {TraceId}",
            httpContext.Request.Method,
            httpContext.Request.Path,
            traceId);

        var (statusCode, title, detail) = exception switch
        {
            ArgumentException => (StatusCodes.Status400BadRequest, "Requisição inválida", exception.Message),
            ResourceNotFoundException => (StatusCodes.Status404NotFound, "Recurso não encontrado", exception.Message),
            ConflictException => (StatusCodes.Status409Conflict, "Conflito de dados", exception.Message),
            DomainException => (StatusCodes.Status400BadRequest, "Erro de domínio", exception.Message),
            KeyNotFoundException => (StatusCodes.Status404NotFound, "Recurso não encontrado", exception.Message),
            InvalidOperationException => (StatusCodes.Status400BadRequest, "Operação inválida", exception.Message),
            _ => (StatusCodes.Status500InternalServerError, "Erro interno no servidor", GetInternalServerErrorDetail(exception))
        };

        var problemDetails = new ProblemDetails
        {
            Type = "about:blank",
            Status = statusCode,
            Title = title,
            Detail = detail,
            Instance = httpContext.Request.Path
        };

        if (_environment.IsDevelopment())
        {
            problemDetails.Extensions["traceId"] = traceId;
        }

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/problem+json";

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private string GetInternalServerErrorDetail(Exception exception)
    {
        if (_environment.IsDevelopment())
        {
            return exception.Message;
        }

        return "Ocorreu um erro inesperado. Tente novamente mais tarde.";
    }
}
