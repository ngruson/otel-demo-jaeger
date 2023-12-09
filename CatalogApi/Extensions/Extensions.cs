using CatalogApi.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CatalogApi.Extensions;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {

        if (builder.Environment.IsDevelopment())
            builder.Services.AddDbContext<CatalogContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("CatalogDb")));
        else 
            builder.Services.AddDbContext<CatalogContext>(
                options => options.UseSqlServer(Environment.GetEnvironmentVariable("CONNECTIONSTRINGS__CATALOGDB")));

        builder.Services.AddMigration<CatalogContext, CatalogContextSeed>();

        builder.Services.AddOptions<CatalogOptions>()
            .BindConfiguration(nameof(CatalogOptions));
    }
}