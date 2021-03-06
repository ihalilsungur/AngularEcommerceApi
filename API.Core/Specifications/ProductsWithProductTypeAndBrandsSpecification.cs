using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using API.Core.DbModels;

namespace API.Core.Specifications
{
  public  class ProductsWithProductTypeAndBrandsSpecification:BaseSpecification<Product>
    {
        public ProductsWithProductTypeAndBrandsSpecification()
        {
            AddInclude(x=>x.ProductBrand);
            AddInclude(x=>x.ProductType);
        }

        public ProductsWithProductTypeAndBrandsSpecification(int id):base(x=>x.Id==id)
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
        }
    }
}
