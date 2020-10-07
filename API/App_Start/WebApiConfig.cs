using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SenderDocumentSync
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            string allowOrigins = ConfigurationManager.AppSettings["AllowOrigins"];
            string allowHeaders = ConfigurationManager.AppSettings["AllowHeaders"];
            string allowMethods = ConfigurationManager.AppSettings["AllowMethods"];
            var cors = new EnableCorsAttribute(allowOrigins, allowHeaders, allowMethods);
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
