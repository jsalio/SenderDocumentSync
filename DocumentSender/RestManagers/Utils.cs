using System;
using System.Collections.Generic;
using System.Linq;
using DocumentSender.RestManagers;
using Optional;

namespace DocumentSender
{
    internal class Utils
    {

        static JsonHandlerAdapter JsonHandler = new JsonHandlerAdapter();

        /// <summary>
        /// Send request to server with authentication.
        /// </summary>
        /// <typeparam name="T">Type of object to retrieve or send</typeparam>
        /// <param name="provider">Server name provide for <see cref="DocumentStrategyApiProviders"/></param>
        /// <param name="path">resource path</param>
        /// <param name="param">Request parameter if required</param>
        /// <param name="body">request body if required</param>
        /// <param name="password"></param>
        /// <param name="method">type of <see cref="MethodType"/></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public static Option<T, Exception> GetDataFromRequest<T>(string provider, string path, IList<string> param, T body, string username, string password, Dictionary<string, string> headers, MethodType method)
        {
            var client = BuildClientRequest(provider, path, param, body, method);
            IRestRequestResolver<CaptureRestClientRequest<T>> restClient = new RestRequestManager<T>(client);

            if (headers.Any())
            {
                foreach (var header in headers)
                {
                    restClient.AddHeaders(new KeyValuePair<string, string>(header.Key, header.Value));
                }

            }
            restClient.AddHeaders(new KeyValuePair<string, string>("Content-type", "application/json"));
            restClient.RequiredBody(requiredBody: false);
            restClient.AddAuthentication(username, password);
            restClient.ValidateResolver();

            

            var result = restClient.SendRequest();

            var dsa = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(result.Content);

            return result.StatusCode == 200 ?
                Option.Some<T, Exception>(Newtonsoft.Json.JsonConvert.DeserializeObject<T>(result.Content)) :
                Option.None<T, Exception>(new ArgumentException($"Failed get data from {provider}/{path}"));
        }

        /// <summary>
        /// Build new Capture client Request object
        /// </summary>
        /// <typeparam name="T">Type ob <see cref="object"/> to retrieve</typeparam>
        /// <param name="url">Server url</param>
        /// <param name="path">Server resource</param>
        /// <param name="params">resource params if required</param>
        /// <param name="body"><see cref="T"/> to send on request if required</param>
        /// <param name="method">Type of http verbs to use</param>
        /// <returns></returns>
        public static CaptureRestClientRequest<T> BuildClientRequest<T>(string url, string path, IList<string> @params, T body,
            MethodType method)
        {
            string uriParams = BuildUriParams(@params.ToList());
            string parameters = uriParams.Length > 0 ? uriParams : string.Empty;
            return new CaptureRestClientRequest<T>
            {
                CaptureClient = new CaptureRestClient
                {
                    MethodType = method,
                    ClientResource = $"{path}{parameters}",
                    ClientServer = url,
                    Timeout = 0
                },
                RequestBody = body
            };

        }

        private static string BuildUriParams(IReadOnlyCollection<string> uriParams)
        {
            var value = "";
            foreach (var uriParam in uriParams)
            {
                var lastParameters = uriParams.LastOrDefault();
                value += lastParameters != uriParam ? $"{uriParam}/" : $"{uriParam}";
            }
            return value;
        }
    }
}