using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Serialization;
using AutoMapper;
using CarDealer.App.Dto.Import;
using CarDealer.Data;
using CarDealer.Models;

namespace CarDealer.App
{
    class StartUpCars
    {
        static void Main()
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile<CarDealerProfile>(); });

            var mapper = config.CreateMapper();


            var xmlString = File.ReadAllText("../../../Xml/cars.xml");

            var serializer = new XmlSerializer(typeof(CarDto[]), new XmlRootAttribute("cars"));

            var deserializedCars = (CarDto[])serializer.Deserialize(new StringReader(xmlString));

            var cars = new List<Car>();
            var partCars = new List<PartCar>();

            foreach (var carDto in deserializedCars)
            {
                var car = mapper.Map<Car>(carDto);

                cars.Add(car);

                var numbersOfParts = new Random().Next(10, 21);

                for (int i = 0; i < numbersOfParts; i++)
                {
                    var part = new Random().Next(1, 31);

                    var partCar = new PartCar()
                    {
                        CarId = car.Id,
                        PartId = part
                    };

                    partCars.Add(partCar);
                }

            }

            var context = new CarDealerContext();

            context.Cars.AddRange(cars);

            context.PartCars.AddRange(partCars);

            context.SaveChanges();

        }
    }
}