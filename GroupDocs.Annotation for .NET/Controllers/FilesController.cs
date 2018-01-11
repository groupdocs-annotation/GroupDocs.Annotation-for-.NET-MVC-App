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
    [RoutePrefix("files")]
    public class FilesController : Controller
    {
        // GET: Document
        [Route("")]
        public ActionResult Get(string file)
        {
            Response.AddHeader("Content-Type", "application/json");
            return Content("[\"Test - Copy.pdf\"]");
        }
    }
}
