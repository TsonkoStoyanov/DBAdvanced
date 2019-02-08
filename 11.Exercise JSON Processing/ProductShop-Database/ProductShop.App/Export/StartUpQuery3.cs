//using System.IO;
//using System.Linq;
//using Newtonsoft.Json;

//namespace ProductShop.App.Export
//{
//    using AutoMapper;

//    using Data;
//    using Models;

//    public class StartUpQuery3
//    {
//        public static void Main(string[] args)
//        {
//            var context = new ProductShopContext();

//            var categories = context.Categories.Select(x => new
//            {
//                category = x.Name,
//                productCount = x.CategoryProducts.Count,
//                averagePrice = x.CategoryProducts.Sum(s => s.Product.Price) / x.CategoryProducts.Count,
//                totalRevenue = x.CategoryProducts.Sum(s => s.Product.Price),
//            }).OrderByDescending(x=>x.productCount).ToArray();


//            var jsonProdusts = JsonConvert.SerializeObject(categories, new JsonSerializerSettings
//            {
//                Formatting = Formatting.Indented,
//                NullValueHandling = NullValueHandling.Ignore
//            });

//            File.WriteAllText("../../../Json/categories-by-products.json", jsonProdusts);

//        }
//    }
//}