using Microsoft.OpenApi.Models;

namespace Skinet.Web.Extensions;

public static class SwaggerServiceExtensions
{
    public static void AddSwaggerDocumentation(this IServiceCollection services)
    {
        //Swagger
        services.AddEndpointsApiExplorer();
        
        services.AddSwaggerGen(c =>
        {
            var securitySchema = new OpenApiSecurityScheme
            {
                Description = "JWT Auth Bearer Scheme",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };
            c.AddSecurityDefinition("Bearer", securitySchema);

            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    securitySchema, new[] {"Bearer"}
                }
            };
            c.AddSecurityRequirement(securityRequirement);
        });
    }

    public static void UseSwaggerDocumentation(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}
