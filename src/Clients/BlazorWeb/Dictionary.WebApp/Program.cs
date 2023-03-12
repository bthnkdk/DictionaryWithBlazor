using Blazored.LocalStorage;
using Dictionary.WebApp;
using Dictionary.WebApp.Insfrastructure.Services;
using Dictionary.WebApp.Insfrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//var adress = builder.Configuration["Address"];
builder.Services.AddHttpClient("WebApiClient", client =>
{
    client.BaseAddress = new Uri("http://localhost:5001");
}); // TODO authtokenhandler

builder.Services.AddScoped(sp =>
{
    var clientFactory = sp.GetRequiredService<IHttpClientFactory>();
    return clientFactory.CreateClient("WebApiClient");
});

builder.Services.AddTransient<IVoteService, VoteService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IFavService, FavService>();
builder.Services.AddTransient<IEntryService, EntryService>();
builder.Services.AddTransient<IIdentityService, IdentityService>();

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
