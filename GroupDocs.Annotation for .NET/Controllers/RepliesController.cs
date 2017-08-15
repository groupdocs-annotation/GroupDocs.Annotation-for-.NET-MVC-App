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
    [RoutePrefix("replies")]
    public class RepliesController : Controller
    {
        // GET: Replies
        [HttpGet]
        [Route("")]
        public ActionResult Get(string guid)
        {
            Response.AddHeader("Content-Type", "application/json");
            AnnotationImageHandler imageHandler = Utils.createAnnotationImageHandler();
            //String guid = request.getParameter("guid");

            GetAnnotationResult annotationResult = imageHandler.GetAnnotation(guid);
            if (annotationResult == null)
            {
                return new HttpNotFoundResult("Not found");
            }
            long annotationId = annotationResult.Id;
            AnnotationReplyInfo[] list = imageHandler.ListAnnotationReplies(annotationId).Replies;

            return Content(JsonConvert.SerializeObject(
                                    list,
                                    Formatting.Indented,
                                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }
                                     ), "application/json");

        }
        [HttpPut]
        [Route("")]
        public ActionResult Put(string guid)
        {
            Response.AddHeader("Content-Type", "application/json");
            AnnotationImageHandler imageHandler = Utils.createAnnotationImageHandler();

            AnnotationReplyInfo info = JsonConvert.DeserializeObject(new StreamReader(Request.InputStream).ReadToEnd()) as AnnotationReplyInfo;
                

        long annotationId = imageHandler.GetAnnotation(info.Guid).Id;

        AddReplyResult result = imageHandler.CreateAnnotationReply(annotationId, "");
       
            return Content(JsonConvert.SerializeObject(
                                    result,
                                    Formatting.Indented,
                                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }
                                     ), "application/json");

        }
    }
}