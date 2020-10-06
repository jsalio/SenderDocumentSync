using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SenderDocumentSync.Controllers
{
    [RoutePrefix("document")]
    public class DocumentController : ApiController
    {

        [Route("send-document/{handler}")]
        [HttpPost]
        public IHttpActionResult SendDocumentToOnBase(long handler)
        {
            return Ok();
        }

        [Route("document-mapping")]
        [HttpGet]
        public IHttpActionResult GetDocumentMapping()
        {
            return Ok();
        }

    }
}
