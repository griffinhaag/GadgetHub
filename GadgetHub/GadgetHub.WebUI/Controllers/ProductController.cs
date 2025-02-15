using GadgetHub.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GadgetHub.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository myrepository;

        public ProductController (IProductRepository productRepository)
        {
            this.myrepository = productRepository;
        }

        public ViewResult List()
        {
            return View(myrepository.Products);
        }
    }
}