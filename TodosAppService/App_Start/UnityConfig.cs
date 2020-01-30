using System.Web.Http;
using TodosApp.BLogic;
using TodosApp.DataAccess;
using Unity;
using Unity.WebApi;

namespace TodosAppService
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

			container.RegisterType<ITodoRepository, TodoRepository>();
			container.RegisterType<ITodoManager, TodoManager>();

			GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}