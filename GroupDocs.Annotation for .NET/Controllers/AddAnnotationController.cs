using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.Domain.Results;
using GroupDocs.Annotation.Handler;
using GroupDocs.Annotation.Handler.Input;
using GroupDocs.Annotation.Handler.Input.DataObjects;
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
    [RoutePrefix("annotation/add")]
    public class AddAnnotationController : Controller
    {
        // GET: AddAnnotation
        [Route("")]
        public ActionResult Post(string file)
        {
            Response.AddHeader("Content-Type", "application/json");
            AnnotationImageHandler imageHandler = Utils.createAnnotationImageHandler();
            IDocumentDataHandler documentDataHandler = imageHandler.GetDocumentDataHandler();

            String filename = file;
            Document doc = documentDataHandler.GetDocument(filename);
            long documentId = doc != null ? doc.Id : imageHandler.CreateDocument(filename);

            Request.InputStream.Seek(0, SeekOrigin.Begin);
            AnnotationInfo annotation = new JsonSerializer().Deserialize<AnnotationInfo>(new JsonTextReader(new StreamReader(Request.InputStream)));
            annotation.DocumentGuid = documentId;
            CreateAnnotationResult result = imageHandler.CreateAnnotation(annotation);
            return Content(JsonConvert.SerializeObject(
                                        result,
                                        Formatting.Indented,
                                        new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }
                                         ), "application/json");

        }
    }
}

