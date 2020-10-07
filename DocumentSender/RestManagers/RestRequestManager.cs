using DocumentSender.RestManagers;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace DocumentSender
{
    /// <summary>
    /// Responsible of implements operation on <see cref="IRestRequestResolver{T}"/> using <see cref="RestSharp"/>
    /// </summary>
    /// <typeparam name="T"> Object to send into request</typeparam>
    public sealed class RestRequestManager<T> : IRestRequestResolver<CaptureRestClientRequest<T>>
    {
        private readonly CaptureRestClientRequest<T> _client;
        private readonly RestClient _restClient;
        private readonly List<KeyValuePair<string, string>> _headers;
        private readonly Dictionary<string, string> _requetsHeaders;
        private bool _isBodyRequired;

        /// <summary>
        /// Created a new instance of <see cref="RestRequestManager{T}"/>
        /// </summary>
        /// <param name="captureRestClientRequest">An instance of <see cref="CaptureRestClientRequest{T}"/></param>
        public RestRequestManager(CaptureRestClientRequest<T> captureRestClientRequest)
        {
            _client = captureRestClientRequest;
            _headers = new List<KeyValuePair<string, string>>();
            _restClient = BuildRestClient();
            _requetsHeaders = new Dictionary<string, string>();
        }

        private RestClient BuildRestClient()
        {
            var client = new RestClient(_client.CaptureClient.ClientServer)
            {
                Timeout = (int)_client.CaptureClient.Timeout * 1000
            };
            return client;
        }

        private bool IsValidUrl()
        {
            var isValidUrl = Uri.TryCreate($"{_client.CaptureClient.ClientServer}/{_client.CaptureClient.ClientResource}", UriKind.Absolute, out Uri uriResult)
                              && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            return isValidUrl;
        }

        CaptureClientRestResponse IRestRequestResolver<CaptureRestClientRequest<T>>.SendRequest()
        {
            RestRequest restRequest = BuildRestRequest();
            IRestResponse response = _restClient.Execute(restRequest);

            return response.IsSuccessful
                ? BuildSuccessResponse(response.Content)
                : BuildFailResponse(response.ErrorMessage, response.StatusCode);
        }

        private static CaptureClientRestResponse BuildFailResponse(string responseErrorMessage, HttpStatusCode statusCode)
        {
            return new CaptureClientRestResponse
            {
                Content = "",
                StatusCode = (int)statusCode,
                ErrorMessage = responseErrorMessage
            };
        }

        private static CaptureClientRestResponse BuildSuccessResponse(string responseContent)
        {
            return new CaptureClientRestResponse
            {
                Content = responseContent,
                StatusCode = 200,
                ErrorMessage = ""
            };
        }

        private RestRequest BuildRestRequest()
        {
            var restRequest = new RestRequest(_client.CaptureClient.ClientResource, TranslateHttpVerb(_client.CaptureClient.MethodType));
            AddHeaders(restRequest);
            if (_isBodyRequired || _client.CaptureClient.MethodType != MethodType.Get)
            {
                restRequest.AddJsonBody(_client.RequestBody);
                restRequest.Parameters[0].ContentType = "json";
            }
            return restRequest;
        }

        private void AddHeaders(IRestRequest restRequest)
        {
            foreach (var header in _requetsHeaders)
            {
                restRequest.AddHeader(header.Key, header.Value);
            }
        }

        BasicOperationResponse<CaptureRestClientRequest<T>> IRestRequestResolver<CaptureRestClientRequest<T>>.ValidateResolver()
        {

            if (!IsValidUrl())
            {
                return BasicOperationResponse<CaptureRestClientRequest<T>>.Fail("InvalidUrl");
            }

            if (_isBodyRequired && _client.RequestBody == null)
            {
                return BasicOperationResponse<CaptureRestClientRequest<T>>.Fail("NotificationDataIsEmpty");
            }

            var duplicateKey = _headers.GroupBy(x => x.Key).FirstOrDefault(x => x.Count() > 1);
            if (duplicateKey != null)
            {
                return BasicOperationResponse<CaptureRestClientRequest<T>>.Fail(
                    $"Duplicate header key detected :{duplicateKey.Key}");
            }

            foreach (var keyValuePair in _headers)
            {
                _requetsHeaders.Add(keyValuePair.Key, keyValuePair.Value);
            }

            return BasicOperationResponse<CaptureRestClientRequest<T>>.Ok(_client);
        }

        private static Method TranslateHttpVerb(MethodType methodtype)
            => (Method)Enum.Parse(typeof(Method), methodtype.ToString().ToUpper());

        void IRestRequestResolver<CaptureRestClientRequest<T>>.AddHeaders(KeyValuePair<string, string> headerPair)
            => _headers.Add(headerPair);

        void IRestRequestResolver<CaptureRestClientRequest<T>>.RequiredBody(bool requiredBody)
            => _isBodyRequired = requiredBody;

        void IRestRequestResolver<CaptureRestClientRequest<T>>.AddAuthentication(string username, string password)
        {
            if (_restClient == null)
            {
                throw new ArgumentException("the client request is null");
            }
            _restClient.Authenticator = new HttpBasicAuthenticator(username, password);
        }
    }
}