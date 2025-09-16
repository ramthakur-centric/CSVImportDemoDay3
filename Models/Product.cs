namespace CsvImportDemo.Models
{
    // Create a simple Product POCO with Id (int), Name (string), Price (decimal), Sku (string), and CreatedAt (DateTime)
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Sku { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
