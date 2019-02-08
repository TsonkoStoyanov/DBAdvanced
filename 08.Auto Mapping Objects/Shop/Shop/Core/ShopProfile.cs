using AutoMapper;
using Shop.App.Core.Dtos;
using Shop.Models;

namespace Shop.App.Core
{
    public class ShopProfile: Profile
    {
        public ShopProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();

        }
    }
}