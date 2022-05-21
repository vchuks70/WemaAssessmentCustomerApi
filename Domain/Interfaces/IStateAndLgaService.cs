using Data.Models;
using Domain.DataTransferObjects.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
  public  interface IStateAndLgaService
    {
        Task<GobalResponse<IEnumerable<State>>> GetAllState();
        Task<GobalResponse<IEnumerable<Local>>> GetLocalByStateId(int StateId);
    }
}
    