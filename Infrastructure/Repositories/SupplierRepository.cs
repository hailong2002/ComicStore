using Application;
using Domain.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ApplicationDbContext _context;

        public SupplierRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Supplier> GetAllSuppliers(string? searchText)
        {
            if(!string.IsNullOrEmpty(searchText))
                return _context.Suppliers.Where(s=>s.SupplierName.Contains(searchText) || s.Address.Contains(searchText)).ToList();
            return _context.Suppliers.ToList();
        }

        public Supplier GetSupplier(int id)
        {
            return _context.Suppliers.Find(id);
        }
        public void AddSupplier(Supplier supplier)
        {
            _context.Add(supplier);
            _context.SaveChanges();
        }

        public void DeleteSupplier(int id)
        {
            var supplier = _context.Suppliers.Find(id);
            _context.Remove(supplier);
            _context.SaveChanges();
        }

        public void EditSupplier(Supplier supplier)
        {
            _context.Update(supplier);
            _context.SaveChanges();
        }

        public bool SupplierExists(int id)
        {
            return (_context.Suppliers?.Any(e => e.SupplierId == id)).GetValueOrDefault();
        }


    }
}
