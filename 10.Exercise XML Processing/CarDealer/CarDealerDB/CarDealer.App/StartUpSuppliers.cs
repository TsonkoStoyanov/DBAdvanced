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
//    class StartUpSuppliers
//    {
//        static void Main()
//        {
//            var config = new MapperConfiguration(cfg => { cfg.AddProfile<CarDealerProfile>(); });

//            var mapper = config.CreateMapper();


//            var xmlString = File.ReadAllText("../../../Xml/suppliers.xml");

//            var serializer = new XmlSerializer(typeof(SupplierDto[]), new XmlRootAttribute("suppliers"));

//            var deserializedSupplier = (SupplierDto[])serializer.Deserialize(new StringReader(xmlString));

//            var suppliers = new List<Supplier>();

//            foreach (var supplierDto in deserializedSupplier)
//            {
//                var supplier = mapper.Map<Supplier>(supplierDto);
//                suppliers.Add(supplier);

//            }

//            var context = new CarDealerContext();
//            context.Suppliers.AddRange(suppliers);
//            context.SaveChanges();
//        }
//    }
//}
