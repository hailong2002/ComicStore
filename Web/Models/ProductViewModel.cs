using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ComicStore.Models
{
    public class ProductViewModel
    {
      
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be longer than 3 and shorter than 100 characters")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description is too long!!!")]
        public string Description { get; set; }

        [Range(1, 10000, ErrorMessage = "Price must be between 1 to 10000")]
        public double Price { get; set; }

        [Range(1, 9999, ErrorMessage = "Quantity must be between 1 and 9999")]
        public int Quantity { get; set; }

        //[Required(ErrorMessage ="You must upload an image file!")]
        [ValidateNever]
        public IFormFile Image { get; set; }
        public string? ImagePath { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual Category? Category { get; set; }

        [Required]
        public int SupplierId { get; set; }

        public virtual Supplier? Supplier { get; set; }
    }
}
