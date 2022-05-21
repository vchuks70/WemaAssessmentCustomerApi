using Domain.DataTransferObjects.Global;
using Domain.DataTransferObjects.Responses.Alat;
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
    public class BankController : ControllerBase    
    {
        public IBankService BankService { get; set; }

        public BankController(IBankService bankService)
        {
            BankService = bankService;
        }

        [HttpGet("all")]
        [ProducesResponseType((StatusCodes.Status200OK))]
        [ProducesResponseType((StatusCodes.Status400BadRequest))]
        public async Task<ActionResult<GobalResponse<IEnumerable<Bank>>>> GetAllBanks()
        {
            var response = await BankService.GetAllBanks();
            return response.ResponseCode == "00" ? Ok(response) : BadRequest(response);
        }
    }
}
