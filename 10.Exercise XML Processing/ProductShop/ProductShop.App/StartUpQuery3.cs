//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Xml;
//using System.Xml.Serialization;
//using ProductShop.App.Dto.Export;
//using ProductShop.Data;
//using ProductShop.Models;

//namespace ProductShop.App
//{
//    public class StartUpQuery3
//    {
//        public static void Main()
//        {
//            var context = new ProductShopContext();
//            var categories = context.Categories.OrderByDescending(c => c.CategoryProducts.Count)
//                .Select(x => new CategoryQ3Dto
//                {
//                    Name = x.Name,
//                    Count = x.CategoryProducts.Count,
//                    AveragePrice = x.CategoryProducts.Select(s => s.Product.Price).DefaultIfEmpty(0).Average(),
//                    totalRevenue = x.CategoryProducts.Sum(p => p.Product.Price),

//                })
//               .ToArray();

//            var sb = new StringBuilder();

//            var xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
//            var seriallaizer = new XmlSerializer(typeof(CategoryQ3Dto[]), new XmlRootAttribute("categories"));
//            seriallaizer.Serialize(new StringWriter(sb), categories, xmlNamespaces);

//            File.WriteAllText("../../../Xml/categories-by-products.xml", sb.ToString());
//        }
//    }
//}