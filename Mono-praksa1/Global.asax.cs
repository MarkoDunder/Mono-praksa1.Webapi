using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using Mono_praksa1.Model.Common;
using Mono_praksa1.Model.Models;
using Mono_praksa1.Repository;
using Mono_praksa1.RepositoryCommon;
using Mono_praksa1.Service;
using Mono_Praksa1.Service.Common;

namespace Mono_praksa1
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            DIBuilder prebuilder = new DIBuilder();
            prebuilder.Build();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
        
           
    }
    public class DIBuilder { 
        
             public void Build()
            {
                var builder = new ContainerBuilder();
                var config = GlobalConfiguration.Configuration;
                builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

                builder.RegisterType<CharacterService>().As<ICharactersService>();
                builder.RegisterType<CharacterRepository>().As<ICharacterRepository>();
                builder.RegisterType<Character>().As<ICharacter>();

                builder.RegisterType<FactionService>().As<IFactionService>();
                builder.RegisterType<FactionRepository>().As<IFactionRepository>();
                builder.RegisterType<Faction>().As<IFaction>();

                var container = builder.Build();
                config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            }
  }


}



