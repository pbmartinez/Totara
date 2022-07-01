using Infrastructure.DependencyInjectionExtensions;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Text.Json.Serialization;
using Infrastructure.Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using System.Configuration;
using Microsoft.IdentityModel.Logging;
using WebApi.WellKnownNames;
using Serilog;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using WebApi.Helpers;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Hellang.Middleware.ProblemDetails;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers(setupAction =>
{
    //Return Not Acceptable Status Code when api is requested in a format that it does not support
    setupAction.ReturnHttpNotAcceptable = true;
})
.AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
.ConfigureApiBehaviorOptions(setupAction =>
 {
     setupAction.InvalidModelStateResponseFactory = context =>
     {
         // create a problem details object
         var problemDetailsFactory = context.HttpContext.RequestServices
             .GetRequiredService<ProblemDetailsFactory>();
         var problemDetails = problemDetailsFactory.CreateValidationProblemDetails(
                 context.HttpContext,
                 context.ModelState);

         // add additional info not added by default
         problemDetails.Detail = "See the errors field for details.";
         problemDetails.Instance = context.HttpContext.Request.Path;

         // find out which status code to use
         var actionExecutingContext =
               context as Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext;

         // if there are modelstate errors & all keys were correctly
         // found/parsed we're dealing with validation errors
         if ((context.ModelState.ErrorCount > 0) &&
             (actionExecutingContext?.ActionArguments.Count == context.ActionDescriptor.Parameters.Count))
         {
             //problemDetails.Type = "";
             problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
             problemDetails.Title = "One or more validation errors occurred.";

             return new UnprocessableEntityObjectResult(problemDetails)
             {
                 ContentTypes = { "application/problem+json" }
             };
         }

         // if one of the keys wasn't correctly found / couldn't be parsed
         // we're dealing with null/unparsable input
         problemDetails.Status = StatusCodes.Status400BadRequest;
         problemDetails.Title = "One or more errors on input occurred.";
         return new BadRequestObjectResult(problemDetails)
         {
             ContentTypes = { "application/problem+json" }
         };
     };
 })
;
// CORS Configuration
var allowedHosts = builder.Configuration[AppSettings.AllowedHosts].Split(',');

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowedHosts", builder =>
    {
        builder.WithOrigins(allowedHosts.ToArray());
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        builder.SetIsOriginAllowedToAllowWildcardSubdomains();
    });
});

//Api Docs Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Application Core Services Configuration
builder.Services.AddAutoMapperWithProfiles();
builder.Services.AddEntitiesServicesAndRepositories();
builder.Services.AddCustomApplicationServices();

//Unit of Work Implementation Configuration
builder.Services.AddDbContext<UnitOfWorkContainer>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString(AppSettings.DefaultConnectionString), sqlServerOptions =>
   {
       sqlServerOptions.CommandTimeout(30);
       sqlServerOptions.EnableRetryOnFailure(3);
   }));


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddMicrosoftIdentityWebApi(builder.Configuration.GetSection(AppSettings.AzureAd));

builder.Services.AddHealthChecks().AddDbContextCheck<UnitOfWorkContainer>(failureStatus: HealthStatus.Degraded);

builder.Services.AddProblemDetails();

IdentityModelEventSource.ShowPII = true;

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseProblemDetails();

app.MapHealthChecks("/health/live", new HealthCheckOptions()
{
    Predicate = _ => false,
    ResponseWriter = HealthCheckExtensions.WriteJsonResponse
});
app.MapHealthChecks("/health/ready", new HealthCheckOptions()
{
    ResponseWriter = HealthCheckExtensions.WriteJsonResponse
});



app.UseHttpsRedirection();

app.UseCors("AllowedHosts");


app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();

