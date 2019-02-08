using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using CarDealer.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CarDealer.App.Export
{
    public class Query6
    {
        public static void Main()
        {
            var context = new CarDealerContext();


            var sales = context.Sales.Select(x => new
            {
                car = new
                {
                    Make = x.Car.Make,
                    Model = x.Car.Model,
                    TravelledDistance = x.Car.TravelledDistance,
                },

                customerName = x.Customer.Name,
                Discount = x.Discount,
                price = x.Car.PartsCar.Sum(s => s.Part.Price),
                priceWithDiscount = x.Car.PartsCar.Sum(s => s.Part.Price) - (x.Car.PartsCar.Sum(s => s.Part.Price) * x.Discount)


            }).ToArray();



            var jsonString = JsonConvert.SerializeObject(sales, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });

            File.WriteAllText("../../../Json/sales-discounts.json", jsonString);
        }
    }
}