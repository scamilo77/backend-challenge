using Checkout.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.Infrastructure.Database
{
    public class BasketRepository : IBasketRepository
    {
        public Task<string> CreateBasket()
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    return BasketDb.Create();
                }
                catch (Exception)
                {
                    throw;
                }
            });        
        }

        public Task AddProductToBasket(string id, Product product)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    var basket = BasketDb.Retrieve(id);

                    if (basket.Products.Exists(x => x.Code == product.Code))
                    {
                        basket.Products.FirstOrDefault(p => p.Code == product.Code).ActualQuantity += product.Quantity;
                        basket.Products.FirstOrDefault(p => p.Code == product.Code).ApplyPromotion();
                    }
                    else
                    {
                        product.ActualPrice = product.Price;
                        product.ActualQuantity = product.Quantity;
                        product.ApplyPromotion();
                        basket.Products.Add(product);
                    }
                    BasketDb.Update(basket);
                }
                catch (Exception)
                {
                    throw;
                }
            });
        }

        public Basket GetBasket(string id)
        {
            try
            {
                var basket = BasketDb.Retrieve(id);

                return basket;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task DeleteBasket(string id)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    BasketDb.Delete(id);
                }
                catch (Exception)
                {
                    throw;
                }
            });
        }
    }
}
