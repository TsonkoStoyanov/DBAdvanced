//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Xml;
//using System.Xml.Serialization;
//using ProductShop.App.Dto.Export;
//using ProductShop.Data;

//namespace ProductShop.App
//{
//    public class StartUpQuery2
//    {
//        public static void Main()
//        {
//            var context = new ProductShopContext();
//            var users = context.Users
//                .Where(x => x.ProductsSold.Count >= 1)

//                .Select(x => new UsersQ2Dto
//                {
//                    FirstName = x.FirstName,
//                    LastName = x.LastName,
//                    SoldProducts = x.ProductsSold.Select(s => new SoldProductDto
//                    {
//                        Name = s.Name,
//                        Price = s.Price
//                    }).ToArray()


//                })
//                .OrderBy(x => x.LastName)
//                .ThenBy(x => x.FirstName)
//                .ToArray();

//            var sb = new StringBuilder();

//            var xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
//            var seriallaizer = new XmlSerializer(typeof(UsersQ2Dto[]), new XmlRootAttribute("users"));
//            seriallaizer.Serialize(new StringWriter(sb), users, xmlNamespaces);

//            File.WriteAllText("../../../Xml/users-sold-products.xml", sb.ToString());
//        }
//    }
//}