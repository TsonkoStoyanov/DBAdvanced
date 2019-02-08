using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ProductShop.App.Export
{
    using AutoMapper;

    using Data;
    using Models;

    public class StartUpQuery4
    {
        public static void Main(string[] args)
        {
            var context = new ProductShopContext();

            var users = new
            {
                usersCount = context.Users.Count(),
                users = context.Users
                    .OrderByDescending(x => x.ProductsSold.Count)
                    .ThenBy(l => l.LastName)
                               .Where(x => x.ProductsSold.Count >= 1 && x.ProductsSold.Any(s => s.Buyer != null))
                    .Select(x => new
                    {
                        firstName = x.FirstName,
                        lastName = x.LastName,
                        age = x.Age,
                        soldProducts = new
                        {
                            count = x.ProductsSold.Count,
                            products = x.ProductsSold.Select(v => new
                            {
                                name = v.Name,
                                price = v.Price
                            }).ToArray()
                        }
                    }).ToArray()
            };


            var jsonProdusts = JsonConvert.SerializeObject(users, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });

            File.WriteAllText("../../../Json/categories-by-products.json", jsonProdusts);

        }
    }
}