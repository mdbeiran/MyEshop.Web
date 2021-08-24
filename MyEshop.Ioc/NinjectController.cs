using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using MyEshop.Services;
using MyEshop.Utility;

namespace MyEshop.Ioc
{
    public class NinjectController : DefaultControllerFactory
    {
        #region Ctor

        private IKernel ninjectKernel;

        public NinjectController()
        {
            ninjectKernel = new StandardKernel();
            AddBinding();
        }

        #endregion

        #region Bind

        void AddBinding()
        {
            ninjectKernel.Bind<MyEshopUOW>().To<MyEshopUOW>();
            ninjectKernel.Bind<IPasswordHelper>().To<PasswordHelper>();
        }

        //روش ایمان مدائنی
        //protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        //{
        //    return controllerType == null ? null : (IController) ninjectKernel.Get(controllerType);
        //}


        //روش اردوخانی
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404, "not found");
            }

            return (IController)ninjectKernel.Get(controllerType);
        }

        #endregion
    }
}