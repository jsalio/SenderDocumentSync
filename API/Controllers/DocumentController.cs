using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DocumentSender;

namespace SenderDocumentSync.Controllers
{
    [RoutePrefix("document")]
    public class DocumentController : ApiController
    {
        readonly SendDocumentToOnBase send = new SendDocumentToOnBase();

        [Route("send-document/{handler}")]
        [HttpPost]
        public IHttpActionResult SendDocumentToOnBase(long handler)
        {
            var jdoc = Newtonsoft.Json.JsonConvert.DeserializeObject("{}");

            send.SaveDocument(handler, new OnBaseReleaser());
            return Ok();
        }

        [Route("document-mapping")]
        [HttpGet]
        public IHttpActionResult GetDocumentMapping()
        {
            var keywords = send.CastSettingToKeywords();
            //var keywords = ConfigurationManager.AppSettings["keywords"].Split(';')
            //    .Select(x =>
            //{
            //    string[] currentPair = x.Split(',');
            //    return new KeyValuePair<string, string>(currentPair[0], currentPair[1]);
            //}).ToDictionary(y => y.Key, z => z.Value);

            return Ok(keywords);
        }

    }
}
