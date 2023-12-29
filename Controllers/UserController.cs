using Microsoft.AspNetCore.Mvc;

public class UserController : Controller
{
    public IActionResult Index()
    {
        // Add logic to retrieve and display user information
        return View();
    }
}