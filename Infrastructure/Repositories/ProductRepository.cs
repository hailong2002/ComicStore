using Application;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Product> GetAllProducts(string? categoryName, string? name)
        {
            if (!string.IsNullOrWhiteSpace(categoryName))
                return _context.Products.Where(p => p.Category.CategoryName == categoryName).ToList();
            if (!string.IsNullOrWhiteSpace(name))
                return _context.Products.Where(p => p.Name.Contains(name) || p.Category.CategoryName.Contains(name) || p.Brand.Contains(name)).ToList();    
            return _context.Products.ToList();
		}

        public List<Product> GetRelatedProducts(string categoryName, int id)
        {
            return _context.Products.Where(p => p.Category.CategoryName == categoryName && p.Id != id).Take(4).ToList();
        }


		public Product GetProduct(int id)
		{
	    	return _context.Products.Find(id);
		}
		public void AddProduct(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
        }


        public void EditProduct(Product product)
        {
            _context.Update(product);
			_context.SaveChanges();
		}

		public void DeleteProduct(int id)
		{
			_context.Products.Remove(_context.Products.Find(id));
            _context.SaveChanges();
		}


        public bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public SelectList GetCategorySelectList(int? id)
        {
            return new SelectList(_context.Categories, "CategoryId", "CategoryName", id);

        }

        public SelectList GetSupplierSelectList(int? id)
        {
            return new SelectList(_context.Suppliers, "SupplierId", "SupplierName", id);
        }
    }
}
