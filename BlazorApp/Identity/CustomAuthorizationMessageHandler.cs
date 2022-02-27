using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
namespace BlazorApp.Identity
{
    public class CustomAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        public CustomAuthorizationMessageHandler(IAccessTokenProvider provider,
            NavigationManager navigationManager)
            : base(provider, navigationManager)
        {
            ConfigureHandler(
                authorizedUrls: new[] { "https://localhost:7219" }
                ,
                scopes: new[] { "api://5e93c8a0-275a-482b-83c6-f4c2c49e18d1/access_as_user" }
                );
        }

        
    }
}
