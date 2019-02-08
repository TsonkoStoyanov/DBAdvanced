using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using ProductShop.App.Dto.Export;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop.App
{
    public class StartUpQuery4
    {
        public static void Main()
        {
            var context = new ProductShopContext();

            var users = new UsersQ4Dto
            {
                Count = context.Users.Count(),
                Users = context.Users.Where(x=>x.ProductsSold.Count>=1)
                    .Select(x=> new UserQ4Dto
                    {
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Age=x.Age.ToString(),
                        SoldProducts =  new SoldQ4ProductDto
                        {
                            Count = x.ProductsSold.Count(),
                            ProductsQ4Dto = x.ProductsSold.Select(k=>new ProductQ4Dto
                            {
                                Name = k.Name,
                                Price = k.Price
                            }).ToArray()
                        }
                    }).ToArray()

            };


            var sb = new StringBuilder();

            var xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var seriallaizer = new XmlSerializer(typeof(UsersQ4Dto), new XmlRootAttribute("categories"));
            seriallaizer.Serialize(new StringWriter(sb), users, xmlNamespaces);

            File.WriteAllText("../../../Xml/users-and-products.xml", sb.ToString());
        }
    }
}