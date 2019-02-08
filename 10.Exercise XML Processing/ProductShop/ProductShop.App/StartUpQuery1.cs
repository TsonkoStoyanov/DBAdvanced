//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Xml;
//using System.Xml.Serialization;
//using ProductShop.App.Dto.Export;
//using ProductShop.Data;

//namespace ProductShop.App
//{
//    public class StartUpQuery1
//    {
//        public static void Main()
//        {
//            var context = new ProductShopContext();

//            var products = context.Products.Where(p => p.Price >= 1000 && p.Price <= 2000 && p.Buyer != null)
//                .OrderByDescending(p => p.Price)
//                .Select(x => new ProductQ1Dto
//                {
//                    Name = x.Name,
//                    Price = x.Price,
//                    Buyer = x.Buyer.FirstName + " " + x.Buyer.LastName ?? x.Buyer.LastName
//                }).ToArray();

//            var sb = new StringBuilder();

//            var xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
//            var seriallaizer = new XmlSerializer(typeof(ProductQ1Dto[]), new XmlRootAttribute("products"));
//            seriallaizer.Serialize(new StringWriter(sb), products, xmlNamespaces);

//            File.WriteAllText("../../../Xml/products-in-range.xml", sb.ToString());
//        }
//    }
//}