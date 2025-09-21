namespace WEB_353504_Antonava_Lab3.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; } 
        public string Name { get; set; } = null!; 
        public string NormalizedName { get; set; } = null!;

        public ICollection<Product> Products { get; set; } = new List<Product>(); 
    }
}
