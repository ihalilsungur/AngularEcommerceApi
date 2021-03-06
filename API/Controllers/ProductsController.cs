using API.Core.DbModels;
using API.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Core.Specifications;
using API.Dtos;
using AutoMapper;

namespace API.Controllers
{
   
    public class ProductsController : BaseApiController
    {
    
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;
        private readonly IMapper _mapper;
        public ProductsController(IGenericRepository<Product> productRepository,
            IGenericRepository<ProductBrand> productBrandRepository,
            IGenericRepository<ProductType> productTypeRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _productBrandRepository = productBrandRepository;
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnResponse>>> GetProducts()
        {
            var spec = new ProductsWithProductTypeAndBrandsSpecification();
            var list = await _productRepository.ListAsync(spec);
           // return Ok(list);
           /*
           return list.Select(product => new ProductToReturnResponse
           {
               Id = product.Id,
               Name = product.Name,
               Description = product.Description,
               PictureUrl = product.PictureUrl,
               Price = product.Price,
               ProductBrand = product.ProductBrand != null ? product.ProductBrand.Name : string.Empty,
               ProductType = product.ProductType != null ? product.ProductType.Name : string.Empty
           }).ToList();
           
           */
           return Ok(_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnResponse>>(list));
        }

        [HttpGet("{id}")] //https://localhost:44306/api/products/3
        public async Task<ActionResult<ProductToReturnResponse>> GetProduct(int id)
        {
            var spec = new ProductsWithProductTypeAndBrandsSpecification(id);
            var product = await _productRepository.GetEntityWithSpec(spec);
            /*
            return new ProductToReturnResponse
            {
              Id = product.Id,
              Name = product.Name,
              Description = product.Description,
              PictureUrl = product.PictureUrl,
              Price = product.Price,
              ProductBrand = product.ProductBrand != null ? product.ProductBrand.Name : string.Empty,
              ProductType = product.ProductType != null ? product.ProductType.Name:string.Empty
            };
            */
            return _mapper.Map<Product, ProductToReturnResponse>(product);
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
