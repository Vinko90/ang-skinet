using Skinet.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
{
    var services = builder.Services;
    
    services.AddControllersWithViews();
    services.AddPersistence(builder.Configuration); //DB
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");

    app.MapFallbackToFile("index.html");
}

app.Run();
