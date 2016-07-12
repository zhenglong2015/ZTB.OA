using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApiWithOath.Controllers
{
    /// <summary>
    /// APIHelp测试
    /// </summary>
    public class HomeController : ApiController
    {
        /// <summary>
        /// // GET: Home
        /// </summary>
        /// <returns></returns>
        [HttpGet]
       // [Authorize]
        public string Index()
        {
            return DateTime.Now.ToString();
        }
    }
}