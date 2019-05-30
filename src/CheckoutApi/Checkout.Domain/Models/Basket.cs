using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.Domain.Models
{
    public class Basket
    {
        public string _id { get; set; }
        public List<Product> Products { get; set; }

        public Basket()
        {
            Products = new List<Product>();
        }
    }
}
