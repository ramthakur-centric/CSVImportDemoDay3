namespace CsvImportDemo.Validators
{
    // Define IProductValidator with bool IsValid(Product p) and IEnumerable<string> Validate(Product p)
    public interface IProductValidator
    {
        bool IsValid(Models.Product p);
        System.Collections.Generic.IEnumerable<string> Validate(Models.Product p);
    }
}
