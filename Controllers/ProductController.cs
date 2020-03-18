using FoodStore.Entities;
using FoodStore.Infrastructure.LocalAPI;
using FoodStore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodStore.Controllers
{
    public class ProductController : Controller
    {


        private readonly IProductRepository _repository;
        public int PageSize = 8;
        private readonly Random _rnd = new Random();

        public ProductController(IProductRepository productRepository)
        {
            _repository = productRepository;
            RealTimeSellData.Products = productRepository.GetClone();
            new RealTimeSellData().Loop();
        }

        public ViewResult List(string category, int page = 1, string q = "")
        {
            IEnumerable<Product> products = null;

            if(q == null || q.Trim().Length == 0)
            {
                products = _repository.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => category == null ? _rnd.Next() : p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize);
            }
            else
            {
                products = _repository.Products
                .Where(p => p.Name.ToLower().Contains(q.ToLower()))
                .Skip((page - 1) * PageSize)
                .Take(PageSize);
            }

            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = products,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = TotalItems(category, q)
                },
                CurrentCategory = category,
                SearchQuery = q
            };
            return View(model);
        }

        private int TotalItems(string category, string q)
        {
            if(category == null || category.Trim().Length == 0)
            {
                return 0; // this will only hit the first page where no page count is shown
            }
            if(category == "Search Results")
            {
                return _repository.Products
                    .Where(p => p.Name.ToLower().Contains(q.ToLower()))
                    .Count();
            }
            return _repository.Products.Where(e => e.Category == category).Count();
        }


    }
}