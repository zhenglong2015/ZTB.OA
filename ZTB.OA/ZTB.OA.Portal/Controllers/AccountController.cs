using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZTB.OA.Common;

namespace ZTB.OA.Portal.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateValidateCode()
        {
            ValidateCode vc = new ValidateCode();
            Bitmap map = vc.CreateValidateCode();

            MemoryStream stream = new MemoryStream();
            map.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            stream.Seek(0, 0);

            Session["Vcode"] = vc.strValidateCode;

            return File(stream.ToArray(), @"image/jpeg");
        }
    }



}