using System.Xml.Serialization;

namespace ProductShop.App.Dto.Export
{
    [XmlType("product")]
    public class ProductQ1Dto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        
        [XmlAttribute("price")]
        public decimal Price { get; set; }

        [XmlAttribute("buyer")]
        public string Buyer { get; set; }
    }
}