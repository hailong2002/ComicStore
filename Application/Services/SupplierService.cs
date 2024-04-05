using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _repository;

        public SupplierService(ISupplierRepository repository)
        {
            _repository = repository;
        }

        public List<Supplier> GetAllSuppliers(string? searchText)
        {
            return _repository.GetAllSuppliers(searchText);
        }

        public Supplier GetSupplier(int id)
        {
            return _repository.GetSupplier(id); 
        }

        public void AddSupplier(Supplier supplier)
        {
            _repository.AddSupplier(supplier);
        }

        public void EditSupplier(Supplier supplier)
        {
            _repository.EditSupplier(supplier);
        }

        public void DeleteSupplier(int id) 
        {
            _repository.DeleteSupplier(id);
        }

        public bool SupplierExists(int id)
        {
            return _repository.SupplierExists(id);
        }
    }
}
