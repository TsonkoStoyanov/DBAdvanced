//using System;
//using System.Collections.Generic;
//using System.Linq;
//using CarDealer.Data;
//using CarDealer.Models;

//namespace CarDealer.App.Import
//{
//    public class StartUpImportSales
//    {
//        public static void Main()
//        {
//            var context = new CarDealerContext();

//            var sales = new List<Sale>();

//            var cars = context.Cars.Select(x => x.Id).ToList();

//            var customers = context.Customers.ToList();

//            var discounts = new[] { 0.05m, 0.10m, 0.15m, 0.20m, 0.30m, 0.40m, 0.50m };

//            for (int i = 0; i < 100; i++)
//            {
//                var index = new Random().Next(0, discounts.Length - 1);
//                var randomCars = new Random().Next(0, cars.Count - 1);
//                var randomCustomer = new Random().Next(0, customers.Count - 1);

//                var sale = new Sale
//                {
//                    Car_Id = cars[randomCars],
//                    Customer = customers[randomCustomer],
//                };

//                if (sale.Customer.IsYoungDriver)
//                {
//                    sale.Discount = discounts[index] + 0.05m;
//                }
//                else
//                {
//                    sale.Discount = discounts[index];
//                }

//                sales.Add(sale);

//            }


//            context.Sales.AddRange(sales);
//            context.SaveChanges();
//        }

//    }
//}
