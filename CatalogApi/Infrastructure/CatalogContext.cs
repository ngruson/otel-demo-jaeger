using CatalogApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CatalogApi.Infrastructure;

public class CatalogContext(
        DbContextOptions<CatalogContext> options
) : DbContext(options)
{
    public DbSet<CatalogItem> CatalogItems { get; set; }
    public DbSet<CatalogBrand> CatalogBrands { get; set; }
    public DbSet<CatalogType> CatalogTypes { get; set; }
}