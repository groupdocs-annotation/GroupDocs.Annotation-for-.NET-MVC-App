using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.Handler;
using GroupDocs.Annotation.Handler.Input.DataObjects;
using GroupDocs.Annotation_for.NET.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupDocs.Annotation_for.NET.Controllers
{
    [RoutePrefix("download/annotated")]
    public class DownloadAnnotatedController : Controller
    {
        [Route("")]
        public ActionResult Get(string file)
        {
            AnnotationImageHandler imageHandler = Utils.createAnnotationImageHandler();
            String filename = file;

            Document document = Utils.findDocumentByName(filename);

            List<AnnotationInfo> list = imageHandler.GetAnnotations(document.Id).Annotations.ToList<AnnotationInfo>();

            Stream exported;
            using (FileStream original = new FileStream(Utils.getStoragePath() + "/" + filename,FileMode.Open)) {
                exported = imageHandler.ExportAnnotationsToDocument(original, list);
            }

            Response.AddHeader("Content-Type", "application/octet-stream");
            //Response.AddHeader("Content-Disposition", "attachment; filename=\"" + filename + "\"");

            using (var ms = new MemoryStream())
            {
                exported.CopyTo(ms);
                return File(ms.ToArray(), "application/octet-stream", file);
            }
        }
    }
}