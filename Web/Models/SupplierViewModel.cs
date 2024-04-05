using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ComicStore.Models
{
    public class SupplierViewModel
    {
        public int SupplierId { get; set; }

        [StringLength(100, MinimumLength = 1, ErrorMessage = "Name cannot be empty and shorter than 100 characters")]
        public string SupplierName { get; set; }

        [StringLength(100, MinimumLength = 1, ErrorMessage = "Address cannot be empty and shorter than 100 characters")]
        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(10, ErrorMessage = "Phone number must have 10 characters")]
        public string Phone { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}
