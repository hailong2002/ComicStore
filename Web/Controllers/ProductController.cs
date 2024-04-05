using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application;
using X.PagedList;
using ComicStore.Models;
using Domain.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ComicStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IProductService service, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _webHostEnvironment = webHostEnvironment;
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

        public string GetImagePath(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                return "/images/" + uniqueFileName;

            }
            return "";
        }

        public Product GetEntityProduct(ProductViewModel product)
        {
            string imagePath = GetImagePath(product.Image);

            Product entityProduct = new Product()
            {
                Name = product.Name,
                Brand = product.Brand,
                Price = product.Price,
                Description = product.Description,
                Quantity = product.Quantity,
                CategoryId = product.CategoryId,
                SupplierId = product.SupplierId,
                Image = imagePath
            };
            return entityProduct;
        }

        public List<ProductViewModel> GetListProductViewModels(List<Product> products)
        {
            List<ProductViewModel> listProducts = new List<ProductViewModel>();
            foreach(var product in products)
            {
                listProducts.Add(GetProductViewModel(product));
            }
            return listProducts;
        }


        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Index(string? cateName, int? page, string searchName, string? sortType)
        {
            List<ProductViewModel> listProducts = new List<ProductViewModel>();
            if (cateName != null) ViewData["CateName"] = cateName;
            if (sortType != null) ViewData["SortType"] = sortType;
            if (searchName != null) ViewData["SearchName"] = searchName;
            ViewBag.CurrentPage = page;
            listProducts = GetListProductViewModels(_service.GetAllProducts(cateName, sortType, searchName));
            if(page == null || page == 0) page = 1;
            return View(listProducts.ToPagedList(page ?? 1, 5));
        }

        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Details(int id, string? cateName, int? currentPage)
        {
            var product = _service.GetProduct(id);
            if (product == null) return NotFound();
            ViewData["CateName"] = cateName;
            ViewBag.CurrentPage = currentPage;
            return View(GetProductViewModel(product));
        }

        [Authorize(Roles = "Manager")]
        public IActionResult Create(string cateName)
        {
            ViewData["CategoryId"] = _service.GetCategorySelectList(null);
            ViewData["SupplierId"] = _service.GetSupplierSelectList(null);
            ViewData["CateName"] = cateName;
            return View(new ProductViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public IActionResult Create(ProductViewModel product, string? categoryName)
        {
            if (ModelState.IsValid)
            {
                if(product.Image != null)
                {
                    _service.AddProduct(GetEntityProduct(product));
                    int pageIndex = _service.GetPage(categoryName);
                    return RedirectToAction(nameof(Index), new { cateName = categoryName, page = pageIndex });
                }
                ViewData["Error"] = "Not uploaded file";
                return View(product);
            }
            ViewData["CategoryId"] = _service.GetCategorySelectList(null);
            ViewData["SupplierId"] = _service.GetSupplierSelectList(null);
            return View(product);
            
        }

        [Authorize(Roles = "Manager")]
        public IActionResult Edit(int id, int? currentPage, string? cateName)
        {
            var product = _service.GetProduct(id);
            if (product == null) return NotFound();
            ViewData["CategoryId"] = _service.GetCategorySelectList(id);
            ViewData["SupplierId"] = _service.GetSupplierSelectList(id);
            ViewData["CateName"] = cateName;
            ViewBag.CurrentPage = currentPage;
            return View(GetProductViewModel(product));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public IActionResult Edit(int id,ProductViewModel product, string? categoryName, int? page)
        {
            if (id != product.Id)  return NotFound();
            if (ModelState.IsValid)
            {   
                try
                {
                    var existProduct = _service.GetProduct(id);
                    var entityProduct = GetEntityProduct(product);
                    existProduct.Name = entityProduct.Name;
                    existProduct.Category = entityProduct.Category;
                    existProduct.CategoryId = entityProduct.CategoryId;
                    existProduct.Brand = entityProduct.Brand;
                    existProduct.Supplier = entityProduct.Supplier;
                    existProduct.SupplierId = entityProduct.SupplierId;
                    existProduct.Price = entityProduct.Price;
                    existProduct.Quantity = entityProduct.Quantity;
                    existProduct.Image = entityProduct.Image.Any() ? entityProduct.Image : product.ImagePath;
                    existProduct.Description = product.Description;

                    _service.EditProduct(existProduct);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_service.ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { cateName = categoryName, page=page});
            }
            ViewData["CategoryId"] = _service.GetCategorySelectList(id);
            ViewData["SupplierId"] = _service.GetSupplierSelectList(id);
            return View(product);
        }

        [Authorize(Roles = "Manager")]
        public IActionResult Delete(int id, int? currentPage, string? cateName)
        {
            var product = _service.GetProduct(id);
            if (product == null) return NotFound();
            var productViewModel = GetProductViewModel(product);
            ViewBag.CurrentPage = currentPage;
            ViewData["CateName"] = cateName;
            return View(productViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _service.GetProduct(id);
            string categoryName = product.Category.CategoryName;
            _service.DeleteProduct(id);
            int pageIndex = _service.GetPage(categoryName);
            return RedirectToAction(nameof(Index), new { cateName =  categoryName, page = pageIndex});
        }

    }
}
