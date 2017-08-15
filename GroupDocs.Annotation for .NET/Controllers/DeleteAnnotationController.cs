using GroupDocs.Annotation.Domain.Results;
using GroupDocs.Annotation.Handler;
using GroupDocs.Annotation_for.NET.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupDocs.Annotation_for.NET.Controllers
{
    [RoutePrefix("annotation/delete")]
    public class DeleteAnnotationController : Controller
    {
        [Route("")]
        public ActionResult Get(string file)
        {

            Response.AddHeader("Content-Type", "application/json");
            AnnotationImageHandler imageHandler = Utils.createAnnotationImageHandler();
            String filename = file;// request.getParameter("file");
            long annotationId = long.Parse(Request.Params["annotationId"]);

            DeleteAnnotationResult result = imageHandler.DeleteAnnotation(annotationId);
            
            return Content(JsonConvert.SerializeObject(
                                    result,
                                    Formatting.Indented,
                                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }
                                     ), "application/json");
        }
    }
}