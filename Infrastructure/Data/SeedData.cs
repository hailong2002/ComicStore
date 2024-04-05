using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Data
{
	public class SeedData
	{
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (!context.Suppliers.Any() && !context.Categories.Any())
                {
 
                    AddSampleCategories(context);
                    AddSampleSuppliers(context);
                    context.SaveChanges();

                    AddSampleProducts(context);
                    AddSampleUsers(context);
                    AddSampleRoles(context);
                    context.SaveChanges();

                    AddSampleUserRoles(context);
                    context.SaveChanges();

                }
            }
        }

        private static void AddSampleCategories(ApplicationDbContext context)
        {
            context.Categories.AddRange(
                new Category { CategoryName = "Comic" },
                new Category { CategoryName = "Keychain" },
                new Category { CategoryName = "Figure" }
            );
        }

        private static void AddSampleSuppliers(ApplicationDbContext context)
        {
            context.Suppliers.AddRange(
                new Supplier { SupplierName = "Supplier I", Address = "New York, US", Phone = "012346789" },
                new Supplier { SupplierName = "Supplier II", Address = "London, UK", Phone = "0987654321" }
            );
        }

        private static void AddSampleProducts(ApplicationDbContext context)
        {
            context.Products.AddRange(
                new Product
                {
                    Name = "Avenger: Secret War",
                    Brand = "Marvel",
                    Price = 100,
                    Image= "/images/p1.jfif",
                    Quantity = 50,
                    Description = "Marvel comic, it's about secret war of avenger.",
                    CategoryId = context.Categories.FirstOrDefault(c => c.CategoryName == "Comic").CategoryId,
                    SupplierId = context.Suppliers.FirstOrDefault(s => s.SupplierName == "Supplier I").SupplierId,
                },
                new Product
                {
                    Name = "Hulkbuster",
                    Brand = "Marvel",
                    Description = "Figure of hulkbuster",
                    Quantity = 50,
                    Image = "/images/p2.jfif",
                    Price = 100,
                    CategoryId = context.Categories.FirstOrDefault(c => c.CategoryName == "Figure").CategoryId,
                    SupplierId = context.Suppliers.FirstOrDefault(s => s.SupplierName == "Supplier I").SupplierId,
                },
                new Product
                {
                    Name = "Flashpoint",
                    Brand = "DC",
                    Description = "It's about flash backed to past and he changed future.",
                    Price = 80,
                    Image = "/images/p3.jfif",
                    Quantity = 50,
                    CategoryId = context.Categories.FirstOrDefault(c => c.CategoryName == "Comic").CategoryId,
                    SupplierId = context.Suppliers.FirstOrDefault(s => s.SupplierName == "Supplier I").SupplierId,
                },
                new Product
                {
                    Name = "Invincible",
                    Brand = "Other",
                    Quantity = 50,
                    Description = "It's about story of hero, his name is Invincible",
                    Price = 90,
                    Image = "/images/p4.png",
                    CategoryId = context.Categories.FirstOrDefault(c => c.CategoryName == "Comic").CategoryId,
                    SupplierId = context.Suppliers.FirstOrDefault(s => s.SupplierName == "Supplier II").SupplierId,
                },
                new Product
                {
                    Name = "Deadpool keychain",
                    Brand = "Marvel",
                    Description = "Keychain with deadpool face",
                    Price = 90,
                    Quantity = 50,
                    Image = "/images/p5.jfif",
                    CategoryId = context.Categories.FirstOrDefault(c => c.CategoryName == "Keychain").CategoryId,
                    SupplierId = context.Suppliers.FirstOrDefault(s => s.SupplierName == "Supplier II").SupplierId,
                }
            );
        }

        private static void AddSampleUsers(ApplicationDbContext context)
        {
            var hasher = new PasswordHasher<IdentityUser>();

            var adminUser = new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                PhoneNumber = "0132667894",
                NormalizedUserName = "admin@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "123456");

            var managerUser = new IdentityUser
            {
                UserName = "manager@gmail.com",
                Email = "manager@gmail.com",
                PhoneNumber = "0987654321",
                NormalizedUserName = "manager@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true

            };
            managerUser.PasswordHash = hasher.HashPassword(managerUser, "123456");

            var customerUser = new IdentityUser
            {
                UserName = "customer@gmail.com",
                Email = "customer@gmail.com",
                PhoneNumber = "0999456789",
                NormalizedUserName = "customer@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true

            };
            customerUser.PasswordHash = hasher.HashPassword(customerUser, "123456");

            context.Users.AddRange(adminUser, managerUser, customerUser);

        }

        private static void AddSampleRoles(ApplicationDbContext context)
        {
            context.Roles.AddRange(
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin"
                },

                new IdentityRole
                {
                    Name = "Manager",
                    NormalizedName = "Manager"
                },

                new IdentityRole
                {
                    Name = "Customer",
                    NormalizedName = "Customer"
                }

            );
        }

        private static void AddSampleUserRoles(ApplicationDbContext context)
        {
            context.UserRoles.AddRange(
                new IdentityUserRole<string>
                {
                    UserId = context.Users.Where(u => u.UserName == "admin@gmail.com").First().Id,
                    RoleId = context.Roles.Where(r=>r.Name == "Admin").First().Id
                },

                new IdentityUserRole<string>
                {
                    UserId = context.Users.Where(u => u.UserName == "manager@gmail.com").First().Id,
                    RoleId = context.Roles.Where(r => r.Name == "Manager").First().Id
                },

                new IdentityUserRole<string>
                {
                    UserId = context.Users.Where(u => u.UserName == "customer@gmail.com").First().Id,
                    RoleId = context.Roles.Where(r => r.Name == "Customer").First().Id
                }

            ) ;
        }



    }
}
