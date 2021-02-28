using API.Core.DbModels;
using API.Core.Interfaces;
using API.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Infrastructure.Implements
{
    public class ProductRepository : IProductRepository
   {
       private readonly StoreContext _storeContext;
        public ProductRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        /// <summary>
        /// Id ile ürün getirme
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _storeContext.Products
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }

        /// <summary>
        /// Tüm ürünleri Listeleme
        /// </summary>
        /// <returns></returns>
        public async Task<IReadOnlyList<Product>> GetProductAsync()
        {
            var list = await _storeContext.Products
                .Include(p=>p.ProductBrand)
                .Include(p=>p.ProductType).
                ToListAsync();
            return list;
        }

        /// <summary>
        /// Get All ProductTypes
        /// </summary>
        /// <returns></returns>
        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _storeContext.ProductTypes.ToListAsync();
        }

        /// <summary>
        /// Get All ProductBrands
        /// </summary>
        /// <returns></returns>
        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync()
        {
            return await _storeContext.ProductBrands.ToListAsync();
        }
   }
}
