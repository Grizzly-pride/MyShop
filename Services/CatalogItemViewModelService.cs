using MyShop.Interfaces;
using MyShop.Models;

namespace MyShop.Services
{
    public sealed class CatalogItemViewModelService : ICatalogItemViewModelService
    {
        private readonly IRepository<CatalogItem> _catalogItemRepository;

        public CatalogItemViewModelService() 
        {
            _catalogItemRepository = new LocalCatalofItemRepository();
        }

        public void UpdateCatalogItem(CatalogItemViewModel viewModel)
        {
            var existingCatalogItem = _catalogItemRepository.GetById(viewModel.Id);
            if (existingCatalogItem is null) throw new Exception($"Catalog item{viewModel.Id} was not found");

            CatalogItem.CatalogItemDetails details = new(viewModel.Name, viewModel.Price);
            existingCatalogItem.UpdateDetails(details);
            _catalogItemRepository.Update(existingCatalogItem);
        }
    }
}
