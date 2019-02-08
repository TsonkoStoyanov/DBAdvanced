//using System.IO;
//using System.Linq;
//using Newtonsoft.Json;

//namespace ProductShop.App.Export
//{
//    using AutoMapper;

//    using Data;
//    using Models;

//    public class StartUpQuery1
//    {
//        public static void Main(string[] args)
//        {
//            var context = new ProductShopContext();

//            var produts = context.Products.Where(x => x.Price >= 500 && x.Price <= 1000).OrderBy(x => x.Price).Select(x => new
//            {
//                name = x.Name,
//                price = x.Price, 
//                seller = x.Seller.FirstName + " " + x.Seller.LastName ?? x.Seller.LastName
//            }).ToArray();


//            var jsonProdusts = JsonConvert.SerializeObject(produts, Formatting.Indented);

//            File.WriteAllText("../../../Json/products-in-range.json", jsonProdusts);

//        }
//    }
//}