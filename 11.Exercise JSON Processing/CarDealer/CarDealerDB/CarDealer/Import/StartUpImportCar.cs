using System;
using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading;
//using CarDealer.Data;
//using CarDealer.Models;
//using Newtonsoft.Json;

//namespace CarDealer.App.Import
//{
//    public class StartUpImportCar
//    {
//        public static void Main()
//        {
//            var context = new CarDealerContext();

//            var jsonString = File.ReadAllText("../../../Json/cars.json");

//            var deserializedCars = JsonConvert.DeserializeObject<Car[]>(jsonString);

//            var cars = new List<Car>();
//            var parts = context.Parts.ToList();

//            foreach (var car in deserializedCars)
//            {
//                car.PartsCar = GetPartsCar(car, parts);
//                cars.Add(car);

//            }

//            context.Cars.AddRange(cars);
//            context.SaveChanges();
//        }

//        private static ICollection<PartCar> GetPartsCar(Car car, List<Part> parts)
//        {
//            var partsCar = new List<PartCar>();
//            var partsColection = new List<Part>();

//                var numberOfParts = new Random().Next(10, 20);

//                while (partsColection.Count <= numberOfParts)
//                {
//                    var part = parts.ElementAt(new Random().Next(1, 131));

//                    if (partsColection.Contains(part))
//                    {
//                        continue;
//                    }

//                    partsColection.Add(part);

//                    var partCar = new PartCar
//                    {
//                        Car = car,
//                        Car_Id = car.Id,
//                        Part = part,
//                        Part_Id = part.Id
//                    };

//                    partsCar.Add(partCar);
//                }
              
          

//            return partsCar;
//        }
//    }
//}