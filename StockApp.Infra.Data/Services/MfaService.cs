using System;
using StockApp.Application.Interfaces;

namespace StockApp.Infra.Data.Services
{
    public class MfaService : IMfaService
    {
        public string GenerateOtp()
        {
            var otp = new Random().Next(100000, 999999).ToString();

            return otp;
        }

        public bool ValidateOtp(string userOtp, string storeOtp)
        {
            return userOtp == storeOtp;
        }
    }
}
