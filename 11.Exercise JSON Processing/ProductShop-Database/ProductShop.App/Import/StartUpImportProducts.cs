//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.IO;
//using Newtonsoft.Json;

//namespace ProductShop.App.Import
//{
//    using AutoMapper;

//    using Data;
//    using Models;

//    public class StartUpImportProducts
//    {
//        public static void Main(string[] args)
//        {
//            var config = new MapperConfiguration(cfg =>
//            {
//                cfg.AddProfile<ProductShopProfile>();
//            });
//            var mapper = config.CreateMapper();

//            var context = new ProductShopContext();

//            var jsonString = File.ReadAllText("../../../Json/products.json");

//            var desirialiedProducts = JsonConvert.DeserializeObject<Product[]>(jsonString);

//            List<Product> products = new List<Product>();

//            foreach (var product in desirialiedProducts)
//            {
//                if (!IsValid(product))
//                {
//                    continue;
//                }


//                var sellerId = new Random().Next(1, 35);
//                var buyerId = new Random().Next(1, 57);

//                var random = new Random().Next(1, 4);



//                product.SellerId = sellerId;
//                product.BuyerId = buyerId;

//                if (random == 3)
//                {
//                    product.BuyerId = null;
//                }

//                products.Add(product);
//            }

//            context.Products.AddRange(products);
//            context.SaveChanges();
//        }

//        public static bool IsValid(object obj)
//        {
//            var validationContex = new System.ComponentModel.DataAnnotations.ValidationContext(obj);

//            var results = new List<ValidationResult>();


//            return Validator.TryValidateObject(obj, validationContex, results, true);
//        }
//    }
//}
