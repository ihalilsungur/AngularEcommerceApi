using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using API.Core.DbModels;

namespace API.Core.Interfaces
{
  public  interface IBasketRepository
  {
      Task<CustomerBasket> GetBasketAsync(string basketId);
      Task<CustomerBasket> UpdateBasketAsync(CustomerBasket customerBasket);
      Task<bool> DeleteBasketAsync(string basketId);
  }
}
