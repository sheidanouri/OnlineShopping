// ShoppingCart.cs
public class ShoppingCart
{
    public int Id { get; set; }

    public int ClothingItemId { get; set; }
    public ClothingItem ClothingItem { get; set; }

    public int Quantity { get; set; }

    public decimal TotalPrice => ClothingItem?.Price * Quantity ?? 0;
}
