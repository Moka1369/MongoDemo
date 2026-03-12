using MongoDemo.Ui.Components;
using MongoDemo.Ui.Services;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();  //MudBlazor in MongoDemo.Ui registrieren

var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"]
                 ?? "http://localhost:5057/";

builder.Services.AddHttpClient("MongoApi", client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
    //client.BaseAddress = new Uri("http://localhost:5057/");  //Das funktioniert im Container nicht, weil localhost im UI-Container der UI-Container selbst ist, nicht die API.
});

builder.Services.AddScoped<CustomerApiService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
