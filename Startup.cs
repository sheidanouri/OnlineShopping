using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LabFinal;

namespace LabFinal
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContextPool<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")),
                poolSize: 1);


        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext dbContext)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
//                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            ConfigureEndpoints(app);

            // Ensure the database is created and migrated
            dbContext.Database.Migrate();
            
        }

        private void ConfigureEndpoints(IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                // Default route
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // Other routes
                endpoints.MapControllerRoute(
                    name: "selectUser",
                    pattern: "select-user",
                    defaults: new { controller = "Home", action = "SelectUser" });

                endpoints.MapControllerRoute(
                    name: "clothDetail",
                    pattern: "cloths/{id:int}",
                    defaults: new { controller = "Home", action = "Detail" });

                endpoints.MapControllerRoute(
                    name: "addToCart",
                    pattern: "add-to-cart/{id:int}",
                    defaults: new { controller = "Home", action = "AddToCart" });

                endpoints.MapControllerRoute(
                    name: "removeFromCart",
                    pattern: "remove-from-cart/{id:int}",
                    defaults: new { controller = "Home", action = "RemoveFromCart" });

                endpoints.MapControllerRoute(
                    name: "checkout",
                    pattern: "checkout",
                    defaults: new { controller = "Home", action = "Checkout" });

                endpoints.MapControllerRoute(
                    name: "payment",
                    pattern: "payment",
                    defaults: new { controller = "Home", action = "Payment" });

                endpoints.MapControllerRoute(
                    name: "confirmPayment",
                    pattern: "confirm-payment",
                    defaults: new { controller = "Home", action = "ConfirmPayment" });
            });
        }

    }

}