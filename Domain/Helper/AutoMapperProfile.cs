using AutoMapper;
using Data.Models;
using Domain.DataTransferObjects.Requests.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddCustomer, Customer>().ReverseMap();
        }
    }
}
