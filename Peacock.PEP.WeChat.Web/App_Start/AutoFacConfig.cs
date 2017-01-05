using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Peacock.InWork4.DataConvert
{
    public class AutoFacConfig
    {
        public static void RegisterAutoFac()
        {
            ContainerBuilder builder = new ContainerBuilder();

            #region IOC注册区域
            //builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerDependency();
            //builder.RegisterType<Project_ProjectBLL>().As<IProject_Project>().InstancePerDependency();
            //builder.Register(c => new Project_ProjectAdapter((IProject_Project)c.Resolve(typeof(IProject_Project))));

            #endregion
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
