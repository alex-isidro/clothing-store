using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Infrastructure.Persistence;
using ClothingStore.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("Postgres");

        if (string.IsNullOrWhiteSpace(connectionString) ||
            connectionString.Contains("__SET_IN_USER_SECRETS_OR_ENV__", StringComparison.Ordinal))
        {
            throw new InvalidOperationException(
                "Configure ConnectionStrings:Postgres via User Secrets ou variável de ambiente.");
        }

        // Add services to the container.
        builder.Services.AddDbContext<ClothingStoreContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
        builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
        builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
