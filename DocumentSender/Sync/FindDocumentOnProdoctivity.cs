using DocumentSender.Model;
using Optional;
using Optional.Unsafe;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace DocumentSender
{
    /// <summary>
    /// Resolve prodoctivity request
    /// </summary>
    public class FindDocumentOnProdoctivity
    {
        readonly string _coordinatorSiteApi;
        readonly string _username;
        readonly string _password;
        readonly string _apiKey;
        readonly string _apiKeyParameterName;

        /// <summary>
        /// Create a new instance of <see cref="FindDocumentOnProdoctivity"/>
        /// </summary>
        public FindDocumentOnProdoctivity()
        {
            _coordinatorSiteApi = ConfigurationManager.AppSettings["CoordinatorApiAddress"];
            _username = ConfigurationManager.AppSettings["ProDoctivityApiUsername"] + "@" + ConfigurationManager.AppSettings["Agent"];
            _password = ConfigurationManager.AppSettings["ProDoctivityApiPassword"];
            _apiKey = ConfigurationManager.AppSettings["ApiKey"];
            _apiKeyParameterName = ConfigurationManager.AppSettings["ApiKeyHeaderName"];
        }

        /// <summary>
        /// Retrieve document form Prodoctivity 
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public ProdoctivityDocument GetProdoctivityDocument(DocumentId handler)
        {
            var result = GetDataFromService(handler, true);
            if (result.HasValue)
            {
                return result.ValueOrFailure();
            }

            var error = "";
            result.MatchNone(x => error = x.Message);
            throw new ArgumentException(error);
        }

        private Option<ProdoctivityDocument, Exception> GetDataFromService(DocumentId handler, bool isPdf)
            => Utils.GetDataFromRequest<ProdoctivityDocument>(_coordinatorSiteApi, $"documents/{handler.Value}?documentVersion=LatestVersion&pdf={isPdf}",
                new List<string>(), null, _username, _password, new Dictionary<string, string>
                {
                    {_apiKeyParameterName, _apiKey}
                }, MethodType.Get);



    }
}
