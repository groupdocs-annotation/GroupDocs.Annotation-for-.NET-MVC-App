using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GroupDocs.Annotation.Handler;
using GroupDocs.Annotation.Config;
using GroupDocs.Annotation;
using System.IO;
using System.Configuration;
using GroupDocs.Annotation.Handler.Input.DataObjects;
using GroupDocs.Annotation.Handler.Input;
using System.Runtime.CompilerServices;

namespace GroupDocs.Annotation_for.NET.Models
{
    public class Utils
    {

        public static AnnotationImageHandler createAnnotationImageHandler()
        {
            AnnotationConfig cfg = new AnnotationConfig();
            cfg.StoragePath = getStoragePath();
            AnnotationImageHandler annotator = new AnnotationImageHandler(cfg);
            return annotator;
        }

        public static string getStoragePath()
        {
            return AppDomain.CurrentDomain.GetData("DataDirectory") + ConfigurationManager.AppSettings["StoragePath"];
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static Document findDocumentByName(string filename)
        {
            AnnotationImageHandler imageHandler = Utils.createAnnotationImageHandler();
            IDocumentDataHandler documentDataHandler = imageHandler.GetDocumentDataHandler();
            Document doc = documentDataHandler.GetDocument(filename);
            if (doc != null)
            {
                return doc;
            }

            long documentId = imageHandler.CreateDocument(filename);

//            using (FileStream original = new FileStream(Utils.getStoragePath() + "/" + filename,FileMode.Create)) {
//                imageHandler.ImportAnnotations(original, documentId);
//            } 
            return documentDataHandler.Get(documentId);
        }
    }
}