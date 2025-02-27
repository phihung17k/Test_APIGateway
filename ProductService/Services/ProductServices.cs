using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProductService.Data;
using ProductService.Models;
using ProductService.Services.Interface;

namespace ProductService.Services
{
    public class ProductServices : IProductServices
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductServices(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> AddProduct(Product product)
        {
            EntityEntry<Product> result = await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            Product? product = await _dbContext.Products.FindAsync(id);
            if (product == null)
                return false;

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Product?> GetProductById(int id)
        {
            return await _dbContext.Products.FindAsync(id);
        }

        public IEnumerable<Product> GetProductList()
        {
            return _dbContext.Products;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            EntityEntry<Product> result = _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}
