using Skinet.Infrastructure.Extensions;
using Skinet.Web.Extensions;
using Skinet.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);
{
    var services = builder.Services;

    services.AddApplicationServices(builder.Configuration); //App services
    services.AddPersistence(builder.Configuration);         //Store DB
    services.AddIdentityPersistence(builder.Configuration); //Identity DB
    services.AddRepositoriesAndServices();                  //Repos & Services
    services.AddSwaggerDocumentation();                     //Swagger
}
var app = builder.Build();
{
    app.UseMiddleware<ExceptionMiddleware>();
    
    app.UseStatusCodePagesWithReExecute("/errors/{0}");
    
    if (!app.Environment.IsDevelopment())
    {
        app.UseHsts();
    }

    app.UseSwaggerDocumentation();

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseCors("CorsPolicy");
    
    app.UseRouting();
    
    app.UseAuthentication();
    app.UseAuthorization();
    
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");

    app.MapFallbackToFile("index.html");

    app.MigrateDatabases();     //Migrate Both DB's
}

app.Run();
