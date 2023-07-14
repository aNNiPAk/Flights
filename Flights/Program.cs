using Flights.Data;
using Flights.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add db context
builder.Services.AddDbContext<Entities>(options => options.UseInMemoryDatabase("Flights"),
    ServiceLifetime.Singleton);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen(c =>
{
    c.AddServer(new OpenApiServer
    {
        Description = "Development Server",
        Url = "https://localhost:7127"
    });

    c.CustomOperationIds(e =>
        $"{e.ActionDescriptor.RouteValues["action"] + e.ActionDescriptor.RouteValues["controller"]}");
});

builder.Services.AddSingleton<Entities>();

var app = builder.Build();

var entities = app.Services.CreateScope().ServiceProvider.GetService<Entities>();
var random = new Random();

Flight[] flightsToSeed =
{
    new(Guid.NewGuid(),
        "American Airlines",
        new TimePlace("Istanbul", DateTime.Now.AddHours(random.Next(4, 10))),
        new TimePlace("Los Angeles", DateTime.Now.AddHours(random.Next(1, 3))),
        random.Next(90, 5000).ToString(),
        random.Next(1, 853)),
    new(Guid.NewGuid(),
        "Deutsche BA",
        new TimePlace("Schiphol", DateTime.Now.AddHours(random.Next(4, 15))),
        new TimePlace("Munchen", DateTime.Now.AddHours(random.Next(1, 10))),
        random.Next(90, 5000).ToString(),
        random.Next(1, 853)),
    new(Guid.NewGuid(),
        "British Airways",
        new TimePlace("Vizzola-Ticino", DateTime.Now.AddHours(random.Next(4, 18))),
        new TimePlace("London, England", DateTime.Now.AddHours(random.Next(1, 15))),
        random.Next(90, 5000).ToString(),
        random.Next(1, 853)),
    new(Guid.NewGuid(),
        "Basiq Air",
        new TimePlace("Glasgow, Scotland", DateTime.Now.AddHours(random.Next(4, 21))),
        new TimePlace("Amsterdam", DateTime.Now.AddHours(random.Next(1, 21))),
        random.Next(90, 5000).ToString(),
        random.Next(1, 853)),
    new(Guid.NewGuid(),
        "BB Heliag",
        new TimePlace("Baku", DateTime.Now.AddHours(random.Next(4, 25))),
        new TimePlace("Zurich", DateTime.Now.AddHours(random.Next(1, 23))),
        random.Next(90, 5000).ToString(),
        random.Next(1, 853))
};

entities?.Flights.AddRange(flightsToSeed);

entities?.SaveChanges();

app.UseCors(policyBuilder => policyBuilder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod());
app.UseSwagger().UseSwaggerUI();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    "default",
    "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();