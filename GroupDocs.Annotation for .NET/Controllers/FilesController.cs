using System.IO;
using System.Collections;
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
            DirectoryInfo d = new DirectoryInfo(this.Server.MapPath("~/App_Data/Storage"));
            ArrayList result = new ArrayList();
            foreach (FileInfo f in d.GetFiles())
            {
                if (f.Name == "README.txt" || f.Name.StartsWith("GroupDocs.") || f.Name.StartsWith("."))
                {
                    continue;
                }
                result.Add(f.Name);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}

