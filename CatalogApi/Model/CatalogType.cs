using System.ComponentModel.DataAnnotations;

namespace CatalogApi.Model;

public class CatalogType
{
    public int Id { get; set; }

    [Required]
    public string? Type { get; set; }
}