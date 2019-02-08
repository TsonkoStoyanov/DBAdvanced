using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using AutoMapper;
using Newtonsoft.Json;
using SoftJail.Data.Models;
using SoftJail.Data.Models.Enums;
using SoftJail.DataProcessor.ImportDto;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace SoftJail.DataProcessor
{

    using Data;
    using System;

    public class Deserializer
    {
        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            var sb = new StringBuilder();
            var jsonDto = JsonConvert.DeserializeObject<DepartamentDto[]>(jsonString);

            var departaments = new List<Department>();

            foreach (var departamentDto in jsonDto)
            {
                bool validCells = true;

                if (!IsValid(departamentDto))
                {
                    sb.AppendLine($"Invalid Data");
                    continue;
                }

                foreach (var cell in departamentDto.Cells)
                {
                    if (!IsValid(cell) || !validCells)
                    {
                        validCells = false;
                        break;
                    }
                }

                if (!validCells)
                {
                    sb.AppendLine($"Invalid Data");
                    continue;
                }
                var departament = Mapper.Map<Department>(departamentDto);
                departaments.Add(departament);

                sb.AppendLine($"Imported {departament.Name} with {departamentDto.Cells.Length} cells");
            }

            context.Departments.AddRange(departaments);
            context.SaveChanges();
            return sb.ToString().TrimEnd();

        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            var sb = new StringBuilder();
            var jsonDto = JsonConvert.DeserializeObject<PrisonerDto[]>(jsonString);


            var prisoners = new List<Prisoner>();

            foreach (var prisionerDto in jsonDto)
            {
                var mails = new List<Mail>();

                var validMails = true;

                if (!IsValid(prisionerDto))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                foreach (var mailDto in prisionerDto.Mails)
                {
                    if (!IsValid(mailDto))
                    {
                        sb.AppendLine("Invalid Data");
                        validMails = false;
                        break;
                    }
                    var mail = new Mail()
                    {
                        Description = mailDto.Description,
                        Address = mailDto.Address,
                        Sender = mailDto.Sender
                    };
                    mails.Add(mail);
                }

                if (!validMails)
                {
                    continue;
                }


                var prisoner = new Prisoner()
                {
                    FullName = prisionerDto.FullName,
                    Nickname = prisionerDto.Nickname,
                    Age = prisionerDto.Age,
                    IncarcerationDate = DateTime.ParseExact(prisionerDto.IncarcerationDate, "dd/MM/yyyy",
                      CultureInfo.InvariantCulture),
                    Bail = prisionerDto.Bail,
                    CellId = prisionerDto.CellId,
                    Mails = mails
                    
                };

                sb.AppendLine($"Imported {prisoner.FullName} {prisoner.Age} years old");
                prisoners.Add(prisoner);
            }

            context.Prisoners.AddRange(prisoners);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            var serialaizer = new XmlSerializer(typeof(OfficerDto[]), new XmlRootAttribute("Officers"));
            var deserialaizedOfficerDtos = (OfficerDto[])serialaizer.Deserialize(new StringReader(xmlString));
            var officerPrisoners = new List<OfficerPrisoner>();

            var sb = new StringBuilder();
            var officers = new List<Officer>();


            foreach (var officerDto in deserialaizedOfficerDtos)
            {

                Weapon weapon;
                Position position;

                var isWeaponValid = Enum.TryParse(officerDto.Weapon, out weapon);
                var isPositionValid = Enum.TryParse(officerDto.Position, out position);


                if (!IsValid(officerDto) || !isWeaponValid || !isPositionValid)
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }


               var officer = new Officer()
                {
                    DepartmentId = officerDto.DepartmentId,
                    FullName = officerDto.Name,
                    Position = position,
                    Weapon = weapon,
                    Salary = officerDto.Money,
                   
                
                };

                foreach (var prisonerXml in officerDto.Prisoners)
                {
                    var officerPrisoner = new OfficerPrisoner()
                    {
                        PrisonerId = prisonerXml.id,
                        Officer = officer
                    };

                    officerPrisoners.Add(officerPrisoner);
                }

                officers.Add(officer);
                sb.AppendLine($"Imported {officer.FullName} ({officerDto.Prisoners.Length} prisoners)");
            }

            context.Officers.AddRange(officers);
            context.SaveChanges();

            context.OfficersPrisoners.AddRange(officerPrisoners);
            context.SaveChanges();


            return sb.ToString().TrimEnd();
        }

        public static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);

            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, validationResult, true);
        }

    }
}