﻿using Microsoft.AspNetCore.Mvc;
using MyShop.ApplicationCore.Entities;
using MyShop.ApplicationCore.Interfaces;
using MyShop.Interfaces;
using MyShop.Models;

namespace MyShop.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogItemViewModelService _catalogItemViewModelService;
        private readonly IRepository<CatalogItem> _catalogRepository;
        
        public CatalogController(
            IRepository<CatalogItem> catalogRepository,
            ICatalogItemViewModelService catalogItemViewModel)
        {
            _catalogRepository = catalogRepository;
            _catalogItemViewModelService = catalogItemViewModel;
        }

        public async Task<IActionResult> Index()
        {
            var catalogItemsViewModel = await _catalogItemViewModelService.GetCatalogItems();
                
            return View(catalogItemsViewModel);
        }

        public IActionResult Details(int id) 
        {
            var item = _catalogRepository.GetById(id);

            if (item == null) return RedirectToAction("Index");

            var result = new CatalogItemViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                PictureUrl = item.PictureUrl,
                Price = item.Price
            };
            return View(result);
        }

        [HttpGet]
        public IActionResult Edit(int id) 
        {
            var item = _catalogRepository.GetById(id);

            if (item == null) return RedirectToAction("Index");

            var result = new CatalogItemViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                PictureUrl = item.PictureUrl,
                Price = item.Price
            };
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CatalogItemViewModel catalogItemViewModel) 
        {
            try
            {
                _catalogItemViewModelService.UpdateCatalogItem(catalogItemViewModel);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}
