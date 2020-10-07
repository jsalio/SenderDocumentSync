using System;
using DocumentSender;
using Unity;

namespace WindowsApiService
{
    internal static class UnityConfig
    {
        private static UnityContainer _container;

        internal static void RegisterComponents(UnityContainer container)
        {
            _container = container;
            _container.RegisterSingleton<OnBaseReleaser>();
        }
    }
}