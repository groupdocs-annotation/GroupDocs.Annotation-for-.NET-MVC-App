using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.Domain.Containers;
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
    [RoutePrefix("page/text")]
    public class PageTextController : Controller
    {
        [Route("")]
        public ActionResult Get(string file)
        {
            Response.AddHeader("Content-Type", "application/json");
            AnnotationImageHandler handler = Utils.createAnnotationImageHandler();

            int pageNumber = int.Parse(Request.Params["page"]);
            String filename = file;

            List<RowData> result = new List<RowData>();
            DocumentInfoContainer documentInfoContainer = handler.GetDocumentInfo(filename);
            foreach (PageData pageData in documentInfoContainer.Pages)
            {
                if (pageData.Number == pageNumber)
                {
                    result = pageData.Rows;
                    break;
                }
            }
            return Content(JsonConvert.SerializeObject(
                                    result,
                                    Formatting.Indented,
                                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }
                                     ), "application/json");

        }
    }
}