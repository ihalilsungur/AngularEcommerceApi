using API.Core.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Core.Interfaces
{
    public  interface IProductRepository
  {
      /// <summary>
      /// Id ile ürünü  getirme
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      Task<Product> GetProductByIdAsync(int id);
      /// <summary>
      /// Tüm ürünler listeleme
      /// </summary>
      /// <returns></returns>
      Task<IReadOnlyList<Product>> GetProductAsync();

      /// <summary>
      /// Get All ProductTypes
      /// </summary>
      /// <returns></returns>
      Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
        /// <summary>
        /// Get All ProductBrand
        /// </summary>
        /// <returns></returns>
      Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync();
      
  }
}
