using AuthCrudApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging(builder => builder.AddConsole());
builder.Services.AddLogging(logbuilder =>
{
    logbuilder.AddConsole(options=>
    {
        options.TimestampFormat = "[HH:MM:SS]";
    });
    logbuilder.AddEventLog(options =>
    {
        options.LogName = "Application";
    });
});
//builder.Host.ConfigureLogging(logging =>
//{
//    logging.ClearProviders();
//    logging.AddConsole();
//});
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var provider = builder.Services.BuildServiceProvider();

var config = provider.GetRequiredService<IConfiguration>();

builder.Services.AddDbContext<AuthDBContext>(item => item.UseSqlServer(config.GetConnectionString("dbcs2")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Employee/Error");
}
app.UseSession();

app.UseStaticFiles();

app.UseRouting();


app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
