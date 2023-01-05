using Microsoft.AspNetCore.Mvc.Rendering;
using MyShop.Models;

namespace MyShop.Web.Models
{
	internal sealed class CatalogIndexViewModel
	{
		public List<CatalogItemViewModel>? CatalogItems { get; set; }
		public List<SelectListItem>? Brands { get; set; }
		public List<SelectListItem>? Types { get; set; }
		public int? BrandFilterApplied { get; set; }
		public int? TypeFilterApplied { get; set; }
	}
}
