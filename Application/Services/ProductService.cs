using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public List<Product> SortProducts(string? sortType, List<Product> products)
        {
            var sortedProducts = products;
            if (sortType != null)
            {
                switch (sortType)
                {
                    case "priceIncrease":
                        sortedProducts = products.OrderBy(x => x.Price).ToList();
                        break;
                    case "priceDecrease":
                        sortedProducts = products.OrderByDescending(x => x.Price).ToList();
                        break;
                    case "nameIncrease":
                        sortedProducts = products.OrderBy(x => x.Name).ToList();
                        break;
                    case "nameDecrease":
                        sortedProducts = products.OrderByDescending(x => x.Name).ToList();
                        break;
                    default: break;
                }
            }
            return sortedProducts;
        }
        public List<Product> GetAllProducts(string? categoryName, string? sortType, string? name)
        {
            var products = _repository.GetAllProducts(categoryName, name);
            return SortProducts(sortType, products);
        }
      
        public Product GetProduct(int id)
        {
            return _repository.GetProduct(id);
        }

        public void AddProduct(Product product)
        {
            _repository.AddProduct(product);
        }

        public void EditProduct(Product product)
        {
            _repository.EditProduct(product);
        }

        public void DeleteProduct(int id)
        {
            _repository.DeleteProduct(id);
        }

        public List<Product> SortProductByPrice(double? minPrice, double? maxPrice, string? cateName, string? name)
        {
            var products = _repository.GetAllProducts(cateName, name);
            if (minPrice == null && maxPrice != null)
                return products.Where(p => p.Price <= maxPrice).ToList();
            if (minPrice != null && maxPrice == null)
                return products.Where(p => p.Price >= minPrice).ToList();
            return products.Where(p => p.Price <= maxPrice && p.Price >= minPrice).ToList();
        }

        public List<Product> GetRelatedProducts(string categoryName, int id)
        {
            return _repository.GetRelatedProducts(categoryName, id);
        }
        
        public bool ProductExists(int id)
        {
            return _repository.ProductExists(id);
        }

        public int GetPage(string? categoryName)
        {
            var products = _repository.GetAllProducts(categoryName, null).OrderBy(p => p.Id).ToList();
            int totalPages = (int)Math.Ceiling((double)products.Count() / 5);
            return totalPages;
        }

        public SelectList GetCategorySelectList(int? id)
        {
            return _repository.GetCategorySelectList(id);
        }

        public SelectList GetSupplierSelectList(int? id)
        {
            return _repository.GetSupplierSelectList(id);
        }
    }
}
