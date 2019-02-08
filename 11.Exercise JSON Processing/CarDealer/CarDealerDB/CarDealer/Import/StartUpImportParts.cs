//using System;
//using System.Collections.Generic;
//using System.IO;
//using CarDealer.Data;
//using CarDealer.Models;
//using Newtonsoft.Json;

//namespace CarDealer.App.Import
//{
//    public class StartUpImportSuppliers
//    {
//        public static void Main()
//        {
//            var context = new CarDealerContext();

//            var jsonString = File.ReadAllText("../../../Json/parts.json");

//            var deserializedParts = JsonConvert.DeserializeObject<Part[]>(jsonString);

//            var parts = new List<Part>();

//            foreach (var part in deserializedParts)
//            {
//                var supplierId = new Random().Next(1, 32);
//                part.Supplier_Id = supplierId;

//                parts.Add(part);
//            }

//            context.Parts.AddRange(parts);
//            context.SaveChanges();
//        }
//    }
//}