using System.Text.Json;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ClothingStore.API.Health;

public static class HealthCheckResponseWriter
{
    public static Task WriteJsonResponse(HttpContext context, HealthReport report)
    {
        var environment = context.RequestServices.GetRequiredService<IHostEnvironment>();
        var isDevelopment = environment.IsDevelopment();

        context.Response.ContentType = "application/json";

        var payload = new
        {
            status = report.Status.ToString(),
            duration = report.TotalDuration,
            checks = report.Entries.Select(entry => new
            {
                name = entry.Key,
                status = entry.Value.Status.ToString(),
                description = entry.Value.Description,
                duration = entry.Value.Duration,
                error = isDevelopment ? entry.Value.Exception?.Message : null
            })
        };

        return context.Response.WriteAsync(
            JsonSerializer.Serialize(payload),
            context.RequestAborted);
    }
}
