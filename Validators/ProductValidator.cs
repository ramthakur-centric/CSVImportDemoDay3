namespace CsvImportDemo.Validators
{
    // Implement basic checks: Name not null/empty, Price > 0, SKU not null and unique-check left for service/db
    public class ProductValidator : IProductValidator
    {
        public bool IsValid(Models.Product p)
        {
            return !string.IsNullOrWhiteSpace(p.Name) && p.Price > 0 && !string.IsNullOrWhiteSpace(p.Sku);
        }

        public IEnumerable<string> Validate(Models.Product p)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(p.Name))
            {
                errors.Add("Product name is required.");
            }
            if (p.Price <= 0)
            {
                errors.Add("Product price must be positive.");
            }
            if (string.IsNullOrWhiteSpace(p.Sku))
            {
                errors.Add("Product SKU is required.");
            }
            return errors;
        }
    }
}
