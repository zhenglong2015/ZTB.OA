using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCall.Models
{
    public class ProductWaper
    {
        public ProductWaper()
        {
            Code = -1;
        }
        /// <summary>
        /// 返回代码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 商品集合
        /// </summary>
        public IEnumerable<Product> Products { get; set; }
    }
}
