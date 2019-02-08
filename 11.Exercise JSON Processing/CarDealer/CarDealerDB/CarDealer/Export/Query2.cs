//using System.IO;
//using System.Linq;
//using CarDealer.Data;
//using Newtonsoft.Json;

//namespace CarDealer.App.Export
//{
//    public class Query2
//    {
//        public static void Main()
//        {
//            var context = new CarDealerContext();

//            var cars = context.Cars.Where(x => x.Make == "Toyota")
//                .OrderBy(x => x.Model)
//                .ThenByDescending(x => x.TravelledDistance).Select(c => new
//                {
//                    Id = c.Id,
//                    Make = c.Make,
//                    Model = c.Model,
//                    TravelledDistance = c.TravelledDistance
//                }).ToArray();


//            var jsonString = JsonConvert.SerializeObject(cars, new JsonSerializerSettings
//            {
//                Formatting = Formatting.Indented,
//                NullValueHandling = NullValueHandling.Ignore
//            });

//            File.WriteAllText("../../../Json/toyota-cars.json", jsonString);
//        }
//    }
//}