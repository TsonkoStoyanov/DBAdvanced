//using System.IO;
//using System.Linq;
//using Newtonsoft.Json;

//namespace ProductShop.App.Export
//{
//    using AutoMapper;

//    using Data;
//    using Models;

//    public class StartUpQuery2
//    {
//        public static void Main(string[] args)
//        {
//            var context = new ProductShopContext();

//            var users = context.Users.Where(x => x.ProductsSold.Count > 1 && x.ProductsSold.Any(s => s.Buyer != null))
//                .OrderBy(l => l.LastName)
//                .ThenBy(f => f.FirstName)
//                .Select(x => new
//                {
//                    firstName = x.FirstName,
//                    lastName = x.LastName,
//                    soldProducts = x.ProductsSold.Where(b=>b.Buyer != null)
//                        .Select(v=> new
//                        {
//                            name = v.Name,
//                            proce = v.Price,
//                            buerFirstName = v.Buyer.FirstName,
//                            buerLastName = v.Buyer.LastName,

//                        }).ToArray()

//                }).ToArray();


//            var jsonProdusts = JsonConvert.SerializeObject(users, new JsonSerializerSettings
//            {
//                Formatting = Formatting.Indented,
//                NullValueHandling = NullValueHandling.Ignore
//            });

//            File.WriteAllText("../../../Json/users-sold-products.json", jsonProdusts);

//        }
//    }
//}