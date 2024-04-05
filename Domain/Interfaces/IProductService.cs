using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProductService
    {
        List<Product> GetAllProducts(string? categoryName, string? sortType, string? name);
        Product GetProduct(int id);
		void AddProduct(Product product);
		void EditProduct(Product product);
		void DeleteProduct(int id);
        int GetPage(string categoryName);
        bool ProductExists(int id);
        List<Product> SortProducts(string? sortType, List<Product> products);
        List<Product> SortProductByPrice(double? minPrice, double? maxPrice, string? cateName, string? name);
        List<Product> GetRelatedProducts(string categoryName, int id);
        SelectList GetCategorySelectList(int? id);
        SelectList GetSupplierSelectList(int? id);


    }
}
