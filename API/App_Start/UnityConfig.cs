using System.Web.Http;
using DocumentSender;
using Unity;
using Unity.WebApi;

namespace SenderDocumentSync
{
    public static class UnityConfig
    {
        private static UnityContainer _container;

        internal static IUnityContainer GetConfigureContainer() => _container;

        public static void RegisterComponents()
        {
			_container = new UnityContainer();

            _container.RegisterSingleton<OnBaseReleaser>();
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(_container);
        }
    }
}