using GadgetHub.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GadgetHub.WebUI.Models;
using System.Runtime.Remoting.Messaging;

namespace GadgetHub.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository myrepository;

        public ProductController (IProductRepository productRepository)
        {
            this.myrepository = productRepository;
        }

        public int PageSize = 4;

        public ViewResult List(int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = myrepository.Products.OrderBy(p => p.ProductID)
                                             .Skip((page - 1) * PageSize)
                                             .Take(PageSize),

            PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = myrepository.Products.Count()
            }
          };
          return View(model);
        }                               
    }
}