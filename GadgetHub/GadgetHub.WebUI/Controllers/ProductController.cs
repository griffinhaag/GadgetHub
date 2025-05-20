using GadgetHub.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GadgetHub.WebUI.Models;
using System.Runtime.Remoting.Messaging;
using GadgetHub.Domain.Entities;

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

        public ViewResult List(string category, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = myrepository.Products
                                             .Where(p => category == null || p.category == category)
                                             .OrderBy(p => p.ProductID)
                                             .Skip((page - 1) * PageSize)
                                             .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                                    myrepository.Products.Count() :
                                    myrepository.Products.Where
                                          (e => e.category == category).Count()
                },
                CurrentCategory = category
          };
          return View(model);
        }
        
        public FileContentResult GetImage(int productId)
        {
            Product prod = myrepository.Products.FirstOrDefault(p => p.ProductID == productId);

            if(prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }

            else
            {
                return null;
            }
        }
    }
}