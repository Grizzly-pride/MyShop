using MyShop.Models;

namespace MyShop.Interfaces
{
    public interface ICatalogItemViewModelService
    {
        void UpdateCatalogItem(CatalogItemViewModel viewModel);
        Task<IEnumerable<CatalogItemViewModel>> GetCatalogItems();
    }
}
