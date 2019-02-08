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

//    public class StartUpImportCategories
//    {
//        public static void Main(string[] args)
//        {
//            var config = new MapperConfiguration(cfg =>
//            {
//                cfg.AddProfile<ProductShopProfile>();
//            });
//            var mapper = config.CreateMapper();

//            var context = new ProductShopContext();

//            var jsonString = File.ReadAllText("../../../Json/categories.json");

//            var desirialiedCategories = JsonConvert.DeserializeObject<Category[]>(jsonString);

//            List<Category> categories = new List<Category>();

//            foreach (var category in desirialiedCategories)
//            {
//                if (IsValid(category))
//                {
//                    categories.Add(category);
//                }

//            }

//            context.Categories.AddRange(categories);
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
