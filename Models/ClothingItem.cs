// ClothingItem.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ClothingItem
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Type { get; set; }

    [Required]
    public string Size { get; set; }

    [Required]
    public string Color { get; set; }

    [Required]
    public int Stock { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }


    [Required]
    public string Gender { get; set; } // "Men", "Women", "Unisex".

    public string Description { get; set; }
    public string ImageUrl { get; set; } 
}
