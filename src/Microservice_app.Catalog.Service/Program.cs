
using System.Runtime.CompilerServices;
using Microservice_app.Catalog.Service;
using Microservice_app.Catalog.Service.Dto;
using Microservice_app.Catalog.Service.Settings;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
StartUp startUp = new StartUp(builder.Configuration);
startUp.ConfigureServices(builder.Services);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();


app.Run();
