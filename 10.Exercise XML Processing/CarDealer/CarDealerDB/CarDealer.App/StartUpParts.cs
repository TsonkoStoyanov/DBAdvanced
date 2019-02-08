//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Xml.Serialization;
//using AutoMapper;
//using CarDealer.App.Dto.Import;
//using CarDealer.Data;
//using CarDealer.Models;

//namespace CarDealer.App
//{
//    class StartUpParts
//    {
//        static void Main()
//        {
//            var config = new MapperConfiguration(cfg => { cfg.AddProfile<CarDealerProfile>(); });

//            var mapper = config.CreateMapper();


//            var xmlString = File.ReadAllText("../../../Xml/parts.xml");

//            var serializer = new XmlSerializer(typeof(PartDto[]), new XmlRootAttribute("parts"));

//            var deserializedParts = (PartDto[])serializer.Deserialize(new StringReader(xmlString));

//            var parts = new List<Part>();

//            foreach (var partDto in deserializedParts)
//            {
//                var part = mapper.Map<Part>(partDto);
//                var supplierId = new Random().Next(1, 32);

//                part.SupplierId = supplierId;

//                parts.Add(part);

//            }

//            var context = new CarDealerContext();
//            context.Parts.AddRange(parts);
//            context.SaveChanges();
//        }
//    }
//}