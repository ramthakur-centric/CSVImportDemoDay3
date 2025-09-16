using System.Collections.Generic;

namespace CsvImportDemo.Repositories
{
   // Implement ProductRepository using AppDbContext and IProductRepository methods (AddAsync, GetAllAsync, SaveChangesAsync)
       
    public class ProductRepository : IProductRepository
    {
        private readonly Data.AppDbContext _context;

        public ProductRepository(Data.AppDbContext context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task AddAsync(Models.Product p)
        {
            await _context.Products.AddAsync(p);
        }

        public async System.Threading.Tasks.Task<IEnumerable<Models.Product>> GetAllAsync()
        {
            return await System.Threading.Tasks.Task.FromResult(_context.Products.AsEnumerable());
        }

        public async System.Threading.Tasks.Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
