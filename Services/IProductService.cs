namespace CsvImportDemo.Services
{
    // Define IProductService with Task ImportProductsFromCsv(IFormFile file) and Task<int> ImportProductsFromCsvAndReturnCount(IFormFile file)
    public interface IProductService
    {
        System.Threading.Tasks.Task ImportProductsFromCsv(Microsoft.AspNetCore.Http.IFormFile file);
        System.Threading.Tasks.Task<int> ImportProductsFromCsvAndReturnCount(Microsoft.AspNetCore.Http.IFormFile file);
    }
}
