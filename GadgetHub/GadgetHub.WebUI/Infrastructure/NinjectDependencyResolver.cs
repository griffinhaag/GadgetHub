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
using System.Configuration; //Configuration namespace
using GadgetHub.WebUI.Infrastructure.Abstract;
using GadgetHub.WebUI.Infrastructure.Concrete;

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
            mykernel.Bind<IProductRepository>().To<EFProductRepository>();
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse
                (ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };

            mykernel.Bind<IOrderProcessor>()
                .To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);

            // Authentication
            mykernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
        }
    }
}