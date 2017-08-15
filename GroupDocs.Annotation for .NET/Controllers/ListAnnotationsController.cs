using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.Domain.Results;
using GroupDocs.Annotation.Handler;
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
    [RoutePrefix("annotation/list")]
    public class ListAnnotationsController : Controller
    {
        // GET: Document
        [Route("")]
        public ActionResult Get(string file)
        {
            Response.AddHeader("Content-Type", "application/json");
            AnnotationImageHandler imageHandler = Utils.createAnnotationImageHandler();
            String filename = file;

            Document doc = Utils.findDocumentByName(filename);
            ListAnnotationsResult listResult = imageHandler.GetAnnotations(doc.Id);

            List<GetAnnotationResult> list = new List<GetAnnotationResult>();
            foreach (AnnotationInfo inf in listResult.Annotations)
            {
                list.Add(imageHandler.GetAnnotation(inf.Guid));
            }

            return Content(JsonConvert.SerializeObject(
                                    list,
                                    Formatting.Indented,
                                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }
                                     ), "application/json");


        }
    }
}