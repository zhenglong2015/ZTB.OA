using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ZTB.OA.Common.Caches;

namespace ZTB.OA.Web.App_Start
{
    public class BuildContainer
    {
        public static void CoreAutoFacInit()
        {
            var builder = new ContainerBuilder();
            
            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());
            builder.RegisterFilterProvider();

            SetupResolveRules(builder);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void SetupResolveRules(ContainerBuilder builder)
        {
            //WebAPI只用引用services和repository的接口，不用引用实现的dll。
            //如需加载实现的程序集，将dll拷贝到bin目录下即可，不用引用dll
            var iServices = Assembly.Load("ZTB.OA.IBLL");
            var services = Assembly.Load("ZTB.OA.BLL");
            var iRepository = Assembly.Load("ZTB.OA.IDAL");
            var repository = Assembly.Load("ZTB.OA.EFDAL");

            // Add our own components
            builder.RegisterType<MemCache>().As<ICache>();


            //根据名称约定（数据访问层的接口和实现均以Repository结尾），实现数据访问接口和数据访问实现的依赖
            builder.RegisterAssemblyTypes(iRepository, repository)
              .Where(t => t.Name.EndsWith("Dal"))
              .AsImplementedInterfaces();

            //根据名称约定（服务层的接口和实现均以Services结尾），实现服务接口和服务实现的依赖
            builder.RegisterAssemblyTypes(iServices, services)
              .Where(t => t.Name.EndsWith("Service"))
              .AsImplementedInterfaces();


        }
    }
}