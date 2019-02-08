using System.Xml.Serialization;

namespace ProductShop.App.Dto.Export
{
    [XmlType("sold-product")]
    public class SoldQ4ProductDto
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlElement("product")]
        public ProductQ4Dto[] ProductsQ4Dto { get; set; }
    }
}