using ComicStore.Models;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace ComicStore.Controllers
{
    public class ClientController : Controller
    {
        private readonly IProductService _service;

        public ClientController(IProductService service)
        {
            _service = service;
        }

        public ProductViewModel GetProductViewModel(Product product)
        {
            ProductViewModel productViewModel = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Brand = product.Brand,
                Price = product.Price,
                Description = product.Description,
                Quantity = product.Quantity,
                CategoryId = product.CategoryId,
                Category = product.Category,
                SupplierId = product.SupplierId,
                Supplier = product.Supplier,
                ImagePath = product.Image
            };
            return productViewModel;

        }

        public List<ProductViewModel> GetListProductViewModels(List<Product> products)
        {
            List<ProductViewModel> listProducts = new List<ProductViewModel>();
            foreach (var product in products)
            {
                listProducts.Add(GetProductViewModel(product));
            }
            return listProducts;
        }

        public IActionResult Index()
        {
            List<ProductViewModel> listProducts = new List<ProductViewModel>();
            listProducts = GetListProductViewModels(_service.GetAllProducts(null, null, null));
            return View(listProducts.Take(4));
        }


        public IActionResult Store(string? cateName, int? page, string searchName, string? sortType, double? minPrice, double? maxPrice)
        {
            List<ProductViewModel> listProducts = new List<ProductViewModel>();
            if (minPrice != null || maxPrice !=null)
            {
                listProducts = GetListProductViewModels(_service.SortProductByPrice(minPrice, maxPrice, cateName, searchName));
                ViewData["TotalPages"] = (int)Math.Ceiling((double)listProducts.Count() / 9);
                ViewData["CurrentPage"] = page != null ? page : 1;
                ViewData["SearchName"] = searchName;
                if (cateName != null) ViewData["CateName"] = cateName;
                return View(listProducts.ToPagedList(page ?? 1, 9));
            }
            listProducts = GetListProductViewModels(_service.GetAllProducts(cateName, sortType, searchName));
            ViewData["TotalPages"] = (int)Math.Ceiling((double)listProducts.Count() / 9);
            ViewData["CurrentPage"] = page != null ? page : 1;
            ViewData["SearchName"] = searchName;
            if (cateName != null) ViewData["CateName"] = cateName;
            if (sortType != null) ViewData["SortType"] = sortType;
            if (page == null || page == 0) page = 1;
            return View(listProducts.ToPagedList(page ?? 1, 9));
        }

        public IActionResult ProductDetails(int id)
        {
            var product = GetProductViewModel(_service.GetProduct(id));
            if (product == null) return NotFound();
            var relatedProduct = GetListProductViewModels(_service.GetRelatedProducts(product.Category.CategoryName, id));
            ViewData["RelatedProducts"] = relatedProduct;
            return View(product);
        }

        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
