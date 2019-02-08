using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using PetClinic.DataProcessor.Dto.Export;
using PetClinic.Models;
using Formatting = Newtonsoft.Json.Formatting;

namespace PetClinic.DataProcessor
{
    using System;

    using PetClinic.Data;

    public class Serializer
    {
        public static string ExportAnimalsByOwnerPhoneNumber(PetClinicContext context, string phoneNumber)
        {
            var animal = context.Animals.Where(x => x.Passport.OwnerPhoneNumber == phoneNumber)
                .Select(a => new
                {
                    OwnerName = a.Passport.OwnerName,
                    AnimalName = a.Name,
                    Age = a.Age,
                    SerialNumber = a.PassportSerialNumber,
                    RegisteredOn = a.Passport.RegistrationDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)
                }).OrderBy(a => a.Age)
                .ThenBy(s => s.SerialNumber).ToArray();

            var jsonString = JsonConvert.SerializeObject(animal, Formatting.Indented);

            return jsonString;
        }

        public static string ExportAllProcedures(PetClinicContext context)
        {
            var procedure = context.Procedures.OrderBy(p => p.DateTime)
                .Select(x => new ExportProceduresDto
                {
                    Passport = x.Animal.PassportSerialNumber,
                    OwnerNumber = x.Animal.Passport.OwnerPhoneNumber,
                    DateTime = x.DateTime.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
                    AnimalAids = x.ProcedureAnimalAids.Select(k => new AnimalAidExportDto()
                    {
                        Name = k.AnimalAid.Name,
                        Price = k.AnimalAid.Price
                    }).ToArray(),
                    TotalPrice = x.ProcedureAnimalAids.Sum(p => p.AnimalAid.Price)

                }).OrderBy(x => x.Passport).ToArray();

            var sb = new StringBuilder();

            var serialaizer = new XmlSerializer(typeof(ExportProceduresDto[]), new XmlRootAttribute("Procedures"));
            serialaizer.Serialize(new StringWriter(sb), procedure, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));

            return sb.ToString().TrimEnd();
        }
    }
}
