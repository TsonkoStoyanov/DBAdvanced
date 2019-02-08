using System.Xml.Serialization;

namespace ProductShop.App.Dto.Export
{
    [XmlType("category")]
    public class CategoryQ3Dto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("products-count")]
        public int Count { get; set; }

        [XmlElement("average-price")]
        public decimal AveragePrice { get; set; }

        [XmlElement("total-revenue")]
        public decimal totalRevenue { get; set; }

    }
}