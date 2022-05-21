﻿using Domain.Helper;
using Domain.Integrations;
using Domain.Integrations.Intefaces;
using Domain.Interfaces;
using Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WemaAssessmentCustomerApi.Helper
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(c =>
            {
                c.AddPolicy("CorsPolicy", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
        }

        public static void ConfigureAuthJWT(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {

                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    var issuer = configuration["AppSettings:Issuer"];
                    var key = configuration["AppSettings:Key"];
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuer = true,
                        // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
      
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        
        }
        public static void AppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
        }

        public static void DependencyInjection(this IServiceCollection services)
        {
            // configure DI for application services
            services.AddScoped<IAlatIntegration, AlatIntegration>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOtpService, OtpService>();
            services.AddScoped<IStateAndLgaService, StateAndLgaService>();
            services.AddScoped<IBankService, BankService>();
        }




    }
}
