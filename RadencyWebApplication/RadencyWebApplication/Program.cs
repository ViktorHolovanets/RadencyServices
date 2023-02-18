using Microsoft.EntityFrameworkCore;
using RadencyWebApplication.Models.Db;
using RadencyWebApplication.Models.Db.Seed;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Configuration.AddJsonFile("config.json");
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApiContext>();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();  //Logger write in the Console
builder.Logging.AddDebug();  //Logger write in the Debug

var app = builder.Build();
Seed(app);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
async void Seed(IHost host)
{
    using var scope = host.Services.CreateScope();
    var services = scope.ServiceProvider;
    var _context = services.GetRequiredService<ApiContext>();
    if (_context != null)
        await SeedDateBase.SeedAsync(_context);
}