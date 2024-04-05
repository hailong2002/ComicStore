﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface ISupplierRepository
    {
        List<Supplier> GetAllSuppliers(string? searchText);
        Supplier GetSupplier(int id);
        void AddSupplier(Supplier supplier);
        void EditSupplier(Supplier supplier);
        void DeleteSupplier(int id);
        bool SupplierExists(int id);

    }
}
