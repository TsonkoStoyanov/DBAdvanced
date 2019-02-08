using System.Xml.Serialization;

namespace PetClinic.DataProcessor.Dto.Import
{
    [XmlType("AnimalAid")]
    public class AnimalAidXmlDto
    {
        [XmlElement("Name")]
        public string Name { get; set; }
    }
}