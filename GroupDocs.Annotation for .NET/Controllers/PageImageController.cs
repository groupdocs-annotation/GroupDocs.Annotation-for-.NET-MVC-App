using GroupDocs.Annotation.Domain.Image;
using GroupDocs.Annotation.Domain.Options;
using GroupDocs.Annotation.Handler;
using GroupDocs.Annotation_for.NET.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupDocs.Annotation_for.NET.Controllers
{
    [RoutePrefix("page/image")]
    public class PageImageController : Controller
    {
        [Route("")]
        public ActionResult Get(string file)
        {
            Response.AddHeader("Content-Type", "image/png");
            AnnotationImageHandler handler = Utils.createAnnotationImageHandler();

            ImageOptions o = new ImageOptions();
            int pageNumber = int.Parse(Request.Params["page"]);
            o.PageNumbersToConvert = new List<int>(pageNumber);
            o.PageNumber = pageNumber;
            o.CountPagesToConvert = 1;
            if (!string.IsNullOrEmpty(Request.Params["width"]))
            {
                o.Width = int.Parse(Request.Params["width"]);
            }
            if (!string.IsNullOrEmpty(Request.Params["height"]))
            {
                o.Height = int.Parse(Request.Params["height"]);
            }

            Stream stream = null;
            List<PageImage> list = handler.GetPages(file, o);
            foreach (PageImage pageImage in list.Where(x => x.PageNumber == pageNumber))
            {
                stream = pageImage.Stream;
            };
            if (stream != null && stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
                return new FileStreamResult(stream, "image/png");
            }
            else
            {
                return new HttpNotFoundResult("Document page " + pageNumber + " not found");
            }

        }
    }
}

