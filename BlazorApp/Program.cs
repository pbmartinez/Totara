using BlazorApp;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "";

//builder.Services.AddScoped(sp => 
//    new HttpClient {
//        BaseAddress = new Uri(apiBaseUrl)
//});
builder.Services.AddHttpClient("MusalaGatewaysApi", client =>
    client.BaseAddress = new Uri(apiBaseUrl))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddTransient(sp=>sp.GetRequiredService<IHttpClientFactory>().CreateClient("MusalaGatewaysApi"));



builder.Services.AddApiAuthorization();

builder.Services.AddLocalization();
builder.Services.AddMudServices();

await builder.Build().RunAsync();
