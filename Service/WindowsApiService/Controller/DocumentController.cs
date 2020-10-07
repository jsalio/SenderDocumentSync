using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DocumentSender;
using Optional.Unsafe;

namespace WindowsApiService.Controller
{
    public class DocumentController:ApiController
    {
        readonly SendDocumentToOnBase _sender = new SendDocumentToOnBase();
        private readonly OnBaseReleaser _releaser;

        public DocumentController()
        {

        }

        public DocumentController(OnBaseReleaser releaser)
        {
            _releaser = releaser;
        }

        /// <summary>
        /// Retrieve map configuration
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public string GetDocumentMapping()
        {
            var keywords = _sender.CastSettingToKeywords();
            return Newtonsoft.Json.JsonConvert.SerializeObject(keywords);
        }

        [HttpPost]
        public object SaveDocumentOnOnBase(long handler)
        {
            var newHandler = _sender.SaveDocument(handler, _releaser);
            ValidateResult(newHandler);
            return new
            {
                Handler= newHandler.ValueOrFailure()
            };
        }

        private void ValidateResult(Optional.Option<long, Exception> newHandler)
        {
            if (!newHandler.HasValue)
            {
                newHandler.MatchNone(none => throw new Exception($"Error on send document {none.Message}"));
            }
        }
    }
}
