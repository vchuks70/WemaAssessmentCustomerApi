using Domain.DataTransferObjects.Global;
using Domain.Integrations;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class OtpService : IOtpService
    {
        public string GenerateOtp()
        {
            var chars1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            var stringChars1 = new char[6];
            var random1 = new Random();

            for (int i = 0; i < stringChars1.Length; i++)
            {
                stringChars1[i] = chars1[random1.Next(chars1.Length)];
            }

            var otp = new String(stringChars1);
            return otp;
        }

        public GobalResponse<string> SendOtp(string message, string phoneNumber)
        {
            var response = SendGridIntegration.SendMessage(message, phoneNumber);
            return response;
        }
    }
}
