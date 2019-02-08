using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using FastFood.Data;
using FastFood.DataProcessor.Dto.Export;
using FastFood.Models.Enums;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace FastFood.DataProcessor
{
    public class Serializer
    {
        public static string ExportOrdersByEmployee(FastFoodDbContext context, string employeeName, string orderType)
        {
            var orderTypeAsEnum = Enum.Parse<OrderType>(orderType);
            var employee = context.Employees
                .ToArray()
                .Where(x => x.Name == employeeName)
                .Select(x => new
                {
                    Name = x.Name,
                    Orders = x.Orders.Where(z => z.Type == orderTypeAsEnum)
                        .Select(c => new
                        {
                            Customer = c.Customer,
                            Items = c.OrderItems.Select(i => new
                            {
                                Name = i.Item.Name,
                                Price = i.Item.Price,
                                Quantity = i.Quantity
                            })
                                .ToArray(),
                            TotalPrice = c.TotalPrice
                        })
                        .OrderByDescending(t => t.TotalPrice)
                        .ThenByDescending(t => t.Items.Length)
                        .ToArray(),
                    TotalMade = x.Orders.Where(s => s.Type == orderTypeAsEnum).Sum(s => s.TotalPrice)
                }).FirstOrDefault();

            var jsonString = JsonConvert.SerializeObject(employee, Formatting.Indented);
            return jsonString;

        }

        public static string ExportCategoryStatistics(FastFoodDbContext context, string categoriesString)
        {
            var categoriesArray = categoriesString.Split(',');

            var categories = context.Categories.Where(x => categoriesArray.Any(s => s == x.Name))
                .Select(s => new CategoryDto
                {
                    Name = s.Name,
                    MostPopularItem = s.Items.Select(z => new MostPopularItemDto
                    {
                        Name = z.Name,
                        TimesSold = z.OrderItems.Sum(x => x.Quantity),
                        TotalMade = z.OrderItems.Sum(x => x.Item.Price * x.Quantity)
                    }).OrderByDescending(x => x.TotalMade)
                        .ThenByDescending(x => x.TimesSold)
                        .FirstOrDefault()
                })
                .OrderByDescending(x => x.MostPopularItem.TotalMade)
                .ThenByDescending(x => x.MostPopularItem.TimesSold)
                .ToArray();

            StringBuilder sb = new StringBuilder();
            var serialaizer = new XmlSerializer(typeof(CategoryDto[]), new XmlRootAttribute("Categories"));

            serialaizer.Serialize(new StringWriter(sb),categories, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty}));

            return sb.ToString().TrimEnd();


        }
    }
}