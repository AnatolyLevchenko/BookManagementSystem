using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BookManagementSystem.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(ApiUrl()) });
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
await builder.Build().RunAsync();


string ApiUrl()
{
    var apiBaseUrl = builder.Configuration["ApiBaseUrl"];
    if (!string.IsNullOrEmpty(apiBaseUrl))
    {
        return apiBaseUrl;
    }

    throw new Exception("Cannot determine backend API endpoint");
}