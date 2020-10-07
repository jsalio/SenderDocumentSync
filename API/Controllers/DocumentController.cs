using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using DocumentSender;
using Optional.Unsafe;

namespace SenderDocumentSync.Controllers
{
    [System.Web.Http.RoutePrefix("document")]
    public class DocumentController : ApiController
    {
        readonly SendDocumentToOnBase _sender = new SendDocumentToOnBase();

        /// <summary>
        /// Send Document from prodoctivity to OnBase
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        [System.Web.Http.Route("send-document/{handler}")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult SendDocumentToOnBase(long handler)
        {
            var newHandler = _sender.SaveDocument(handler, new OnBaseReleaser());
            ValidateResult(newHandler);
            return Ok(new
            {
                ObStoreId =  newHandler.ValueOrFailure()
            });
        }

        private void ValidateResult(Optional.Option<long, Exception> newHandler)
        {
            if (!newHandler.HasValue)
            {
                newHandler.MatchNone(none => throw new Exception($"Error on send document {none.Message}"));
            }
        }

        /// <summary>
        /// Retrieve map configuration
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.Route("document-mapping")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetDocumentMapping()
        {
            var keywords = _sender.CastSettingToKeywords();
            return Ok(keywords);
        }

    }
}
