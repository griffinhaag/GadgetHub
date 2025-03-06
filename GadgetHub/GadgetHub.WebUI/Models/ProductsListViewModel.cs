using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GadgetHub.Domain.Entities;

namespace GadgetHub.WebUI.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }

        public PagingInfo PagingInfo { get; set; }

        // Add current category property

        public string CurrentCategory {  get; set; }
    }
}