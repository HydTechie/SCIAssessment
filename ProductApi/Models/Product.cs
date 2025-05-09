namespace ProductApi.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public bool IsActive { get; set; }  

        public Product()
        {
            IsActive = true;
        }
    }
}
