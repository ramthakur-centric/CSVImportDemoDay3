using Microsoft.AspNetCore.Mvc;

namespace CsvImportDemo.Controllers
{
    // Create API controller with POST /api/products/import-csv accepting IFormFile file, calling IProductService.ImportProductsFromCsvAndReturnCount(file), and returning 200 with saved count.
    [ApiController]
    [Route("api/[controller]")] 
    public class ProductsController : ControllerBase
    {
        private readonly Services.IProductService _productService;

        public ProductsController(Services.IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("import-csv")]
        public async System.Threading.Tasks.Task<IActionResult> ImportCsv(Microsoft.AspNetCore.Http.IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty");

            var savedCount = await _productService.ImportProductsFromCsvAndReturnCount(file);
            return Ok(new { SavedCount = savedCount });
        }
    }
}
