//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading;
//using CarDealer.Data;
//using CarDealer.Models;
//using Newtonsoft.Json;

//namespace CarDealer.App.Import
//{
//    public class StartUpImportCustomer
//    {
//        public static void Main()
//        {
//            var context = new CarDealerContext();

//            var jsonString = File.ReadAllText("../../../Json/customers.json");

//            var deserializedCustomers = JsonConvert.DeserializeObject<Customer[]>(jsonString);

//            var customers = new List<Customer>();
//            var parts = context.Parts.ToList();

//            foreach (var customer in deserializedCustomers)
//            {
//                customers.Add(customer);
//            }

//            context.Customers.AddRange(customers);
//            context.SaveChanges();
//        }

//    }
//}