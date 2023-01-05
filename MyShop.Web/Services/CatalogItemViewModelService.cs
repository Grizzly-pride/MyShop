using Microsoft.AspNetCore.Mvc.Rendering;
using MyShop.ApplicationCore.Entities;
using MyShop.ApplicationCore.Interfaces;
using MyShop.Interfaces;
using MyShop.Models;
using MyShop.Web.Models;

namespace MyShop.Services
{
    public sealed class CatalogItemViewModelService : ICatalogItemViewModelService
    {
        private readonly IRepository<CatalogItem> _catalogItemRepository;
		private readonly IRepository<CatalogBrand> _brandRepository;
		private readonly IRepository<CatalogType> _typeRepository;
        private readonly IAppLogger<CatalogItemViewModelService> _logger;


        public CatalogItemViewModelService(
            IRepository<CatalogItem> catalogItemRepository,
            IAppLogger<CatalogItemViewModelService> logger,
			IRepository<CatalogBrand> brandRepository,
			IRepository<CatalogType> typeRepository
			) 
        {
            _catalogItemRepository = catalogItemRepository;
            _brandRepository = brandRepository;
            _typeRepository = typeRepository;
            _logger = logger;
        }

		public async Task<IEnumerable<SelectListItem>> GetBrands()
		{
            _logger.LogInformation("Get Brands called");

            var brands = await _brandRepository.GetAllAsync();

            var items = brands
                .Select(brand => new SelectListItem() { Value = brand.Id.ToString(), Text = brand.Brand })
                .OrderBy(brand => brand.Text)
                .ToList();

            var allItem = new SelectListItem() { Value = null, Text = "All", Selected = true };

			items.Insert(0, allItem);

			return items;
		}

		public async Task<IEnumerable<SelectListItem>> GetTypes()
		{
			_logger.LogInformation("Get Types called");

			var types = await _typeRepository.GetAllAsync();

			var items = types
				.Select(type => new SelectListItem() { Value = type.Id.ToString(), Text = type.Type })
				.OrderBy(type => type.Text)
				.ToList();

			var allItem = new SelectListItem() { Value = null, Text = "All", Selected = true };

            items.Insert(0, allItem);

			return items;
		}

		public async Task<CatalogIndexViewModel> GetCatalogItems(int? brandId, int? typeId)
		{
			var entities = await _catalogItemRepository.GetAllAsync();

            var catalogitems = entities
                .Where(item => (!brandId.HasValue || item.CatalogBrandId == brandId)
                &&(!typeId.HasValue || item.CatalogTypeId == typeId))
                .Select(item => new CatalogItemViewModel()
                {
                    Id= item.Id,
                    Name = item.Name,
                    PictureUrl= item.PictureUrl,
                    Price= item.Price,
                }).ToList();

            var vm = new CatalogIndexViewModel()
            {
                CatalogItems = catalogitems,
                Brands = (await GetBrands()).ToList(),
                Types = (await GetTypes()).ToList(),
            };

            return vm;
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
