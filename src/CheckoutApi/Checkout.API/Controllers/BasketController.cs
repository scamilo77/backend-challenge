using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkout.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Checkout.API.Controllers
{
    [Produces("application/json")]
    [Route("api/basket")]
    public class BasketController : Controller
    {
        public readonly IBasketRepository basketRepository;

        public BasketController(IBasketRepository repository)
        {
            basketRepository = repository;
        }

        [HttpGet("get/{id}")]
        public ContentResult GetBasket(string id)
        {
            try
            {
                var basket = basketRepository.GetBasket(id);

                List<string> names = new List<string>();
                var total = 0m;

                for (int i = 0; i < basket.Products.Count; i++)
                {

                    for (int j = 0; j < basket.Products[i].ActualQuantity; j++)
                    {
                        names.Add(basket.Products[i].Code);
                    }

                    total += (basket.Products[i].Price * basket.Products[i].Quantity);
                }

                return new ContentResult
                {
                    StatusCode = 200,
                    Content = JsonConvert.SerializeObject(new
                    {
                        Items = string.Join(",", names.Select(x => x.ToString()).ToArray()),
                        Total = $"{total.ToString("N2")}€"
                    })
                };
            }
            catch (Exception ex)
            {
                return new ContentResult
                {
                    StatusCode = 500,
                    Content = ex.Message
                };
            }
            
        }

        [HttpPost("create")]
        public async Task<ContentResult> CreateBasket()
        {
            try
            {
                var id = await basketRepository.CreateBasket();

                return new ContentResult
                {
                    StatusCode = 200,
                    Content = JsonConvert.SerializeObject(new { basketId = id })
                };
            }
            catch (Exception ex)
            {
                return new ContentResult
                {
                    StatusCode = 500,
                    Content = ex.Message
                };
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ContentResult> DeleteBasket(string id)
        {
            try
            {
                await basketRepository.DeleteBasket(id);

                return new ContentResult
                {
                    StatusCode = 200,
                    Content = $"Basket {id} deleted successfully"
                };
            }
            catch (Exception ex)
            {
                return new ContentResult
                {
                    StatusCode = 500,
                    Content = ex.Message
                };
            }
        }

        [HttpPut("add/{id}")]
        public async Task<ContentResult> AddProductToBasket(string id, [FromBody] Product product)
        {
            try
            {
                await basketRepository.AddProductToBasket(id, product);

                return new ContentResult
                {
                    StatusCode = 200,
                    Content = $"Added {product.ActualQuantity} {product.Name} to the basket"
                };
            }
            catch (Exception ex)
            {
                return new ContentResult
                {
                    StatusCode = 500,
                    Content = ex.Message
                };
            }
        }
    }
}