using System;
using System.Linq;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GolPooch.Api
{
    public class SwaggerIntegration
    {
        //public static OpenApiInfo GetOpenApiInfo(string version) => new OpenApiInfo
        //{
        //    Title = "GolPooch",
        //    Version = $"v{version}",
        //    Description = $"Copyright © {DateTime.Now.Year} Avanod Company. All rights reserved.",
        //    TermsOfService = new Uri("https://Avanod.com/"),

        //    Contact = new OpenApiContact
        //    {
        //        Name = "Mohammad Karimi",
        //        Email = "M.Karimi@avanod.com",
        //        Url = new Uri("https://Avanod.com/")
        //    },
        //    License = new OpenApiLicense
        //    {
        //        Name = "Avanod Service Licence",
        //        Url = new Uri("https://Avanod.com/applicense")
        //    }
        //};

        //public static void AddSwaggerGen(this IServiceCollection services)
        //{
        //    services.AddSwaggerGen(c =>
        //    {
        //        c.SwaggerDoc("v1", GetOpenApiInfo("1.0"));
        //        c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
        //    });
        //}

        //public static void AddSwagger(this IApplicationBuilder app)
        //{
        //    app.UseSwagger();
        //    app.UseSwaggerUI(c =>
        //    {
        //        c.DefaultModelExpandDepth(2);
        //        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GolPooch API - v1.0");
        //        c.RoutePrefix = "help";
        //    });
        //}
        
    }
}