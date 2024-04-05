using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Data;
using Application;
using ComicStore.Models;
using Microsoft.AspNetCore.Authorization;
using X.PagedList;

namespace ComicStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        public Category GetCategoryEntity(CategoryViewModel category)
        {
            Category categoryEntity = new Category()
            {
                CategoryName = category.CategoryName,
            };
            return categoryEntity;
        }

        public CategoryViewModel GetCategoryViewModel(Category category)
        {
            CategoryViewModel categoryViewModel = new CategoryViewModel
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Products = category.Products
            };
            return categoryViewModel;
        }

        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Index(int? page)
        {
            var categories = _service.GetAllCategories();
            List<CategoryViewModel> categoriesViewModels = new List<CategoryViewModel>();
            foreach(var category in categories)
            {
                categoriesViewModels.Add(GetCategoryViewModel(category));
            }
            ViewBag.CurrentPage = page;
            return View(categoriesViewModels.ToPagedList(page ?? 1, 5));
        }

        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Details(int id)
        {
            var category = _service.GetCategory(id);
            return View(GetCategoryViewModel(category));
        }

        [Authorize(Roles = "Manager")]
        public IActionResult Create()
        {
            return View(new CategoryViewModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public IActionResult Create(CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                _service.AddCategory(GetCategoryEntity(category));
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [Authorize(Roles = "Manager")]
        public IActionResult Edit(int id)
        {
            var category = _service.GetCategory(id);
            if (category == null)return NotFound();
            return View(GetCategoryViewModel(category));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public IActionResult Edit(int id, CategoryViewModel category)
        {
            if (id != category.CategoryId) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    var categoryEntity = _service.GetCategory(id);
                    categoryEntity.CategoryName = category.CategoryName;
                    categoryEntity.Products = category.Products;
                    _service.EditCategory(categoryEntity);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [Authorize(Roles = "Manager")]
        public IActionResult Delete(int id)
        {
            var category = _service.GetCategory(id);
            if (category == null) return NotFound();
            return View(GetCategoryViewModel(category));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = _service.GetCategory(id);
            if (category == null) return NotFound();

            if (category.Products.Any())
            {
                ViewData["Status"] = "Category have products.";
                return View(GetCategoryViewModel(category));  
            }
            _service.DeleteCategory(id);
            return RedirectToAction(nameof(Index));

        }


        private bool CategoryExists(int id)
        {
            return _service.CategoryExists(id);
        }
    }
}
