using PropTechWeb.Components;
using PropTechWeb.Models;
using PropTechWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register PropTech AI services
builder.Services.AddSingleton<AIConfiguration>(_ => AIConfiguration.CreateDemo());
builder.Services.AddSingleton<HttpClient>();
builder.Services.AddSingleton<HuaweiAIService>();
builder.Services.AddSingleton<DataStore>();
builder.Services.AddSingleton<AIPropertyAgent>();
builder.Services.AddSingleton<PropertyManager>();

var app = builder.Build();

// Seed sample data
var dataStore = app.Services.GetRequiredService<DataStore>();
dataStore.AddProperty(new Property("P001", "12 Mandela Ave, Soweto", 3800m));
dataStore.AddProperty(new Property("P002", "45 Long St, Cape Town", 5200m));
dataStore.AddProperty(new Property("P003", "8 Berea Rd, Durban", 4100m));

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
