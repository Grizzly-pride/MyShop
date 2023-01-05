using MyShop.ApplicationCore.Interfaces;
using MyShop.Interfaces;
using MyShop.ApplicationCore.Entities;
using MyShop.Services;
using MyShop.Infrastructure;
using MyShop.Web.Configuration;
using MyShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region IoC
builder.Services.AddControllersWithViews();
builder.Services.AddCoreServices();
builder.Services.AddScoped<ICatalogItemViewModelService, CatalogItemViewModelService>();
Dependencies.ConfigureServices(builder.Configuration, builder.Services);
#endregion

var app = builder.Build();

app.Logger.LogInformation("Database migration running...");

#region Auto Database Update
using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var catalogContext = scopedProvider.GetRequiredService<CatalogContext>();
        if (catalogContext.Database.IsSqlServer())
        {
            catalogContext.Database.Migrate();
        }
        await CatalogContextSeed.SeedAsync(catalogContext, app.Logger);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred seeding the DB.");
    }
}
#endregion

app.Logger.LogInformation("App Created...");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}   

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Catalog}/{action=Index}/{id?}");

app.Logger.LogDebug("Starting the App...");

app.Run();
