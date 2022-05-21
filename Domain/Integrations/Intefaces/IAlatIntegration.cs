using Domain.DataTransferObjects.Responses.Alat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Integrations.Intefaces
{
  public  interface IAlatIntegration
    {
        Task<GetAllBanksResponse> GetAllBanks();
    }
}
