namespace Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string? Image { get; set; }
        public string Brand { get; set; }
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public int SupplierId { get; set; }
        public virtual Supplier? Supplier { get; set; }


    }
}