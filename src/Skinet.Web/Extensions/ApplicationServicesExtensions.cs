using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Skinet.Web.Errors;
using Skinet.Web.Helpers;

namespace Skinet.Web.Extensions;

public static class ApplicationServicesExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        //Controllers
        services.AddControllersWithViews();
        
        //Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        //AutoMapper
        services.AddAutoMapper(typeof(MappingProfiles));
        
        //Api Behavior
        services.Configure<ApiBehaviorOptions>(opt =>
        {
            opt.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();

                var errorResponse = new ApiValidationErrorResponse
                {
                    Errors = errors
                };

                return new BadRequestObjectResult(errorResponse);
            };
        });
        
        //Enable Cors
        services.AddCors(opt =>
        {
            opt.AddPolicy("CorsPolicy", policy =>
            {
                policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
            });
        });
        
        //Configure Auth
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"]!)),
                    ValidIssuer = config["Token:Issuer"],
                    ValidateAudience = false,
                    ValidateIssuer = true
                };
            });
        
        
        services.AddAuthorization();
    }
}
