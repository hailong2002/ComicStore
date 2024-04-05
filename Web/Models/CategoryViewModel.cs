using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ComicStore.Models
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }

        [StringLength(100, MinimumLength = 1, ErrorMessage = "Name cannot be empty and shorter than 100 characters")]
        public string CategoryName { get; set; }
        public virtual ICollection<Product>? Products { get; set; }


    }
}
