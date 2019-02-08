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

//            var suppliers = context.Suppliers
//                .Where(x => x.IsImporter == false)
//                .Select(s => new
//                {
//                    Id = s.Id,
//                    Name = s.Name,
//                    PartsCount = s.Parts.Count
//                }).ToArray();
       
//            var jsonString = JsonConvert.SerializeObject(suppliers, new JsonSerializerSettings
//            {
//                Formatting = Formatting.Indented,
//                NullValueHandling = NullValueHandling.Ignore
//            });

//            File.WriteAllText("../../../Json/local-suppliers.json", jsonString);
//        }
//    }
//}