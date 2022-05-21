using Data.Models;
using Domain.DataTransferObjects.Global;
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
    public class StateAndLgaController : ControllerBase
    {
        public IStateAndLgaService StateAndLgaService { get; set; }

        public StateAndLgaController(IStateAndLgaService stateAndLgaService)
        {
            StateAndLgaService = stateAndLgaService;
        }

        [HttpGet("states")]
        [ProducesResponseType((StatusCodes.Status200OK))]
        [ProducesResponseType((StatusCodes.Status400BadRequest))]
        public async Task<ActionResult<GobalResponse<IEnumerable<State>>>> GetStates()
        {
            var states = await StateAndLgaService.GetAllState();
            return Ok(states);
        }
            

        [HttpGet("states/{stateId}/lga")]
        [ProducesResponseType((StatusCodes.Status200OK))]
        [ProducesResponseType((StatusCodes.Status400BadRequest))]
        public async Task<ActionResult<GobalResponse<IEnumerable<Local>>>> GetStates([FromRoute]int stateId)
        {
            var lgas = await StateAndLgaService.GetLocalByStateId(stateId);
            return Ok(lgas);
        }
    }
}
