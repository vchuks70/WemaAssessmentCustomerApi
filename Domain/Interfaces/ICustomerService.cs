using Data.Models;
using Domain.DataTransferObjects.Global;
using Domain.DataTransferObjects.Requests.Customer;
using Domain.DataTransferObjects.Responses.Alat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
   public interface ICustomerService
    {
        Task<GobalResponse<Guid>> CreateCustomer (AddCustomer request);
        Task<GobalResponse<IEnumerable<Customer>>> GetAllCustomers();
        Task<GobalResponse<string>> VerifyOtp(VerifyOtpRequest request);
        Task<GobalResponse<string>> ResendOtp(ResendOtpRequest request);    
    }
}
