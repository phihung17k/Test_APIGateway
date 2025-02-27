using ProductService.Models;

namespace ProductService.Services.Interface
{
    public interface IProductServices
    {
        public IEnumerable<Product> GetProductList();
        public Task<Product?> GetProductById(int id);
        public Task<Product> AddProduct(Product product);
        public Task<Product> UpdateProduct(Product product);
        public Task<bool> DeleteProduct(int id);
    }
}