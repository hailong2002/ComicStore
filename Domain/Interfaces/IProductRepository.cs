using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace Application
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts(string? categoryName, string? name);
        Product GetProduct(int id);
        void AddProduct(Product product);
        void EditProduct(Product product);
        void DeleteProduct(int id);
        List<Product> GetRelatedProducts(string categoryName, int id);
        bool ProductExists(int id);

        SelectList GetCategorySelectList(int? id);
        SelectList GetSupplierSelectList(int? id);



    }
}
