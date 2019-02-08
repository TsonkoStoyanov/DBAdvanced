//using System.IO;
//using System.Linq;
//using CarDealer.Data;
//using Microsoft.EntityFrameworkCore;
//using Newtonsoft.Json;

//namespace CarDealer.App.Export
//{
//    public class Query5
//    {
//        public static void Main()
//        {
//            var context = new CarDealerContext();

//            var customers = context.Customers.Where(x => x.Sales.Count >= 1).Select(s => new
//            {
//                fullName = s.Name,
//                boughtCars = s.Sales.Count,
//                spentMoney = s.Sales.Sum(c => c.Car.PartsCar.Sum(p => p.Part.Price))
                
//            })
//            .OrderByDescending(c => c.spentMoney)
//                .ThenByDescending(c => c.boughtCars)
//                .ToArray();


//            var jsonString = JsonConvert.SerializeObject(customers, new JsonSerializerSettings
//            {
//                Formatting = Formatting.Indented,
//                NullValueHandling = NullValueHandling.Ignore
//            });

//            File.WriteAllText("../../../Json/customers-total-sales.json", jsonString);
//        }
//    }
//}