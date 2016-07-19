using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZTB.OA.IBLL;

namespace ZTB.OA.Web.Controllers
{
    public class ManageController : BaseController
    {

        public IUserInfoService UserInfoService { get; set; }
        // 修改头像
        public ActionResult ModifyHead()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ProModifyHead()
        {
            int x1 = Convert.ToInt32(Request["x1"]);
            int x2 = Convert.ToInt32(Request["x2"]);
            int y1 = Convert.ToInt32(Request["y1"]);
            int y2 = Convert.ToInt32(Request["y2"]);

            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;

            HttpPostedFile file = files[0];
            file.SaveAs(Server.MapPath("~/Upload/" + file.FileName));

            //设置缩略图
            int Thumbnailwidth = 400;
            int Thumbnailheight = 300;
            //新建一个bmp图片  
            Bitmap bitmap = new Bitmap(Thumbnailwidth, Thumbnailheight);

            //新建一个画板  
            Graphics graphic = Graphics.FromImage(bitmap);

            //设置高质量插值法  
            graphic.InterpolationMode = InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度  
            graphic.SmoothingMode = SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充  
            graphic.Clear(Color.Transparent);

            //原图片
            Bitmap originalImage = new Bitmap(file.InputStream);

            //在指定位置并且按指定大小绘制原图片的指定部分  
            graphic.DrawImage(originalImage, new Rectangle(0, 0, Thumbnailwidth, Thumbnailheight),
                new Rectangle(0, 0, originalImage.Width, originalImage.Height), GraphicsUnit.Pixel);

            //得到缩略图
            Image ThumbnailImage = Image.FromHbitmap(bitmap.GetHbitmap());

            //创建选择图片
            Bitmap selectbitmap = new Bitmap(x2 - x1, y2 - y1);

            //新建一个画板  
            Graphics selectgraphic = Graphics.FromImage(selectbitmap);

            //裁切
            selectgraphic.DrawImage(ThumbnailImage, 0, 0, new Rectangle(x1, y1, x2 - x1, y2 - y1), GraphicsUnit.Pixel);

            //保存
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            string url = "/Upload/" + (DateTime.Now - startTime).TotalSeconds.ToString().Replace(".","")+ file.FileName;
            selectbitmap.Save(Server.MapPath(url), ImageFormat.Jpeg);

            originalImage.Dispose();
            selectbitmap.Dispose();
            selectgraphic.Dispose();
            return Content(url);
        }

        //个人信息
        public ActionResult UseProfile()
        {
            return View();
        }

        public ActionResult ModifyPwd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ModifyPwd(string oldPwd, string newPwd)
        {
            if (!string.IsNullOrEmpty(oldPwd) && oldPwd == base.UserInfo.Pwd)
            {
                string userId = Request.Cookies["LoginUser"].Value;

                var user = UserInfoService.GetEntities(u => u.Id == UserInfo.Id).FirstOrDefault();
                user.Pwd = newPwd;
                UserInfoService.Update(user);
                Response.Cookies["LoginUser"].Value = null;
                return Content("ok");

            }
            else
            {
                return Content("原密码输入错误");
            }
        }


        //联系我们
        public ActionResult Contacts()
        {
            return View();
        }
    }
}