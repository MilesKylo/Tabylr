using Tabylr.Client.Pages;
using Tabylr.Components;
using Supabase;
using Tabylr.Services.Interfaces;
using Tabylr.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Services;
using Tabylr.Repositories.Interfaces;
using Tabylr.Repositories;

var builder = WebApplication.CreateBuilder(args);
// Add configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddControllers();




// Configure Supabase
var supabaseUrl = builder.Configuration["Supabase:Url"];
var supabaseApiKey = builder.Configuration["Supabase:ApiKey"];

if (string.IsNullOrEmpty(supabaseUrl) || string.IsNullOrEmpty(supabaseApiKey))
{
    throw new InvalidOperationException("Supabase URL or API Key is missing in the configuration.");
}

var supabaseSettings = new Supabase.SupabaseOptions
{
    AutoConnectRealtime = true
};
var supabaseClient = new Supabase.Client(supabaseUrl, supabaseApiKey);

// Add other services
builder.Services.AddSingleton(supabaseClient);
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
// Add this before builder.Build()
builder.Services.AddScoped<LazyAssemblyLoader>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.MapControllers();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Tabylr.Client._Imports).Assembly);

app.Run();
