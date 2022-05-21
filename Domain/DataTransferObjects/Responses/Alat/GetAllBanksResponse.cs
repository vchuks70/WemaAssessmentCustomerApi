using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataTransferObjects.Responses.Alat
{
    public class GetAllBanksResponse
    {
        public IEnumerable<Bank> result { get; set; }
        public string errorMessage { get; set; }
        public List<string> errorMessages { get; set; }
        public bool hasError { get; set; }
        public DateTime timeGenerated { get; set; }
    }
    public class Bank
    {
        public string bankName { get; set; }
        public string bankCode { get; set; }
    }

}
