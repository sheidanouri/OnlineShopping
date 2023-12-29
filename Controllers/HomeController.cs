using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LabFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace LabFinal.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
        DatabaseInitializer.Initialize(_context);

    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    [HttpPost]
    public IActionResult SelectUser(UserSelectionViewModel viewModel)
    {
        // Logic to handle user selection
        // Access selected user ID using viewModel.SelectedUserId
        return RedirectToAction("Index");
    }


    public IActionResult Users()
    {
        var users = _context.Users.ToList();
        return View(users);
    }

    [HttpPost]
    public IActionResult SelectUser(int userId)
    {
        // Logic for handling user selection (e.g., set user in session)
        return RedirectToAction("Index");
    }

    public IActionResult Detail(int id)
    {
        var clothingItem = _context.ClothingItems.Find(id);
        return View(clothingItem);
    }

    [HttpPost]
    public IActionResult AddToCart(int id, int quantity)
    {
        var clothingItem = _context.ClothingItems.Find(id);

        if (clothingItem == null)
        {
            // Handle the case where the item with the given id is not found
            return RedirectToAction("Index");
        }

        // Create a new shopping cart item
        var shoppingCartItem = new ShoppingCart
        {
            ClothingItem = clothingItem,
            Quantity = quantity
        };

        // Add the item to the shopping cart
        _context.ShoppingCarts.Add(shoppingCartItem);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult Payment()
    {
        // Logic for displaying payment page
        var shoppingCart = _context.ShoppingCarts.Include(sc => sc.ClothingItem).ToList();
        return View(shoppingCart);
    }

    public IActionResult ConfirmPayment()
    {
        // Logic for confirming payment
        return RedirectToAction("Index");
    }    

    public IActionResult Index(UserSelectionViewModel viewModel)
    {
        var query = _context.ClothingItems.AsQueryable();

        // Apply filters
        if (!string.IsNullOrEmpty(viewModel.NameFilter))
        {
            query = query.Where(c => c.Name.Contains(viewModel.NameFilter));
        }

        if (!string.IsNullOrEmpty(viewModel.SizeFilter))
        {
            query = query.Where(c => c.Size.Contains(viewModel.SizeFilter));
        }

        if (!string.IsNullOrEmpty(viewModel.ColorFilter))
        {
            query = query.Where(c => c.Color.Contains(viewModel.ColorFilter));
        }

        // Other filter criteria as needed...

        // Get the filtered data
        viewModel.ClothingItems = query.ToList();

        return View(viewModel);
    }

    [HttpPost]


    [HttpGet]
    public IActionResult ShoppingCarts()
    {
        var shoppingCarts = _context.ShoppingCarts.Include(sc => sc.ClothingItem).ToList();
        return View(shoppingCarts);
    }

   public IActionResult Checkout()
    {
        // Get the shopping cart items
        var shoppingCarts = _context.ShoppingCarts.Include(sc => sc.ClothingItem).ToList();

        // Calculate total price
        decimal total = shoppingCarts.Sum(sc => sc.ClothingItem.Price * sc.Quantity);

        // Pass the shopping carts and total price to the view
        ViewBag.ShoppingCarts = shoppingCarts;
        ViewBag.TotalPrice = total;

        return View();
    }

    [HttpPost]
    public IActionResult RemoveFromCart(int id)
    {
        var shoppingCart = _context.ShoppingCarts.Find(id);

        if (shoppingCart != null)
        {
            _context.ShoppingCarts.Remove(shoppingCart);
            _context.SaveChanges();
        }

        return RedirectToAction("ShoppingCarts");
    }
}
