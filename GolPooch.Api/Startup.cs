using System;
using Elk.Core;
using System.Text;
using Elk.AspNetCore;
using Newtonsoft.Json;
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
        private const string AllowedOrigins = "_Origins";
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
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
                options.ReturnHttpNotAcceptable = true;
                // option.Filters.Add(typeof(ModelValidationFilter));
            })
            .AddJsonOptions(options =>
            {
                //options.JsonSerializerOptions.MaxDepth = 2;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            })
            .AddXmlSerializerFormatters();

            services.AddElkAuthentication();

            services.AddElkJwtConfiguration(_jwtSettings);

            services.AddCors(options =>
            {
                options.AddPolicy(AllowedOrigins, builder =>
                {
                    builder
                        .WithOrigins(_config.GetSection("AllowOrigin").Value.Split(";"))
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            services.AddMemoryCache();

            services.Configure<JwtSettings>(_config.GetSection("JwtSetting"));
            services.AddTransient<IJwtService, JwtService>();

            services.AddTransient<AuthFilter>();
            services.AddTransient<AuthorizeFilter>();

            services.AddTransient(_config);
            services.AddScoped(_config);
            services.AddSingleton(_config);

            //services.AddHostedService<QuartzHostedService>();

            services.AddElkSwagger(_swaggerSetting);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseElkCrossOriginResource();

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
            app.UseCors(AllowedOrigins);
            app.UseRouting();
            app.UseMvcWithDefaultRoute();
        }
    }
}