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
    [RoutePrefix("annotation")]
    public class AnnotationController : Controller
    {
        [HttpGet]
        [Route("")]
        public ActionResult Get(string guid)
        {
            Response.AddHeader("Content-Type", "application/json");

            AnnotationImageHandler imageHandler = Utils.createAnnotationImageHandler();
            return Content(JsonConvert.SerializeObject(
                                    imageHandler.GetAnnotation(guid).Annotation,
                                    Formatting.Indented,
                                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }
                                     ), "application/json");
        }
        [HttpDelete]
        public ActionResult Delete(string guid)
        {
            Response.AddHeader("Content-Type", "application/json");
            AnnotationImageHandler imageHandler = Utils.createAnnotationImageHandler();
            long annotationId = imageHandler.GetAnnotation(guid).Id;
            DeleteAnnotationResult result = imageHandler.DeleteAnnotation(annotationId);
            return Content(JsonConvert.SerializeObject(
                                    result,
                                    Formatting.Indented,
                                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }
                                     ), "application/json");


        }
        [HttpPost]
        public ActionResult Post()
        {
            Response.AddHeader("Content-Type", "application/json");
            AnnotationImageHandler imageHandler = Utils.createAnnotationImageHandler();
            String guid = Request.Params["guid"];
            String section = Request.Params["guid"];
            AnnotationInfo annotationInfo = imageHandler.GetAnnotation(guid).Annotation;
            long annotationId = imageHandler.GetAnnotation(guid).Id;

            switch (section)
            {
                case "fieldtext":
                    var jsonString = String.Empty;
                    using (var inputStream = new StreamReader(Request.InputStream))
                    {
                        jsonString = inputStream.ReadToEnd();
                    }
                    TextFieldInfo info = JsonConvert.DeserializeObject<TextFieldInfo>(jsonString);

                    SaveAnnotationTextResult result = imageHandler.SaveTextField(annotationId, info);
                    return Content(JsonConvert.SerializeObject(
                                               result,
                                               Formatting.Indented,
                                               new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }
                                                ), "application/json");
                case "position":
                    var jsonStringPos = String.Empty;
                    using (var inputStream = new StreamReader(Request.InputStream))
                    {
                        jsonStringPos = inputStream.ReadToEnd();
                    }
                    Point point = JsonConvert.DeserializeObject<Point>(jsonStringPos);

                    MoveAnnotationResult moveresult = imageHandler.MoveAnnotationMarker(annotationId, point, annotationInfo.PageNumber);
                    return Content(JsonConvert.SerializeObject(
                                               moveresult,
                                               Formatting.Indented,
                                               new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }
                                                ), "application/json");
                default:
                    return Content(null);
            }
        }



        public ActionResult Index(string guid)
        {
            return View();
        }
        // GET: Annotation
        public ActionResult List(string file)
        {
            Response.AddHeader("Content-Type", "application/json");
            AnnotationImageHandler handler = Utils.createAnnotationImageHandler();
            String filename = file;
            Document doc = Utils.findDocumentByName(filename);
            ListAnnotationsResult listResult = handler.GetAnnotations(doc.Id);

            List<GetAnnotationResult> list = new List<GetAnnotationResult>();
            foreach (AnnotationInfo inf in listResult.Annotations)
            {
                list.Add(handler.GetAnnotation(inf.Guid));
            }
            return Content(JsonConvert.SerializeObject(
                                    list,
                                    Formatting.Indented,
                                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }
                                     ), "application/json");


            //Response.Write(list);
            /*
             * 
             * response.setHeader("Content-Type", "application/json");
        AnnotationImageHandler imageHandler = Utils.createAnnotationImageHandler();
        String filename = request.getParameter("file");

        Document doc = Utils.findDocumentByName(filename);
        ListAnnotationsResult listResult = imageHandler.getAnnotations(doc.getId());

        ArrayList<GetAnnotationResult> list = new ArrayList<>();
        for (AnnotationInfo inf : listResult.getAnnotations()) {
            list.add(imageHandler.getAnnotation(inf.getGuid()));
        }

        new ObjectMapper().writeValue(response.getOutputStream(), list);*/


            return View();
        }
        public ActionResult Add2(string file)
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
