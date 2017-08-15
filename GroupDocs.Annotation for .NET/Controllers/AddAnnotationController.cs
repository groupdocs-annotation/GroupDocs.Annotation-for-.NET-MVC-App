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

            //StreamReader stream = new StreamReader(Request.InputStream);
            //string x = stream.ReadToEnd();  // added to view content of input stream

            AnnotationInfo annotation = new AnnotationInfo(); //Request.InputStream as AnnotationInfo;
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