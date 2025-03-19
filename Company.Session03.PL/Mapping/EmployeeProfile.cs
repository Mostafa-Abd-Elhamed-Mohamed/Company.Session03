using AutoMapper;
using Company.Session03.DAL.Models;
using Company.Session03.PL.Dtos;

namespace Company.Session03.PL.Mapping
{
    public class EmployeeProfile : Profile
    {

        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeDtos,Employee>();
        }

    }
}
