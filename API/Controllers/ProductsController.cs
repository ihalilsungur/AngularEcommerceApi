using API.Core.DbModels;
using API.Core.Interfaces;
using API.Core.Specifications;
using API.Dtos;
using API.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<ActionResult<Pagination<ProductToReturnResponse>>> GetProducts([FromQuery]ProductSpecParams productSpecParams)
        {
            var spec = new ProductsWithProductTypeAndBrandsSpecification(productSpecParams);
            var countSpec =  new ProductWithFiltersForCountSpecification(productSpecParams);
            var totalItems = await _productRepository.CountAsync(countSpec);
            var products = await _productRepository.ListAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnResponse>>(products);
            
            return Ok(new Pagination<ProductToReturnResponse>(productSpecParams.PageIndex,productSpecParams.PageSize,totalItems,data));
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
