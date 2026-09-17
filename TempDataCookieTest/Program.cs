using Microsoft.AspNetCore.DataProtection;
using TempDataCookieTest.Client.Pages;
using TempDataCookieTest.Components;

var builder = WebApplication.CreateBuilder(args);

var cookieName = builder.Configuration["TempDataTest:CookieName"];
var keyDirectory = builder.Configuration["TempDataTest:KeyDirectory"];
var applicationName = builder.Configuration["TempDataTest:ApplicationName"];
var pathBase = builder.Configuration["TempDataTest:PathBase"];

// Add services to the container.
builder.Services.AddRazorComponents(options =>
    {
        if (!string.IsNullOrWhiteSpace(cookieName))
        {
            options.TempDataCookie.Name = cookieName;
        }
    })
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var dataProtection = builder.Services.AddDataProtection();
if (!string.IsNullOrWhiteSpace(keyDirectory))
{
    dataProtection.PersistKeysToFileSystem(new DirectoryInfo(keyDirectory));
}
if (!string.IsNullOrWhiteSpace(applicationName))
{
    dataProtection.SetApplicationName(applicationName);
}

var app = builder.Build();

if (!string.IsNullOrWhiteSpace(pathBase))
{
    app.UsePathBase(pathBase);
}
app.UseRouting();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(TempDataCookieTest.Client._Imports).Assembly);

app.Run();
