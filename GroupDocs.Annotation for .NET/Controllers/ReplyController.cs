using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.Domain.Results;
using GroupDocs.Annotation.Handler;
using GroupDocs.Annotation_for.NET.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupDocs.Annotation_for.NET.Controllers
{
    [RoutePrefix("reply")]
    public class ReplyController : Controller
    {
        // GET: Reply
        [HttpGet]
        [Route("")]
        public ActionResult Get(string guid)
        {
            Response.AddHeader("Content-Type", "application/json");
            AnnotationImageHandler imageHandler = Utils.createAnnotationImageHandler();

            //String guid = request.getParameter("guid");

            DeleteReplyResult result = imageHandler.DeleteAnnotationReply(guid);
            return Content(JsonConvert.SerializeObject(
                                    result,
                                    Formatting.Indented,
                                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }
                                     ), "application/json");

        }
        [HttpPost]
        [Route("")]
        public ActionResult Post(string guid)
        {
            Response.AddHeader("Content-Type", "application/json");
            AnnotationImageHandler imageHandler = Utils.createAnnotationImageHandler();
            //String guid = request.getParameter("guid");
            String section = Request.Params["section"];

            switch (section)
            {
                case "message":
                    AnnotationReplyInfo info = JsonConvert.DeserializeObject(new StreamReader(Request.InputStream).ReadToEnd()) as AnnotationReplyInfo; ;
                    EditReplyResult result = imageHandler.EditAnnotationReply(guid, info.Message);
                    return Content(JsonConvert.SerializeObject(
                                    result,
                                    Formatting.Indented,
                                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }
                                     ), "application/json");
                default:
                    return new HttpNotFoundResult("Not found");
            }


        }
    }
}