using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
		public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			base.OnModelCreating(modelBuilder);

		}

        //public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        //{
        //    public ApplicationDbContext CreateDbContext(string[] args)
        //    {
        //        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        //        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ComicStore;Trusted_Connection=True;MultipleActiveResultSets=true");

        //        return new ApplicationDbContext(optionsBuilder.Options);
        //    }
        //}


    }
}