//using System;
//using System.Collections.Generic;
//using System.Diagnostics.Tracing;
//using DataAnnotations = System.ComponentModel.DataAnnotations;
//using System.IO;
//using System.Xml.Serialization;
//using AutoMapper;
//using ProductShop.App.Dto;
//using ProductShop.Data;
//using ProductShop.Models;


//namespace ProductShop.App
//{
//    public class StartUpImport
//    {
//        public static void Main()
//        {
//            var config = new MapperConfiguration(cfg =>
//            {
//                cfg.AddProfile<ProductShopProfile>();
//            });

//            var mapper = config.CreateMapper();

//            //var xmlString = File.ReadAllText("Xml/users.xml");
//            //var xmlString = File.ReadAllText("Xml/products.xml");
//            //var xmlString = File.ReadAllText("Xml/categories.xml");


//            //var serializer = new XmlSerializer(typeof(UserDto[]), new XmlRootAttribute("users"));
//            //var serializer = new XmlSerializer(typeof(ProductDto[]), new XmlRootAttribute("products"));
//            //var serializer = new XmlSerializer(typeof(CategoryDto[]), new XmlRootAttribute("categories"));


//            //var deserializedUser = (UserDto[])serializer.Deserialize(new StringReader(xmlString));
//            //var deserializedUser = (ProductDto[])serializer.Deserialize(new StringReader(xmlString));
//            //var deserializedUser = (CategoryDto[])serializer.Deserialize(new StringReader(xmlString));


//            //List<User> users = new List<User>();
//            //List<Product> products = new List<Product>();
//            // List<Category> categories = new List<Category>();
//            List<CategoryProduct> categoryPsroducts = new List<CategoryProduct>();


//            //foreach (var userDto in deserializedUser)
//            //{

//            //    if (!IsValid(userDto))
//            //    {
//            //        continue;
//            //    }

//            //    var user = mapper.Map<User>(userDto);
//            //    users.Add(user);
//            //}


//            //int counter = 1;
//            //foreach (var productDto in deserializedUser)
//            //{

//            //    if (!IsValid(productDto))
//            //    {
//            //        continue;
//            //    }

//            //    var product = mapper.Map<Product>(productDto);

//            //    var buyerId = new Random().Next(1, 30);
//            //    var sellerId = new Random().Next(31, 58);

//            //    product.BuyerId = buyerId;
//            //    product.SellerId = sellerId;

//            //    if (counter == 5)
//            //    {
//            //        product.BuyerId = null;
//            //        counter = 1;
//            //    }

//            //    counter++;
//            //    products.Add(product);
//            //}

//            //foreach (var categoryDto in deserializedUser)
//            //{

//            //    if (!IsValid(categoryDto))
//            //    {
//            //        continue;
//            //    }

//            //    var category = mapper.Map<Category>(categoryDto);
//            //    categories.Add(category);
//            //}

//            var context = new ProductShopContext();

//            //context.Users.AddRange(users);
//            //context.Products.AddRange(products);
//            //context.Categories.AddRange(categories);

//            for (int productId = 1; productId < 201; productId++)
//            {
//                var categoryId = new Random().Next(1, 12);

//                var categoryProduct = new CategoryProduct()
//                {
//                    ProductId = productId,
//                    CategoryId = categoryId,
//                };

//                categoryPsroducts.Add(categoryProduct);
//            }

//            context.CategoryProducts.AddRange(categoryPsroducts);

//            context.SaveChanges();

//        }

//        public static bool IsValid(object obj)
//        {
//            var validationContext = new DataAnnotations.ValidationContext(obj);

//            var validationResult = new List<DataAnnotations.ValidationResult>();

//            return DataAnnotations.Validator.TryValidateObject(obj, validationContext, validationResult, true);
//        }
//    }
//}
