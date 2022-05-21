using Domain.DataTransferObjects.Global;
using Domain.DataTransferObjects.Responses.Alat;
using Domain.Integrations.Intefaces;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
   public class BankService: IBankService
    {
        private readonly IAlatIntegration _IAlatIntegration;

        public BankService(IAlatIntegration iAlatIntegration)
        {
            _IAlatIntegration = iAlatIntegration;
        }
        public async Task<GobalResponse<IEnumerable<Bank>>> GetAllBanks()
        {
            var banks = await _IAlatIntegration.GetAllBanks();
            return new GobalResponse<IEnumerable<Bank>>
            {
                ResponseCode = banks is not null && banks.result.Any() ? "00" : "99",
                ResponseMessage = banks is not null && banks.result.Any() ? "sucessful" : banks.errorMessage ?? "failed, error occured",
                Data = banks.result
            };
        }
    }
}
