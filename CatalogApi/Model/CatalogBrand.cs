using System.ComponentModel.DataAnnotations;

namespace CatalogApi.Model;

public class CatalogBrand
{
    public int Id { get; set; }

    [Required]
    public string? Brand { get; set; }
}