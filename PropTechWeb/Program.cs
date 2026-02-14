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

// Seed sample 360° virtual tours
dataStore.AddVirtualTour(new VirtualTour("T001", "P001", new List<RoomPanorama>
{
    new RoomPanorama("Living Room", "panorama://p001/living-room.jpg", "Open-plan living area with large windows"),
    new RoomPanorama("Kitchen", "panorama://p001/kitchen.jpg", "Fitted kitchen with granite countertops"),
    new RoomPanorama("Bedroom 1", "panorama://p001/bedroom1.jpg", "Master bedroom with built-in wardrobe"),
    new RoomPanorama("Bathroom", "panorama://p001/bathroom.jpg", "Full bathroom with shower and bath")
}));
dataStore.AddVirtualTour(new VirtualTour("T002", "P002", new List<RoomPanorama>
{
    new RoomPanorama("Lounge", "panorama://p002/lounge.jpg", "Spacious lounge with sea views"),
    new RoomPanorama("Kitchen", "panorama://p002/kitchen.jpg", "Modern open-plan kitchen"),
    new RoomPanorama("Bedroom 1", "panorama://p002/bedroom1.jpg", "Main bedroom with en-suite"),
    new RoomPanorama("Bedroom 2", "panorama://p002/bedroom2.jpg", "Second bedroom with built-in cupboards"),
    new RoomPanorama("Bathroom", "panorama://p002/bathroom.jpg", "Shared bathroom with tiled finishes")
}));

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
