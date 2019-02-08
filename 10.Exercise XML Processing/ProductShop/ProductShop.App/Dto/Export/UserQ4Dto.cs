using System.Xml.Serialization;

namespace ProductShop.App.Dto.Export
{
    [XmlType("user")]
    public class UserQ4Dto
    {
        [XmlAttribute("first-name")]
        public string FirstName { get; set; }

        [XmlAttribute("last-name")]
        public string LastName { get; set; }

        [XmlAttribute("age")]
        public string Age { get; set; }

        [XmlElement("sold-products")]
        public SoldQ4ProductDto SoldProducts { get; set; }

    }
}