using Data.Models;
using Domain.DataTransferObjects.Global;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class StateAndLgaService : IStateAndLgaService
    {
        private readonly ApplicationDbContext _dataContext;

        public StateAndLgaService(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<GobalResponse<IEnumerable<State>>> GetAllState()
        {
            var states = await _dataContext.States.ToListAsync();
            return new GobalResponse<IEnumerable<State>>
            {
                ResponseCode = "00",
                ResponseMessage = "successful",
                Data = states
            };

        }

        public async Task<GobalResponse<IEnumerable<Local>>> GetLocalByStateId(int StateId)
        {
            var state = await _dataContext.States.FirstOrDefaultAsync(x => x.Id == StateId);
            if (state is null)
            {
                return new GobalResponse<IEnumerable<Local>>
                {
                    ResponseCode = "99",    
                    ResponseMessage = "Invalid State"
                };
            }

            var local = await _dataContext.Locals.Where(x => x.StateId == state.Id).ToListAsync();

            return new GobalResponse<IEnumerable<Local>>
            {
                ResponseCode = "00",
                ResponseMessage = "successful",
                Data = local
            };
        }
    }
}
