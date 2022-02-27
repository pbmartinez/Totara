using BlazorApp;
using BlazorApp.Identity;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

    var builder = WebAssemblyHostBuilder.CreateDefault(args);
    builder.RootComponents.Add<App>("#app");
    builder.RootComponents.Add<HeadOutlet>("head::after");


    var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "";
//1
//builder.Services.AddHttpClient("MusalaGatewaysApi", client =>
//    client.BaseAddress = new Uri(apiBaseUrl))
//    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

//builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("MusalaGatewaysApi"));


//2
//builder.Services.AddScoped(sp =>
//new HttpClient(
//        sp.GetRequiredService<AuthorizationMessageHandler>()
//        .ConfigureHandler(
//            authorizedUrls: new[] { apiBaseUrl },
//            scopes: new[] { "api://5e93c8a0-275a-482b-83c6-f4c2c49e18d1/access_as_user" }))
//    {
//        BaseAddress = new Uri(apiBaseUrl)
//    });

//3
//builder.Services.AddScoped<CustomAuthorizationMessageHandler>();

//builder.Services.AddHttpClient("MusalaGatewaysApi",
//        client => client.BaseAddress = new Uri("https://localhost:7219/"))
//    .AddHttpMessageHandler<CustomAuthorizationMessageHandler>();

//builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("MusalaGatewaysApi"));

//4

//builder.Services.AddTransient<AuthorizationMessageHandler>(sp =>
//{
//    //  Get required services from DI.
//    var provider = sp.GetRequiredService<IAccessTokenProvider>();
//    var naviManager = sp.GetRequiredService<NavigationManager>();

//    //  Create a new "AuthorizationMessageHandler" instance,
//    //    and return it after configuring it.
//    var handler = new AuthorizationMessageHandler(provider, naviManager);
//    handler.ConfigureHandler(authorizedUrls: new[] {
//      // List up URLs which to be attached access token.
//      naviManager.ToAbsoluteUri("api/").AbsoluteUri
//    });
//    return handler;
//});

//  Use "AuthorizationMessageHandler" that is configured above 
//    instead of "BaseAddressAuthorizationMessageHandler".
builder.Services.AddHttpClient("BlazorWasmApp.ServerAPI", client => client.BaseAddress= new Uri(apiBaseUrl))
  // .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
  .AddHttpMessageHandler<AuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>()
  .CreateClient("BlazorWasmApp.ServerAPI"));


    builder.Services.AddMsalAuthentication(options =>
    {
        builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
        options.ProviderOptions.LoginMode = "redirect";

        //Default scopes in access token
        options.ProviderOptions.DefaultAccessTokenScopes.Add("api://5e93c8a0-275a-482b-83c6-f4c2c49e18d1/access_as_user");

        //Additional scopes
        //options.ProviderOptions.AdditionalScopesToConsent.Add("api://5e93c8a0-275a-482b-83c6-f4c2c49e18d1/access_as_user");
    });



    builder.Services.AddApiAuthorization();

    builder.Services.AddLocalization();
    builder.Services.AddMudServices();

    await builder.Build().RunAsync();
