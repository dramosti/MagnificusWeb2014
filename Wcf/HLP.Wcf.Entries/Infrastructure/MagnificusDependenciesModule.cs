using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HLP.Wcf.Entries.Infrastructure
{
    public class MagnificusDependenciesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<UnitOfWorkBase>().ToConstant(new UnitOfWork());
            ResolveRepositories();
            ResolveServices();
        }

        protected void ResolveRepositories()        
        { 

        }

        protected void ResolveServices()
        {

        }
    }
}