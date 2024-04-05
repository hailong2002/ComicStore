using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
		public virtual ICollection<Product>? Products { get; set; }

	}
}
