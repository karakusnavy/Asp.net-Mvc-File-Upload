using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileUpload_With_Mvc.Controllers
{
    public class HomeController : Controller
    {
        private bool FileType(string ContentType)
        {
            return ContentType.Equals("image/png") || ContentType.Equals("image/gif") || ContentType.Equals("image/jpg") || ContentType.Equals("image/jpeg");
        }
        private bool FileSize(int ContentLenght)
        {
            return ((ContentLenght / 1024) / 1024) < 1;
        }

        //------------------------

        [Route("")]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [Route("")]
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase image)
        {
            if (!FileType(image.ContentType))
            {
                // file type is suitable ?
                ViewBag.Error = "File Type Error ! Please just: .png .jpg jpeg .gif";
                return View();
            }
            if (!FileSize(image.ContentLength))
            {
                // file size is good ?
                ViewBag.Error = "File Size Error ! File size is very big.";
                return View();
            }

            // if file is ready ? now upload my content file this image.
            var pathname = Path.GetFileName(image.FileName);
            var path = Path.Combine(Server.MapPath("~/Content"), pathname);
            image.SaveAs(path);
            ViewBag.Image = pathname;
            return View("Show");
        }       
        [Route("show")]
        [HttpGet]
        public ActionResult Show()
        { 
            //showing image page
            return View();
        }
    }
}