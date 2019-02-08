//using System.IO;
//using System.Linq;
//using CarDealer.Data;
//using Newtonsoft.Json;

//namespace CarDealer.App.Export
//{
//    public class Query1
//    {
//        public static void Main()
//        {
//            var context = new CarDealerContext();

//            var cars = context.Customers
//                .Select(c => new
//                {
//                    Id = c.Id,
//                    Name = c.Name,
//                    BirthDate = c.BirthDate,
//                    IsYoungDriver = c.IsYoungDriver,
//                    Sales = c.Sales.ToArray()
//                })
//                .OrderBy(c => c.BirthDate)
//                .ThenBy(c => c.IsYoungDriver)
//                .ToArray();

//            var jsonString = JsonConvert.SerializeObject(cars, new JsonSerializerSettings
//            {
//                Formatting = Formatting.Indented,
//                NullValueHandling = NullValueHandling.Ignore
//            });

//            File.WriteAllText("../../../Json/ordered-customers.json", jsonString);
//        }
//    }
//}