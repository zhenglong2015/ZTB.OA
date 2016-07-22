// ==========================================
// Author                  :  ZTB 
// Create Time           :    2016/7/22 13:43:38
// ==========================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTB.OA.Model.ResponseMessage
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }
}
