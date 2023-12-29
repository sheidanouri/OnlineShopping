// ClothingItemRepository.cs
using System.Collections.Generic;
using System.Linq;

public class ClothingItemRepository
{
    private readonly AppDbContext _context;

    public ClothingItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<ClothingItem> GetAll()
    {
        return _context.ClothingItems.ToList();
    }

    // ... add other methods for interacting with the database
}

