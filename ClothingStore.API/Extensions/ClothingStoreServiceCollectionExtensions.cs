using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Application.Interfaces.Services;
using ClothingStore.Application.Services;
using ClothingStore.Infrastructure.Persistence;
using ClothingStore.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ClothingStore.API.Extensions;

/// <summary>
/// Extensões para registrar persistência, repositórios, serviços de aplicação e health checks.
/// </summary>
public static class ClothingStoreServiceCollectionExtensions
{
    public static IServiceCollection AddClothingStoreDbContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres");

        if (string.IsNullOrWhiteSpace(connectionString) ||
            connectionString.Contains("__SET_IN_USER_SECRETS_OR_ENV__", StringComparison.Ordinal))
        {
            throw new InvalidOperationException(
                "Configure ConnectionStrings:Postgres via User Secrets ou variável de ambiente.");
        }

        services.AddDbContext<ClothingStoreContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }

    public static IServiceCollection AddClothingStoreRepositories(
        this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IPedidoRepository, PedidoRepository>();
        services.AddScoped<IProdutoRepository, ProdutoRepository>();

        return services;
    }

    public static IServiceCollection AddClothingStoreApplicationServices(
        this IServiceCollection services)
    {
        services.AddScoped<IProdutoService, ProdutoService>();

        return services;
    }

    public static IServiceCollection AddClothingStoreHealthChecks(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres");

        services
            .AddHealthChecks()
            .AddCheck(
                "self",
                () => HealthCheckResult.Healthy("API em execução."))
            .AddDbContextCheck<ClothingStoreContext>(
                "database",
                tags: ["db", "postgres"]);

        return services;
    }
}
