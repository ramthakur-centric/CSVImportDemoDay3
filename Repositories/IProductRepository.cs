using System.Collections.Generic;

namespace CsvImportDemo.Repositories
{
    public interface IProductRepository
    {
        // Define IProductRepository with Task AddAsync(Product p), Task<IEnumerable<Product>> GetAllAsync(), Task SaveChangesAsync()
        System.Threading.Tasks.Task AddAsync(Models.Product p);
        System.Threading.Tasks.Task<IEnumerable<Models.Product>> GetAllAsync();
        System.Threading.Tasks.Task SaveChangesAsync();
    }
}
