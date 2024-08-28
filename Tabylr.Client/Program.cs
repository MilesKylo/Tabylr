using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Tabylr.Client.Services;
using Tabylr.Client.Services.Interfaces;


var builder = WebAssemblyHostBuilder.CreateDefault(args);



// HttpClient registration
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Authentication and Authorization
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthStateProvider>());

// Other services
builder.Services.AddScoped<IAuthService, AuthService>();

// ... rest of your configuration
await builder.Build().RunAsync();
