using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using FoodStore.Abstract;
using FoodStore.Concrete;
using Ninject.Web.Common;

namespace FoodStore.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            _kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            _kernel.Bind<IProductRepository>().To<EFProductRepository>().InRequestScope();
            _kernel.Bind<IPurchaseHistoryRepository>().To<EFPurchaseHistoryRepository>().InRequestScope();
            _kernel.Bind<IOrderProcessor>().To<OrderProcessor>().InRequestScope();
            _kernel.Bind<ICommentsRepository>().To<EFCommentRepository>().InRequestScope();
            _kernel.Bind<IPublicProfilesRepository>().To<EFPublicProfileRepository>().InRequestScope();
        }
    }
}