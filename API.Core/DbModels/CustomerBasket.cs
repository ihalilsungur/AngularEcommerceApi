using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace API.Core.DbModels
{
    public class CustomerBasket
    {
        
        public CustomerBasket()
        {
            
        }

        public CustomerBasket(string id)
        {
            Id = id;
        }
        public string Id { get; set; }
        public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
       
    }
}
