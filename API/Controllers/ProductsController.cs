using API.Core.DbModels;
using API.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
    
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;
        public ProductsController(IGenericRepository<Product> productRepository,
            IGenericRepository<ProductBrand> productBrandRepository,
            IGenericRepository<ProductType> productTypeRepository)
        {
            _productRepository = productRepository;
            _productBrandRepository = productBrandRepository;
            _productTypeRepository = productTypeRepository;
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var list = await _productRepository.ListAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")] //https://localhost:44306/api/products/3
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product;
        }

        [HttpGet("brands")] //https://localhost:44306/api/products/brands
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var product = await _productBrandRepository.ListAllAsync();
            return Ok(product);
        }

        [HttpGet("types")] //https://localhost:44306/api/products/types
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var product = await _productTypeRepository.ListAllAsync();
            return Ok(product);
        }
    }
}
