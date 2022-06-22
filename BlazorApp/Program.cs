using BlazorApp;
using BlazorApp.Identity;
using BlazorApp.Services;
using BlazorApp.WellKnownNames;
using Humanizer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.IdentityModel.Logging;
using MudBlazor.Services;
using Polly;
using Polly.Extensions.Http;
using Toolbelt.Blazor;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


var apiBaseUrl = builder.Configuration[AppSettings.ApiBaseUrl] ?? "";

builder.Services.AddScoped<CustomAuthorizationMessageHandler>();


_ = int.TryParse(builder.Configuration[AppSettings.PollyRetryCount],out var pollyRetryCount);

var policy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .WaitAndRetryAsync(pollyRetryCount, retryAttempt =>
    {
        var nextTimeInterval = TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
        Console.WriteLine($"Polly: error during call, retry for {retryAttempt} time in {TimeSpan.FromSeconds(nextTimeInterval.TotalSeconds).Humanize()} second(s).");
        return nextTimeInterval;
    });

builder.Services.AddScoped<HttpInterceptorService>();
builder.Services.AddHttpClientInterceptor();

builder.Services.AddHttpClient(AppSettings.HttpClientApi, (services, client) =>
{
    client.BaseAddress = new Uri(apiBaseUrl);    
    client.EnableIntercept(services);
    
})
    .AddHttpMessageHandler<CustomAuthorizationMessageHandler>()
    .AddPolicyHandler(policy);


builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient(AppSettings.HttpClientApi));





builder.Services.AddMsalAuthentication(options =>
    {
        builder.Configuration.Bind(AppSettings.AzureAd, options.ProviderOptions.Authentication);
        options.ProviderOptions.LoginMode = "redirect";
        
        //Default scopes in access token
        //options.ProviderOptions.DefaultAccessTokenScopes.Add("api://5e93c8a0-275a-482b-83c6-f4c2c49e18d1/access_as_user");

        //Additional scopes
        //options.ProviderOptions.AdditionalScopesToConsent.Add("api://5e93c8a0-275a-482b-83c6-f4c2c49e18d1/access_as_user");
    });



    builder.Services.AddApiAuthorization();

    builder.Services.AddLocalization();
    builder.Services.AddMudServices();

IdentityModelEventSource.ShowPII = true;

await builder.Build().RunAsync();
