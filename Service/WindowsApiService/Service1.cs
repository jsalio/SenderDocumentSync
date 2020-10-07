using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using DocumentSender;
using Newtonsoft.Json.Converters;

namespace WindowsApiService
{
    public partial class Service1 : ServiceBase
    {
        private OnBaseReleaser _releaser;

        public Service1()
        {
            InitializeComponent();
        }

        public Service1(OnBaseReleaser releaser)
        {
            InitializeComponent();
            _releaser = releaser;
        }

        protected override void OnStart(string[] args)
        {
            InitServer();
        }

        internal void OnDebug()
        {
            InitServer();
        }

        protected override void OnStop()
        {
            _releaser.Disconect();
        }

        void InitServer()
        {
            var url = ConfigurationManager.AppSettings["urlApi"];
            var config = new HttpSelfHostConfiguration(url);
            config.Routes.MapHttpRoute(
                name: "API",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());

            HttpSelfHostServer server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();
        }
    }
}
