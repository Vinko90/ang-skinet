using Skinet.Infrastructure.Extensions;
using Skinet.Web.Extensions;
using Skinet.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);
{
    var services = builder.Services;

    services.AddApplicationServices();              //App services
    services.AddPersistence(builder.Configuration); //DB
    services.AddRepositories();                     //Repos
}
var app = builder.Build();
{
    app.UseMiddleware<ExceptionMiddleware>();
    
    app.UseStatusCodePagesWithReExecute("/errors/{0}");
    
    if (!app.Environment.IsDevelopment())
    {
        app.UseHsts();
    }
    
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");

    app.MapFallbackToFile("index.html");

    app.MigrateDatabase();
}

app.Run();
