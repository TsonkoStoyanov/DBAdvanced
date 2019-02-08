using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using AutoMapper;
using Newtonsoft.Json;
using PetClinic.DataProcessor.Dto.Import;
using PetClinic.Models;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace PetClinic.DataProcessor
{
    using System;

    using PetClinic.Data;

    public class Deserializer
    {

        public static string ImportAnimalAids(PetClinicContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            List<AnimalAid> animalAids = new List<AnimalAid>();

            var deserilaizedAnimalAids = JsonConvert.DeserializeObject<animalAidDto[]>(jsonString);

            foreach (var animalAidDto in deserilaizedAnimalAids)
            {
                if (!IsValid(animalAidDto))
                {
                    sb.AppendLine($"Error: Invalid data.");
                    continue;
                }

                bool isExist = animalAids.Any(x => x.Name == animalAidDto.Name);

                if (isExist)
                {
                    sb.AppendLine($"Error: Invalid data.");
                    continue;
                }

                AnimalAid animalAid = new AnimalAid()
                {
                    Name = animalAidDto.Name,
                    Price = animalAidDto.Price
                };

                animalAids.Add(animalAid);
                sb.AppendLine($"Record {animalAid.Name} successfully imported.");
            }

            context.AnimalAids.AddRange(animalAids);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportAnimals(PetClinicContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            List<Animal> animals = new List<Animal>();
            List<Passport> passports = new List<Passport>();

            var deserialaizedAnimals = JsonConvert.DeserializeObject<AnimalDto[]>(jsonString);

            foreach (var animalDto in deserialaizedAnimals)
            {
                if (!IsValid(animalDto))
                {
                    sb.AppendLine($"Error: Invalid data.");
                    continue;
                }

                if (!IsValid(animalDto.Passport))
                {
                    sb.AppendLine($"Error: Invalid data.");
                    continue;
                }

                bool ifExistPassport = passports.Any(x => x.SerialNumber == animalDto.Passport.SerialNumber);

                if (ifExistPassport)
                {
                    sb.AppendLine($"Error: Invalid data.");
                    continue;
                }

                Animal animal = new Animal()
                {
                    Name = animalDto.Name,
                    Type = animalDto.Type,
                    Age = animalDto.Age,
                    PassportSerialNumber = animalDto.Passport.SerialNumber

                };

                Passport passport = new Passport()
                {
                    Animal = animal,
                    SerialNumber = animalDto.Passport.SerialNumber,
                    OwnerName = animalDto.Passport.OwnerName,
                    OwnerPhoneNumber = animalDto.Passport.OwnerPhoneNumber,
                    RegistrationDate = DateTime.ParseExact(animalDto.Passport.RegistrationDate, "dd-MM-yyyy", CultureInfo.InvariantCulture)

                };

                animals.Add(animal);
                passports.Add(passport);
                sb.AppendLine($"Record {animal.Name} Passport №: {passport.SerialNumber} successfully imported.");

            }
            context.Animals.AddRange(animals);
            context.Passports.AddRange(passports);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportVets(PetClinicContext context, string xmlString)
        {

            StringBuilder sb = new StringBuilder();
            List<Vet> vets = new List<Vet>();

            var serialaizer = new XmlSerializer(typeof(VetDto[]), new XmlRootAttribute("Vets"));
            var deserialaizedXml = (VetDto[])serialaizer.Deserialize(new StringReader(xmlString));

            foreach (var vetDto in deserialaizedXml)
            {
                bool isExist = vets.Any(x => x.PhoneNumber == vetDto.PhoneNumber);

                if (!IsValid(vetDto) || isExist)
                {
                    sb.AppendLine($"Error: Invalid data.");
                    continue;
                }

                var vet = Mapper.Map<Vet>(vetDto);

                vets.Add(vet);
                sb.AppendLine($"Record {vet.Name} successfully imported.");
            }
            context.AddRange(vets);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportProcedures(PetClinicContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            List<Procedure> validProcedures = new List<Procedure>();

            var serialaizer = new XmlSerializer(typeof(ProcedureDto[]), new XmlRootAttribute("Procedures"));
            var deserialaizedXml = (ProcedureDto[])serialaizer.Deserialize(new StringReader(xmlString));

            foreach (var procedureDto in deserialaizedXml)
            {
                var vetExist = context.Vets.FirstOrDefault(x => x.Name == procedureDto.Vet);
                var animalExist = context.Animals.FirstOrDefault(x => x.PassportSerialNumber == procedureDto.Animal);

                bool allAnimalAidExist = true;

                List<ProcedureAnimalAid> validProcedureAnimalAids = new List<ProcedureAnimalAid>();


                foreach (var peocedureAnimalAid in procedureDto.AnimalAids)
                {
                    var animalAid = context.AnimalAids.FirstOrDefault(x => x.Name == peocedureAnimalAid.Name);

                    if (animalAid == null||validProcedureAnimalAids.Any(p=>p.AnimalAid.Name == peocedureAnimalAid.Name))
                    {
                        allAnimalAidExist = false;
                        break;
                    }

                    var validAnimalAid = new ProcedureAnimalAid()
                    {
                        AnimalAid = animalAid
                    };

                    validProcedureAnimalAids.Add(validAnimalAid);
                }


                if (!IsValid(procedureDto) || vetExist == null || animalExist == null || !allAnimalAidExist
                    || !procedureDto.AnimalAids.All(IsValid))
                {
                    sb.AppendLine($"Error: Invalid data.");
                    continue;
                }

                Procedure procedure = new Procedure()
                {
                    Animal = animalExist,
                    Vet = vetExist,
                    DateTime = DateTime.ParseExact(procedureDto.DateTime, "dd-MM-yyyy", CultureInfo.InvariantCulture),
                    ProcedureAnimalAids = validProcedureAnimalAids
                };

                validProcedures.Add(procedure);
                sb.AppendLine($"Record successfully imported.");
            }

            context.Procedures.AddRange(validProcedures);
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
