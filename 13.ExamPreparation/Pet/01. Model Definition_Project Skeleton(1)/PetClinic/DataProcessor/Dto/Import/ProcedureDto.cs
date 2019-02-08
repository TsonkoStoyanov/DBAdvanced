
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace PetClinic.DataProcessor.Dto.Import
{
    [XmlType("Procedure")]
    public class ProcedureDto
    {
        [XmlElement("Vet")]
        [Required]
        public string Vet { get; set; }

        [XmlElement("Animal")]
        [Required]
        public string Animal { get; set; }

        [XmlElement("DateTime")]
        [Required]
        public string DateTime { get; set; }

        [XmlArray("AnimalAids")]
        public AnimalAidXmlDto[] AnimalAids { get; set; }


    }
}