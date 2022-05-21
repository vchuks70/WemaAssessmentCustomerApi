using AutoMapper;
using Data.Models;
using Domain.DataTransferObjects.Global;
using Domain.DataTransferObjects.Requests.Customer;
using Domain.DataTransferObjects.Responses.Alat;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;
using Domain.Helper;

namespace Domain.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IOtpService _otpService;

        public CustomerService(ApplicationDbContext dataContext, IMapper mapper, IOtpService otpService)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _otpService = otpService;
        }

        public async Task<GobalResponse<Guid>> CreateCustomer(AddCustomer request)
        {
            // validate email

            if (!EmailValidator.IsValid(request.Email))
            {
                return new GobalResponse<Guid>
                {
                    ResponseCode = "99",
                    ResponseMessage = "Invalid email"
                };
            }


            // Check if email exist
            var existingCustomer = await _dataContext.Customers.FirstOrDefaultAsync(x => x.Email == request.Email);

            if(existingCustomer is not null)
            {
                return new GobalResponse<Guid>
                {
                    ResponseCode = "5003",
                    ResponseMessage = "User email already exist"
                };
            }

            // Check State and Local Government

            var state = await _dataContext.States.FirstOrDefaultAsync(state => state.Id == request.StateOfResidenceId);
            if (state is null)
            {
                return new GobalResponse<Guid>
                {
                    ResponseCode = "99",
                    ResponseMessage = "Invalid State"
                };
            }

            var local = await _dataContext.Locals.FirstOrDefaultAsync(local => local.Id == request.LocalId && local.StateId == request.StateOfResidenceId);
            if (local is null)
            {
                return new GobalResponse<Guid>
                {
                    ResponseCode = "99",
                    ResponseMessage = "Invalid LGA"
                };
            }


            var newCustomer = _mapper.Map<Customer>(request);

            newCustomer.StateOfResidence = state;
            newCustomer.LGA = local;
            newCustomer.PasswordHash = BC.HashPassword(request.Password);
            newCustomer.isVerified = false;

            var otp = new Otp
            {
                DateCreated = DateTime.Now,
                isUsed = false,
                isDisabled = false,
                Value = _otpService.GenerateOtp(),
                validilityTime = DateTime.Now.AddMinutes(10)
            };




            newCustomer.Otps.Add(otp);
            var message = $"your resgistration OTP is {otp.Value}";
            var sendGridResponse = _otpService.SendOtp(message, newCustomer.PhoneNumber);

            if (sendGridResponse.ResponseCode != "00")
            {
                return new GobalResponse<Guid>
                {
                    ResponseCode = "99",
                    ResponseMessage = "Internal error, OTP not delivered"
                };
            }

            await _dataContext.Customers.AddAsync(newCustomer);

            await _dataContext.SaveChangesAsync();

            return new GobalResponse<Guid>
            {
                ResponseCode = "00",
                Data = newCustomer.Id,
                ResponseMessage = "Successful, Kindly activate your account with the otp sent to your mobile number"
            };

        }

        public async Task<GobalResponse<IEnumerable<Customer>>> GetAllCustomers()
        {
            var customers = await _dataContext.Customers.ToListAsync();
            return new GobalResponse<IEnumerable<Customer>>
            {
                ResponseCode = "00",
                ResponseMessage = "successful",
                Data = customers
            };
        }

        public async Task<GobalResponse<string>> ResendOtp(ResendOtpRequest request)
        {
            // validate email

            if (!EmailValidator.IsValid(request.Email))
            {
                return new GobalResponse<string>
                {
                    ResponseCode = "99",
                    ResponseMessage = "Invalid email"
                };
            }

            // Check if email exist

            var existingCustomer = await _dataContext.Customers.Include(x => x.Otps).FirstOrDefaultAsync(x => x.Email == request.Email);

            if (existingCustomer is null)
            {
                return new GobalResponse<string>
                {
                    ResponseCode = "5003",
                    ResponseMessage = "Customer not found"
                };
            }


            if (existingCustomer.isVerified)
            {
                return new GobalResponse<string>
                {
                    ResponseCode = "01",
                    ResponseMessage = "Customer is already verified"
                };
            }

            foreach (var item in existingCustomer.Otps)
            {
                item.isDisabled = true;

            }

            var otp = new Otp
            {
                DateCreated = DateTime.Now,
                isUsed = false,
                isDisabled = false,
                Value = _otpService.GenerateOtp(),
                validilityTime = DateTime.Now.AddMinutes(10)
            };




            existingCustomer.Otps.Add(otp);
            var message = $"your resgistration OTP is {otp.Value}";
            var sendGridResponse = _otpService.SendOtp(message, existingCustomer.PhoneNumber);

            if (sendGridResponse.ResponseCode != "00")
            {
                return new GobalResponse<string>
                {
                    ResponseCode = "99",
                    ResponseMessage = "Internal error, OTP not delivered"
                };
            }

            _dataContext.UpdateRange(existingCustomer);
            await _dataContext.SaveChangesAsync();

            return new GobalResponse<string>
            {
                ResponseCode = "00",
                ResponseMessage = "successful",
                Data = "Successful, otp resent to your mobile number"
            };

        }

        public async Task<GobalResponse<string>> VerifyOtp(VerifyOtpRequest request)
        {
            // validate email

            if (!EmailValidator.IsValid(request.Email))
            {
                return new GobalResponse<string>
                {
                    ResponseCode = "99",
                    ResponseMessage = "Invalid email"
                };
            }

            // Check if email exist

            var existingCustomer = await _dataContext.Customers.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (existingCustomer is null)
            {
                return new GobalResponse<string>
                {
                    ResponseCode = "5003",
                    ResponseMessage = "Customer not found"
                };
            }


            if (existingCustomer.isVerified)
            {
                return new GobalResponse<string>
                {
                    ResponseCode = "01",
                    ResponseMessage = "Customer is already verified"
                };
            }


            // get otp
            var otp = await _dataContext.Otps.FirstOrDefaultAsync(x => x.Value == request.Token && x.CustomerId == existingCustomer.Id && x.isDisabled == false && x.isUsed == false);
            if (otp is null)
            {
                return new GobalResponse<string>
                {
                    ResponseCode = "5003",
                    ResponseMessage = "otp not found"
                };
            }

            if (otp.isUsed || otp.isDisabled)
            {
                return new GobalResponse<string>
                {
                    ResponseCode = "99",
                    ResponseMessage = "otp is no longer valid, kindly request another"
                };
            }

            if (otp.validilityTime < otp.DateCreated)
            {
                return new GobalResponse<string>
                {
                    ResponseCode = "99",
                    ResponseMessage = "otp as expired, kindly request another"
                };
            }
            otp.DateUsed = DateTime.Now;
            otp.isUsed = true;
            otp.isDisabled = true;
            existingCustomer.isVerified = true;
            await _dataContext.SaveChangesAsync();
            return new GobalResponse<string>
            {
                ResponseCode = "00",
                ResponseMessage = "successful",
                Data = "otp verification successful"
            };
        }
    }
}
