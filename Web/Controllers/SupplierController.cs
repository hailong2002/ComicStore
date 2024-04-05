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
    public class SupplierController : Controller
    {
        private readonly ISupplierService _service;

        public SupplierController(ISupplierService service)
        {
            _service = service;
        }

        public Supplier GetSupplierEntity(SupplierViewModel supplier)
        {
            Supplier supplierEntity = new Supplier()
            {
                SupplierName = supplier.SupplierName,
                Address = supplier.Address,
                Phone = supplier.Phone
            };
            return supplierEntity;
        }

        public SupplierViewModel GetSupplierViewModel(Supplier supplier)
        {
            var supplierViewModel = new SupplierViewModel()
            {
                SupplierId = supplier.SupplierId,
                SupplierName = supplier.SupplierName,
                Address = supplier.Address,
                Phone = supplier.Phone,
                Products = supplier.Products
            };
            return supplierViewModel;
        }

        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Index(int? page, string? searchText)
        {
            var suppliers = _service.GetAllSuppliers(searchText);
            List<SupplierViewModel> supplierViewModels = new List<SupplierViewModel>();
            foreach(var supplier in suppliers)
            {
                supplierViewModels.Add(GetSupplierViewModel(supplier));
            }
            ViewBag.CurrentPage = page;
            return View(supplierViewModels.ToPagedList(page ?? 1, 5));
        }

        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Details(int id)
        {
            var supplier = _service.GetSupplier(id);
            return View(GetSupplierViewModel(supplier));
        }

        [Authorize(Roles = "Manager")]
        public IActionResult Create()
        {
            return View(new SupplierViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public IActionResult Create( SupplierViewModel supplier)
        {
            if (ModelState.IsValid)
            {
                _service.AddSupplier(GetSupplierEntity(supplier));
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        [Authorize(Roles = "Manager")]
        public IActionResult Edit(int id)
        {
            var supplier = _service.GetSupplier(id);
            if (supplier == null)return NotFound();
            return View(GetSupplierViewModel(supplier));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public IActionResult Edit(int id, SupplierViewModel supplier)
        {
            if (id != supplier.SupplierId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var supplierEntity = _service.GetSupplier(id);
                    supplierEntity.SupplierName = supplier.SupplierName;
                    supplierEntity.Address = supplier.Address;
                    supplierEntity.Phone = supplier.Phone;
                    supplier.Products = supplier.Products;
                    _service.EditSupplier(supplierEntity);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierExists(supplier.SupplierId))
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
            return View(supplier);
        }

        [Authorize(Roles = "Manager")]
        public IActionResult Delete(int id)
        {
            var supplier = _service.GetSupplier(id);
            if (supplier == null) return NotFound();
            return View(GetSupplierViewModel(supplier));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supplier = _service.GetSupplier(id);
            if (supplier == null) return NotFound();
            if (supplier.Products.Any())
            {
                ViewData["Status"] = "Supplier have products.";
                return View(GetSupplierViewModel(supplier));
            }
            _service.DeleteSupplier(id);
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierExists(int id)
        {
            return _service.SupplierExists(id);
        }
    }
}
