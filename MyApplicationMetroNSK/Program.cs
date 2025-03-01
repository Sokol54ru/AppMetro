using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApplicationMetroNSK.Data;
using MyApplicationMetroNSK.Service;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

var connectionString = configuration.GetConnectionString("connectionDB");

services.AddDbContextFactory<AppDbContext>(optionsAction =>
{
    if (string.IsNullOrWhiteSpace(connectionString))
    {
        throw new ArgumentNullException(nameof(connectionString), "Database connection string is empty");
    }
    optionsAction.UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure());
});

services.AddScoped<ITimeCardService, TimeCardService>();
services.AddScoped<ISalaryCalculationService, SalaryCalculationService>();
services.AddAutoMapper(typeof(Program));
builder.Services.AddControllersWithViews();

services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=View}/{action=Main}/{id?}");

app.Run();
