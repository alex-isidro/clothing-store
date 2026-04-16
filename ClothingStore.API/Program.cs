using ClothingStore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();
        
        builder.Services.AddDbContext<ClothingStoreContex>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
        });

        
        app.Run();
    }
}