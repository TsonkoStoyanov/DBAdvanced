using System.Xml.Serialization;

namespace ProductShop.App.Dto.Export
{
    [XmlRoot("users")]
    public class UsersQ4Dto
    {
        [XmlAttribute("count")]
        public int Count { get; set; }
        
        [XmlElement("user")]
        public UserQ4Dto[] Users { get; set; }

    }
}