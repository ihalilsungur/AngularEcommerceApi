using System.Threading.Tasks;
using API.Core.DbModels;
using API.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   // [Route("api/[controller]")]
    //[ApiController]
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket customerBasket)
        {
            var updated = await _basketRepository.UpdateBasketAsync(customerBasket);
            return Ok(updated);
        }

        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }
     }
}
