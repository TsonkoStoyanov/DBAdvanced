using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using SoftJail.DataProcessor.ExportDto;
using Formatting = Newtonsoft.Json.Formatting;

namespace SoftJail.DataProcessor
{

    using Data;
    using System;

    public class Serializer
    {
        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            var prisioners = context.Prisoners.Where(x => ids.Any(s => s == x.Id)).Select(k => new
            {
                Id = k.Id,
                Name = k.FullName,
                CellNumber = k.Cell.CellNumber,
                Officers = k.PrisonerOfficers.Select(z => new
                {
                    OfficerName = z.Officer.FullName,
                    Department = z.Officer.Department.Name
                }).OrderBy(x => x.OfficerName).ToArray(),
                TotalOfficerSalary = k.PrisonerOfficers.Sum(n => n.Officer.Salary)

            }).OrderBy(x => x.Name).ThenBy(x => x.Id)
                .ToArray();

            var jsonString = JsonConvert.SerializeObject(prisioners, Formatting.Indented);
            return jsonString;
        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            var prisonersNamesArray = prisonersNames.Split(',');

            var prisioners = context.Prisoners
                .Where(x => prisonersNamesArray.Any(s => s == x.FullName))
                .Select(z => new PrisonerExportDto()
                {
                    Id = z.Id,
                    Name = z.FullName,
                    IncarcerationDate = z.IncarcerationDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    EncryptedMessages = z.Mails.Select(c => new MessagesDto()
                    {
                        Description = GetRevesed(c.Description)

                    }).ToArray()

                })
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Id)
                .ToArray();

            StringBuilder sb = new StringBuilder();
            var serialaizer = new XmlSerializer(typeof(PrisonerExportDto[]), new XmlRootAttribute("Prisoners"));

            serialaizer.Serialize(new StringWriter(sb), prisioners, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));

            return sb.ToString().TrimEnd();
        }

        private static string GetRevesed(string description)
        {
            char[] result;
            StringBuilder sb = new StringBuilder();


            result = description.ToCharArray();
            

            for (int i = 0; i <= result.Length - 1; i++)
            {

                sb.Append(result[result.Length - 1-i]);
            }

            return sb.ToString();

        }
    }
}