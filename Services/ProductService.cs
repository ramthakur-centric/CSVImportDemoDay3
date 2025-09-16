using System.Collections.Generic;
using CsvImportDemo.Repositories;
using CsvImportDemo.Validators; // Add this if IProductValidator is in Validators namespace

namespace CsvImportDemo.Services
{
    // Implement ImportProductsFromCsv using CsvHelper: parse CSV into Product objects, call IProductValidator.Validate, and call IProductRepository.AddAsync for valid items, then SaveChangesAsync. Return count of saved records.
    public class ProductService: IProductService
    {
        private readonly IProductValidator _validator;
        private readonly IProductRepository _repository;

        public ProductService(IProductValidator validator, IProductRepository repository)
        {
            _validator = validator;
            _repository = repository;
        }

        public async System.Threading.Tasks.Task ImportProductsFromCsv(Microsoft.AspNetCore.Http.IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new System.ArgumentException("File is empty");

            using var reader = new System.IO.StreamReader(file.OpenReadStream());
            using var csv = new CsvHelper.CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            });

            var products = csv.GetRecords<Models.Product>();
            var validProducts = new List<Models.Product>();

            foreach (var product in products)
            {
                var errors = _validator.Validate(product);
                if (errors == null || !System.Linq.Enumerable.Any(errors))
                {
                    validProducts.Add(product);
                    await _repository.AddAsync(product);
                }
                else
                {
                    // Log or handle validation errors as needed
                }
            }

            await _repository.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task<int> ImportProductsFromCsvAndReturnCount(Microsoft.AspNetCore.Http.IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new System.ArgumentException("File is empty");

            using var reader = new System.IO.StreamReader(file.OpenReadStream());
            using var csv = new CsvHelper.CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            });

            var products = csv.GetRecords<Models.Product>();
            var validProducts = new List<Models.Product>();

            foreach (var product in products)
            {
                var errors = _validator.Validate(product);
                if (errors == null || !System.Linq.Enumerable.Any(errors))
                {
                    validProducts.Add(product);
                    await _repository.AddAsync(product);
                }
                else
                {
                    // Log or handle validation errors as needed
                }
            }

            await _repository.SaveChangesAsync();
            return validProducts.Count;
        }
    }
}
