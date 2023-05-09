using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using UK.NHS.CookieBanner.Controllers;
using UK.NHS.CookieBanner.DataServices;
using UK.NHS.CookieBanner.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<IGenericApiHttpClient, GenericApiHttpClient>();
builder.Services.AddScoped<ICookiePolicyService, CookiePolicyAPIService>();
//builder.Services.AddScoped<ICookiePolicyService, CookiePolicyService>();

string defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped<IDbConnection>(_ => new SqlConnection(defaultConnectionString));
builder.Services.AddScoped<ICookiePolicyService, CookiePolicyDBService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=CookieConsent}/{action=CookiePolicy}/{id?}");

app.Run();
