using Domain.DataTransferObjects.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
   public interface IOtpService
    {
        GobalResponse<string> SendOtp(string message, string phoneNumber);
        string GenerateOtp();
    }
}
