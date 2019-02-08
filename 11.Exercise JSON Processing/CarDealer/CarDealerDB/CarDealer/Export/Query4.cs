//using System.IO;
//using System.Linq;
//using CarDealer.Data;
//using Newtonsoft.Json;

//namespace CarDealer.App.Export
//{
//    public class Query4
//    {
//        public static void Main()
//        {
//            var context = new CarDealerContext();

//            var car = new
//            {
//               car = context.Cars.Select(x => new
//                {
//                    Make = x.Make,
//                    Model = x.Model,
//                    TravelledDistance = x.TravelledDistance, 
//                    parts = x.PartsCar.Select(s => new
//                    {
//                        Name = s.Part.Name,
//                        Price = s.Part.Price

//                    }).ToArray()
//                }).ToArray()
//            };
       
//            var jsonString = JsonConvert.SerializeObject(car, new JsonSerializerSettings
//            {
//                Formatting = Formatting.Indented,
//                NullValueHandling = NullValueHandling.Ignore
//            });

//            File.WriteAllText("../../../Json/cars-and-parts.json", jsonString);
//        }
//    }
//}