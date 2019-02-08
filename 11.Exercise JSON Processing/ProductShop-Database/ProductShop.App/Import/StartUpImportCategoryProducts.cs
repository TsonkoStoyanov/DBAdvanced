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

//    public class StartUpImportCategoryProducts
//    {
//        public static void Main(string[] args)
//        {
//            var config = new MapperConfiguration(cfg =>
//            {
//                cfg.AddProfile<ProductShopProfile>();
//            });
//            var mapper = config.CreateMapper();

//            var context = new ProductShopContext();

//            var categoryProducts = new List<CategoryProduct>();
//            for (int productId = 1; productId < 201; productId++)
//            {
//                var categoryId = new Random().Next(1, 12);

//                var categoryProduct = new CategoryProduct
//                {
//                    CategoryId = categoryId,
//                    ProductId = productId
//                };

//                categoryProducts.Add(categoryProduct);
//            }

//            context.CategoryProducts.AddRange(categoryProducts);
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
