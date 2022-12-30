﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyShop.ApplicationCore.Entities;
using MyShop.Infrastructure.Data;


namespace MyShop.Infrastructure
{
    public sealed class CatalogContextSeed
    {
        public static async Task SeedAsync(CatalogContext catalogContext, ILogger loger, int retry = 0)
        {
            var retryForAvailability = retry;

            try
            {
                if (!await catalogContext.CatalogBrands.AnyAsync())
                {
                    await catalogContext.CatalogBrands.AddRangeAsync(GetPreConfiguredBrands());
                    await catalogContext.SaveChangesAsync();
                }
                if (!await catalogContext.CatalogTypes.AnyAsync())
                {
                    await catalogContext.CatalogTypes.AddRangeAsync(GetPreConfiguredCatalogTypes());
                    await catalogContext.SaveChangesAsync();
                }
                if (!await catalogContext.CatalogItems.AnyAsync())
                {
                    await catalogContext.CatalogItems.AddRangeAsync(GetPreConfiguredCatalogItems());
                    await catalogContext.SaveChangesAsync(); 
                }
            }
            catch (Exception ex)
            {
                if(retryForAvailability >= 10) throw;

                retryForAvailability++;

                loger.LogError(ex.Message);
                await SeedAsync(catalogContext, loger, retry);
            }
        }

        private static IEnumerable<CatalogItem> GetPreConfiguredCatalogItems()
        {
            return new List<CatalogItem>
            {
                new(2,2, ".NET Bot Black Sweatshirt", ".NET Bot Black Sweatshirt", 19.5M,  "http://catalogbaseurltobereplaced/images/products/1.png"),
                new(1,2, ".NET Black & White Mug", ".NET Black & White Mug", 8.50M, "http://catalogbaseurltobereplaced/images/products/2.png"),
                new(2,5, "Prism White T-Shirt", "Prism White T-Shirt", 12,  "http://catalogbaseurltobereplaced/images/products/3.png"),
                new(2,2, ".NET Foundation Sweatshirt", ".NET Foundation Sweatshirt", 12, "http://catalogbaseurltobereplaced/images/products/4.png"),
                new(3,5, "Roslyn Red Sheet", "Roslyn Red Sheet", 8.5M, "http://catalogbaseurltobereplaced/images/products/5.png"),
                new(2,2, ".NET Blue Sweatshirt", ".NET Blue Sweatshirt", 12, "http://catalogbaseurltobereplaced/images/products/6.png"),
                new(2,5, "Roslyn Red T-Shirt", "Roslyn Red T-Shirt",  12, "http://catalogbaseurltobereplaced/images/products/7.png"),
                new(2,5, "Kudu Purple Sweatshirt", "Kudu Purple Sweatshirt", 8.5M, "http://catalogbaseurltobereplaced/images/products/8.png"),
                new(1,5, "Cup<T> White Mug", "Cup<T> White Mug", 12, "http://catalogbaseurltobereplaced/images/products/9.png"),
                new(3,2, ".NET Foundation Sheet", ".NET Foundation Sheet", 12, "http://catalogbaseurltobereplaced/images/products/10.png"),
                new(3,2, "Cup<T> Sheet", "Cup<T> Sheet", 8.5M, "http://catalogbaseurltobereplaced/images/products/11.png"),
                new(2,5, "Prism White TShirt", "Prism White TShirt", 12, "http://catalogbaseurltobereplaced/images/products/12.png")
            };
        }

        private static IEnumerable<CatalogType> GetPreConfiguredCatalogTypes()
        {
            return new List<CatalogType>
            {
                new("Mug"),
                new("T-Shirt"),
                new("Sgeet"),
                new("USB Memory Stick"),
            };
        }

        private static IEnumerable<CatalogBrand>GetPreConfiguredBrands() 
        {
            return new List<CatalogBrand>
            {
                new("Azure"),
                new(".NET"),
                new("Visual Studio"),
                new("SQL Server"),
                new("Other")
            };
        }
    }
}
