using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.Domain.Models
{
    public class Product
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal ActualPrice { get; set; }
        public decimal Price { get; set; }
        public int ActualQuantity { get; set; }
        public int Quantity { get; set; }

        public void ApplyPromotion()
        {
            if (Code.ToUpperInvariant() == "VOUCHER")
            {
                if (ActualQuantity >= 2)
                {
                    Quantity = ActualQuantity;

                    for (int i = 2; i <= ActualQuantity ; i++)
                    {
                        if (i % 2 == 0)
                        {
                            --Quantity;
                        }
                    }

                    
                }
            }
            else if (Code.ToUpperInvariant() == "TSHIRT")
            {
                if (ActualQuantity >= 3)
                {
                    var newPrice = ActualPrice * 0.95m;

                    Price = newPrice;
                }
                else
                {
                    Price = ActualPrice;
                }
            }
        }
    }
}
