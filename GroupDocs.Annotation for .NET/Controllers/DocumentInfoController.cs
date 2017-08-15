using GroupDocs.Annotation.Domain.Containers;
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
    [RoutePrefix("document/info")]
    public class DocumentInfoController : Controller
    {
        // GET: Document
        [Route("")]
        public ActionResult Get(string file)
        {
            AnnotationImageHandler handler = Utils.createAnnotationImageHandler();
            String filename = file;
            DocumentInfoContainer result = handler.GetDocumentInfo(filename);
            Response.AddHeader("Content-Type", "application/json");
            return Content(JsonConvert.SerializeObject(
                                    result,
                                    Formatting.Indented,
                                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }
                                     ), "application/json");

        }
    }
}
