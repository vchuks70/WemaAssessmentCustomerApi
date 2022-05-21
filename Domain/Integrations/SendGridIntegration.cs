using Domain.DataTransferObjects.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Integrations
{
   public static class SendGridIntegration
    {
        public static GobalResponse<string> SendMessage(string message, string phoneNumber)
        {

            // mock otp message
            return new GobalResponse<string>
            {
                ResponseCode = "00",
                ResponseMessage = "Successful",

            };
        }
    }
}
