using Codecool.CodecoolShop.Helpers;
using Codecool.CodecoolShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Codecool.CodecoolShop.Services;
using Codecool.CodecoolShop.Daos.Implementations;
using Microsoft.Extensions.Logging;

namespace Codecool.CodecoolShop.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        public ProductService ProductService { get; set; }

        public CartController(ILogger<CartController> logger)
        {
            _logger = logger;
            ProductService = new ProductService(
                ProductDaoMemory.GetInstance(),
                ProductCategoryDaoMemory.GetInstance(),
                CartDaoMemory.GetInstance());
        }

       

        [Route("index")]
        public IActionResult Index()
        {
            
            return View(ProductService.cartDao.GetAll());
        }

        [Route("add/{id}")]
        public IActionResult Add(string id)
        {
            Product item = ProductService.cartDao.Get(Int32.Parse(id));
            ProductService.cartDao.Add(item);
            return RedirectToAction("Index");
        }


        [Route("remove/{id}")]
        public IActionResult Remove(string id)
        {
            ProductService.cartDao.Remove(Int32.Parse(id));
            return RedirectToAction("Index");
        }


        [Route("cart")]
        public IActionResult ViewCart()
        {
            ViewBag.cart = ProductService.cartDao.GetAll();
            return RedirectToAction("Cart");
        }

    }
}
