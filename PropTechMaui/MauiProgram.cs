using Microsoft.Extensions.Logging;
using PropTechMaui.Models;
using PropTechMaui.Services;

namespace PropTechMaui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Register PropTech AI services
        builder.Services.AddSingleton<AIConfiguration>(_ => AIConfiguration.CreateDemo());
        builder.Services.AddSingleton<HttpClient>();
        builder.Services.AddSingleton<HuaweiAIService>();
        builder.Services.AddSingleton<DataStore>();
        builder.Services.AddSingleton<AIPropertyAgent>();
        builder.Services.AddSingleton<PropertyManager>();

        // Register pages for DI
        builder.Services.AddTransient<Pages.DashboardPage>();
        builder.Services.AddTransient<Pages.PropertiesPage>();
        builder.Services.AddTransient<Pages.TenantsPage>();
        builder.Services.AddTransient<Pages.LeasesPage>();
        builder.Services.AddTransient<Pages.MaintenancePage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        var app = builder.Build();

        // Seed sample data
        var dataStore = app.Services.GetRequiredService<DataStore>();
        dataStore.AddProperty(new Property("P001", "12 Mandela Ave, Soweto", 3800m));
        dataStore.AddProperty(new Property("P002", "45 Long St, Cape Town", 5200m));
        dataStore.AddProperty(new Property("P003", "8 Berea Rd, Durban", 4100m));

        return app;
    }
}
