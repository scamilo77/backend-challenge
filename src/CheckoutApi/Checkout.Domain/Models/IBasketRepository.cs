using Checkout.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Domain.Models
{
    public interface IBasketRepository
    {
        Task<string> CreateBasket();
        Task AddProductToBasket(string id, Product product);
        Basket GetBasket(string id);
        Task DeleteBasket(string id);
    }
}
