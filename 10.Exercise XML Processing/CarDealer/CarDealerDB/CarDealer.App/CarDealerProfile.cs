using AutoMapper;
using CarDealer.App.Dto.Import;
using CarDealer.Models;

namespace CarDealer.App
{
    public class CarDealerProfile:Profile
    {
        public CarDealerProfile()
        {
            CreateMap<SupplierDto, Supplier>();
            CreateMap<PartDto, Part>();
            CreateMap<CarDto, Car>();


        }
        
    }
}