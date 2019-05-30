using Checkout.Domain.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Checkout.Infrastructure.Database
{
    public class BasketDb
    {
        public static string Create()
        {
            try
            {
                using (var db = new LiteDatabase(@"Checkout.db"))
                {
                    var baskets = db.GetCollection<Basket>("basket");

                    var id = baskets.Insert(new Basket
                    {
                        _id = Guid.NewGuid().ToString()
                    });

                    return id.AsString;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool Update(Basket basket)
        {
            try
            {
                using (var db = new LiteDatabase(@"Checkout.db"))
                {
                    var baskets = db.GetCollection<Basket>("basket");

                    var ret = baskets.Update(basket);

                    return ret;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Basket Retrieve(string id)
        {
            try
            {
                using (var db = new LiteDatabase(@"Checkout.db"))
                {
                    var baskets = db.GetCollection<Basket>("basket");

                    return baskets.FindById(id);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool Delete(string id)
        {
            try
            {
                using (var db = new LiteDatabase(@"Checkout.db"))
                {
                    var baskets = db.GetCollection<Basket>("basket");

                    var ret = baskets.Delete(id);

                    return ret; 
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
