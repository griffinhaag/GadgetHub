using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Moq;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;
using GadgetHub.Domain.Concrete;

namespace GadgetHub.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel mykernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            mykernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type myservicetype)
        {
            return mykernel.TryGet(myservicetype);
        }

        public IEnumerable<object> GetServices(Type myservicetype)
        {
            return mykernel.GetAll(myservicetype);
        }

        private void AddBindings()
        {
            //Mock<IProductRepository> myMock = new Mock<IProductRepository>();
            //myMock.Setup(m => m.Products).Returns(new List<Product> {
            //    new Product { Name = "MacBook Pro", Price = 2600, Description = "High end computer from Apple", category = "Laptops"  },
            //    new Product { Name = "Logitech mouse", Price = 36, Description = "Work mouse from Logitech", category = "Mice" },
            //    new Product { Name = "AirPod Pros", Price = 150, Description = "High end headphones from Apple", category = "Headphones" }
            //    });
            //mykernel.Bind<IProductRepository>().ToConstant(myMock.Object);
            mykernel.Bind<IProductRepository>().To<EFProductRepository>();
        }
    }
}