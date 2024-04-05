using Application.Services;
using Application;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Infrastructure.Repositories;
using System.Configuration;
using Microsoft.Extensions.Options;
using Domain.Interfaces;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();


            //add _service
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();

            builder.Services.AddScoped<ISupplierService, SupplierService>();
            builder.Services.AddTransient<ISupplierRepository, SupplierRepository>();

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddTransient<IProductRepository, ProductRepository>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();


            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
           
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            //password
            builder.Services.AddDefaultIdentity<IdentityUser>(
                options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredUniqueChars = 1;
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = false;
                 
                }
            
            ).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();



			var app = builder.Build();

			using (var scope = app.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				SeedData.Initialize(services);
			}


			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
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
                pattern: "{controller=Client}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}