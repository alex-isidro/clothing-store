using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ClothingStore.API.Extensions;

public sealed class ConfigureSwaggerOptions(
    IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(
                description.GroupName,
                new OpenApiInfo
                {
                    Title = "Clothing Store API",
                    Version = description.ApiVersion.ToString(),
                    Description = description.IsDeprecated
                        ? "Esta versão está deprecada. Use a versão 2.0."
                        : "API REST para gerenciamento da Clothing Store."
                });
        }
    }
}
