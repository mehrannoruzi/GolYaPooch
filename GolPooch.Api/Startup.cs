using System;
using Elk.Core;
using System.Text;
using Elk.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System.Text.Json.Serialization;
using GolPooch.DependencyResolver.Ioc;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GolPooch.Api
{
    public class Startup
    {
        private IConfiguration _config { get; }
        private JwtSettings _jwtSettings { set; get; }
        private SwaggerSetting _swaggerSetting { set; get; }

        public Startup(IConfiguration configuration)
        {
            _config = configuration;

            _jwtSettings = new JwtSettings
            {
                SecretKey = _config["JwtSetting:SecretKey"],
                Encryptionkey = _config["JwtSetting:Encryptionkey"],
                Issuer = _config["JwtSetting:Issuer"],
                Audience = _config["JwtSetting:Audience"],
                NotBeforeMinutes = int.Parse(_config["JwtSetting:NotBeforeMinutes"]),
                ExpirationMinutes = int.Parse(_config["JwtSetting:ExpirationMinutes"]),
            };

            _swaggerSetting = new SwaggerSetting
            {
                Name = "GolPooch API - v1.0",
                Title = "GolPooch",
                Version = "v1.0",
                Description = $"Copyright © {DateTime.Now.Year} Avanod Company. All rights reserved.",
                TermsOfService = "https://Avanod.com/",
                JsonUrl = "/swagger/v1/swagger.json",
                Contact = new SwaggerContact
                {
                    Name = "Mohammad Karimi",
                    Email = "M.Karimi@avanod.com",
                    Url = "https://Avanod.com/"
                },
                License = new SwaggerLicense
                {
                    Name = "Avanod Service Licence",
                    Url = "https://Avanod.com/applicense"
                }
            };
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option =>
            {
                option.EnableEndpointRouting = false;
                option.ReturnHttpNotAcceptable = true;
                // option.Filters.Add(typeof(ModelValidationFilter));
            })
            .AddXmlSerializerFormatters()
            .AddJsonOptions(opts =>
            {
                //opts.JsonSerializerOptions.MaxDepth = 2;
                opts.JsonSerializerOptions.PropertyNamingPolicy = null;
                opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddElkAuthentication();

            services.AddElkJwtConfiguration(_jwtSettings);

            services.AddMemoryCache();

            services.Configure<JwtSettings>(_config.GetSection("JwtSetting"));
            services.AddTransient<IJwtService, JwtService>();

            services.AddTransient<AuthFilter>();
            services.AddTransient<AuthorizeFilter>();

            services.AddTransient(_config);
            services.AddScoped(_config);
            services.AddSingleton(_config);

            services.AddElkSwagger(_swaggerSetting);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseElkCrossOriginResource();

            app.UseElkSwaggerConfiguration(_swaggerSetting);

            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var errorhandler = context.Features.Get<IExceptionHandlerPathFeature>();
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/Json";
                    var bytes = Encoding.ASCII.GetBytes(new Response<object> { IsSuccessful = false, Message = errorhandler.Error?.Message, ResultCode = 500 }.SerializeToJson());
                    await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                });
            });

            app.UseMiddleware<JwtParserMiddleware>();

            app.UseElkJwtConfiguration();

            app.UseRouting();

            app.UseMvcWithDefaultRoute();
        }
    }
}