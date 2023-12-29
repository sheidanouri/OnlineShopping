using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using LabFinal; 

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add database context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public static class DatabaseInitializer
{
    public static void Initialize(AppDbContext context)
    {
        context.Database.EnsureCreated();

        // Check if there are already users in the database
        if (!context.Users.Any())
        {
            // Add sample users
            var users = new List<User>
            {
                new User { Username = "user1", Password = "password1" },
                new User { Username = "user2", Password = "password2" },
                new User { Username = "user3", Password = "password3" }
            };
            context.Users.AddRange(users);
            context.SaveChanges();
        }

        // Check if there are already clothing items in the database
        if (!context.ClothingItems.Any())
        {
            var clothingItems = new List<ClothingItem>
            {
                // Clothing
                new ClothingItem { Name = "Blue Shirt", Type = "Top", Color = "Blue", Size = "M", Price = 19.99m, Stock = 50, Gender = "Men", Description = "Casual blue shirt for men.", ImageUrl = "/images/blue-shirt-erkek.jpg" },
                new ClothingItem { Name = "Blue Shirt", Type = "Top", Color = "Blue", Size = "M", Price = 19.99m, Stock = 50, Gender = "Men", Description = "Casual blue shirt for women.", ImageUrl = "/images/blue-shirt-kadin.jpg" },

                new ClothingItem { Name = "Black Jeans", Type = "Bottom", Color = "Black", Size = "32", Price = 29.99m, Stock = 30, Gender = "Men", Description = "Classic black jeans for men.", ImageUrl = "/images/black-jeans-erkek.jpg" },
                new ClothingItem { Name = "Black Jeans", Type = "Bottom", Color = "Black", Size = "32", Price = 29.99m, Stock = 30, Gender = "Women", Description = "Classic black jeans for women.", ImageUrl = "/images/black-jeans-kadin.jpg" },

                new ClothingItem { Name = "Black Dress", Type = "Top", Color = "Black", Size = "32", Price = 29.99m, Stock = 30, Gender = "Unisex", Description = "Classic black jeans.", ImageUrl = "/images/black-dress-unisex.jpg" },

                new ClothingItem { Name = "Red Dress", Type = "Dress", Color = "Red", Size = "S", Price = 39.99m, Stock = 20, Gender = "Women", Description = "Elegant red dress for women.", ImageUrl = "/images/red-dress-kadin.jpg" },
                new ClothingItem { Name = "Red Dress", Type = "Dress", Color = "Red", Size = "S", Price = 39.99m, Stock = 20, Gender = "Men", Description = "Elegant red dress for men.", ImageUrl = "/images/red-dress-erkek.jpeg" },

                // Shoes
                new ClothingItem { Name = "Running Shoes", Type = "Shoes", Color = "White", Size = "10", Price = 49.99m, Stock = 25, Gender = "Unisex", Description = "Comfortable white running shoes.", ImageUrl = "/images/running-shoes.jpg" },
                new ClothingItem { Name = "High Heels", Type = "Shoes", Color = "Black", Size = "8", Price = 59.99m, Stock = 15, Gender = "Women", Description = "Stylish black high heels for women.", ImageUrl = "/images/high-heels.jpg" },
                // ... add more shoes

                // Hats
                new ClothingItem { Name = "Winter Hat", Type = "Hat", Color = "Gray", Size = "One Size", Price = 14.99m, Stock = 40, Gender = "Unisex", Description = "Warm gray winter hat.", ImageUrl = "/images/winter-hat.jpg" },
                new ClothingItem { Name = "Baseball Cap", Type = "Hat", Color = "Red", Size = "One Size", Price = 9.99m, Stock = 50, Gender = "Unisex", Description = "Casual red baseball cap.", ImageUrl = "/images/baseball-cap.jpg" },
                // ... add more hats

                // Scarves
                new ClothingItem { Name = "Silk Scarf", Type = "Scarf", Color = "Pink", Size = "One Size", Price = 24.99m, Stock = 35, Gender = "Women", Description = "Soft pink silk scarf for women.", ImageUrl = "/images/silk-scarf.jpg" },
                new ClothingItem { Name = "Wool Scarf", Type = "Scarf", Color = "Green", Size = "One Size", Price = 29.99m, Stock = 30, Gender = "Unisex", Description = "Warm green wool scarf.", ImageUrl = "/images/wool-scarf.jpg" },
            
            };           
            context.ClothingItems.AddRange(clothingItems);
            context.SaveChanges();
        }

    }
}
