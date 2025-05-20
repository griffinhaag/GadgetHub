using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;
using GadgetHub.WebUI.Models;

namespace GadgetHub.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;

        private IOrderProcessor orderProcessor; // IOrderProcessor

        public CartController(IProductRepository repo, IOrderProcessor proc)
        {
            repository = repo;
            orderProcessor = proc;
        }

        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];

            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;

        }
        
        public RedirectToRouteResult AddToCart(Cart cart, int productID, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault
                                                 (p => p.ProductID == productID);

            if (product != null)
            {
                cart.AddItem(product, 1);
            }

            return RedirectToAction("Index", new { returnUrl  });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl) 
        {
            Product product = repository.Products.FirstOrDefault
                                                    (p => p.ProductID == productId);

            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                ReturnUrl = returnUrl,
                Cart = cart
            });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }
        [HttpPost]

        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if(ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed", shippingDetails);
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}