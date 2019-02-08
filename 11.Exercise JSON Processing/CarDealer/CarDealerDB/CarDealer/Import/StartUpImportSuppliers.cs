//using System;
//using System.Collections.Generic;
//using System.IO;
//using CarDealer.Data;
//using CarDealer.Models;
//using Newtonsoft.Json;

//namespace CarDealer.App
//{
//    public class StartUpImportSuppliers
//    {
//        public static void Main()
//        {
//            var context = new CarDealerContext();

//            var jsonString = File.ReadAllText("../../../Json/suppliers.json");

//            var deserializedSuppliers = JsonConvert.DeserializeObject<Supplier[]>(jsonString);

//            var suppliers = new List<Supplier>();

//            foreach (var supplier in deserializedSuppliers)
//            {
//                suppliers.Add(supplier);
//            }

//            context.Suppliers.AddRange(suppliers);
//            context.SaveChanges();
//        }
//    }
//}
