using MyShop.Interfaces;
using MyShop.ApplicationCore.Entities;
using MyShop.Services;
using MyShop.Infrastructure;
using MyShop.Web.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Ioc
builder.Services.AddCoreServices();
builder.Services.AddScoped(typeof(IRepository<CatalogItem>), typeof(LocalCatalofItemRepository));
builder.Services.AddScoped<ICatalogItemViewModelService, CatalogItemViewModelService>();
Dependencies.ConfigureServices(builder.Configuration, builder.Services);
#endregion

var app = builder.Build();

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
