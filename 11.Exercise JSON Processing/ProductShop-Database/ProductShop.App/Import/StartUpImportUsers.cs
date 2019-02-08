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

//    public class StartUpImportUsers
//    {
//        public static void Main(string[] args)
//        {
//            var config = new MapperConfiguration(cfg =>
//            {
//                cfg.AddProfile<ProductShopProfile>();
//            });
//            var mapper = config.CreateMapper();

//            var context = new ProductShopContext();

//            var jsonString = File.ReadAllText("../../../Json/users.json");

//            var desirialiedUser = JsonConvert.DeserializeObject<User[]>(jsonString);

//            List<User> users = new List<User>();

//            foreach (var user in desirialiedUser)
//            {
//                if (IsValid(user))
//                {
//                    users.Add(user);
//                }
//            }

//            context.Users.AddRange(users);
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
