using MyShop.ApplicationCore.Entities;
using MyShop.ApplicationCore.Interfaces;
using MyShop.Interfaces;
using MyShop.Models;

namespace MyShop.Services
{
    public sealed class CatalogItemViewModelService : ICatalogItemViewModelService
    {
        private readonly IRepository<CatalogItem> _catalogItemRepository;
        private readonly IAppLogger<CatalogItemViewModelService> _logger;

        public CatalogItemViewModelService(
            IRepository<CatalogItem> catalogItemRepository,
            IAppLogger<CatalogItemViewModelService> logger) 
        {
            _catalogItemRepository = catalogItemRepository;
            _logger = logger;
        }

        public void UpdateCatalogItem(CatalogItemViewModel viewModel)
        {
            var existingCatalogItem = _catalogItemRepository.GetById(viewModel.Id);
            if (existingCatalogItem is null)
            {
                Exception exception = new($"Catalog item{viewModel.Id} was not found");
                _logger.LogError(exception, exception.Message, this);
                throw exception;               
            } 

            CatalogItem.CatalogItemDetails details = new(viewModel.Name, viewModel.Price);
            existingCatalogItem.UpdateDetails(details);

            _logger.LogInformation($"Updating catalog item {existingCatalogItem.Id}," +
                $" Name{existingCatalogItem.Name}," +
                $" Price{existingCatalogItem.Price}");

            _catalogItemRepository.Update(existingCatalogItem);
        }
    }
}
