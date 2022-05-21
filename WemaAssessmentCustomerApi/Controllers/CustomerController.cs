using Data.Models;
using Domain.DataTransferObjects.Global;
using Domain.DataTransferObjects.Requests.Customer;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WemaAssessmentCustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public ICustomerService CustomerService { get; set; }

        public CustomerController(ICustomerService customerService)
        {
            CustomerService = customerService;
        }

        [HttpPost("add-customer")]
        [ProducesResponseType((StatusCodes.Status200OK))]
        [ProducesResponseType((StatusCodes.Status400BadRequest))]
        public async Task<ActionResult<GobalResponse<Guid>>> CreateCustomer([FromBody] AddCustomer customer)
        {
            var response = await CustomerService.CreateCustomer(customer);
            return  response.ResponseCode == "00" ?  Ok(response) : BadRequest(response);
        }


        [HttpGet("all")]
        [ProducesResponseType((StatusCodes.Status200OK))]
        [ProducesResponseType((StatusCodes.Status400BadRequest))]
        public async Task<ActionResult<GobalResponse<IEnumerable<Customer>>>> GetAllCustomers()
        {
            var response = await CustomerService.GetAllCustomers();
            return response.ResponseCode == "00" ? Ok(response) : BadRequest(response);
        }

        [HttpPost("verify-otp")]
        [ProducesResponseType((StatusCodes.Status200OK))]
        [ProducesResponseType((StatusCodes.Status400BadRequest))]
        public async Task<ActionResult<GobalResponse<string>>> VerifyOtp(VerifyOtpRequest request)
        {
            var response = await CustomerService.VerifyOtp(request);
            return response.ResponseCode == "00" ? Ok(response) : BadRequest(response);
        }


        [HttpPost("resend-otp")]
        [ProducesResponseType((StatusCodes.Status200OK))]
        [ProducesResponseType((StatusCodes.Status400BadRequest))]
        public async Task<ActionResult<GobalResponse<string>>> ResendOtp(ResendOtpRequest request)
        {
            var response = await CustomerService.ResendOtp(request);
            return response.ResponseCode == "00" ? Ok(response) : BadRequest(response);
        }

    }
}
