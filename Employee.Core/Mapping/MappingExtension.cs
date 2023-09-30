using AutoMapper;
using Employee.Model;
using Employee.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core.Mapping
{
    public  class MappingExtension:Profile
    {
        public MappingExtension()
        {
            CreateMap<VMEmployee, Employees>().ReverseMap();
            CreateMap<VMCountry, Country>().ReverseMap();
            CreateMap<VMState, State>().ReverseMap();

        }
    }
}
