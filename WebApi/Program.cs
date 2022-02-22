using Infraestructure.DependencyInjectionExtensions;
using Microsoft.AspNetCore.Mvc.Formatters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(setupAction=>
{
    //Return Not Acceptable Status Code when api is requested in a format that it does not support
    setupAction.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters(); // XML output formatter for support responses in xml format



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapperWithProfiles();
builder.Services.AddEntitiesServicesAndRepositories();
builder.Services.AddCustomApplicationServices();

var app = builder.Build();

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
