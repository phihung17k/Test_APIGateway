using Microsoft.AspNetCore.Mvc;
using ProductService.Models;
using ProductService.Services.Interface;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices _productService;
        public ProductsController(IProductServices productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IEnumerable<Product> ProductList()
        {
            var productList = _productService.GetProductList();
            return productList;

        }

        [HttpGet("{id}")]
        public async Task<Product?> GetProductById(int id)
        {
            return await _productService.GetProductById(id);
        }

        [HttpPost]
        public async Task<Product> AddProduct(Product product)
        {
            return await _productService.AddProduct(product);
        }

        [HttpPut]
        public async Task<Product> UpdateProduct(Product product)
        {
            return await _productService.UpdateProduct(product);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteProduct(int id)
        {
            return await _productService.DeleteProduct(id);
        }
    }
}
