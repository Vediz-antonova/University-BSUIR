namespace WEB_353504_Antonava_Lab3.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; } 
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int? CategoryId { get; set; } 
        public Category? Category { get; set; }
        public decimal Weight { get; set; }
        public string? ImagePath { get; set; } 
        public string? ImageMimeType { get; set; } 
    }
}
