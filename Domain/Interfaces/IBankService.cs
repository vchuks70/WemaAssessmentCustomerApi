using Domain.DataTransferObjects.Global;
using Domain.DataTransferObjects.Responses.Alat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface  IBankService
    {
        Task<GobalResponse<IEnumerable<Bank>>> GetAllBanks();
    }
}
