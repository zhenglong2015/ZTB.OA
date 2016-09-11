using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeChatApi.Resources;

namespace WeChatApi.Entity.ReceiveEntity
{
    /// <summary>
    /// 错误信息
    /// </summary>
    public class ErrorEntity
    {
        /// <summary>
        /// 错误编码
        /// </summary>
        private int errCode;
        /// <summary>
        /// 错误描述
        /// </summary>
        public string ErrDescription { get; set; }

        public int ErrCode
        {
            get { return errCode; }
            set
            {
                errCode = value;
                //根据错误码，从错误列表中找到错误信息，并给ErrDescription赋值
                ErrDescription = ErrList.FirstOrDefault(e => e.Key == value).Value;
            }
        }
        private static Dictionary<int, string> errDic;
        public static Dictionary<int, string> ErrList
        {
            get
            {
                if (errDic != null && errDic.Count > 0)
                    return errDic;
                errDic = new Dictionary<int, string>();
                var temp = Code.CodeInfo.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var strArr in temp.Select(str => str.Split(new char[] { '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries)))
                {
                    if (!errDic.ContainsKey(int.Parse(strArr[0])))
                        errDic.Add(int.Parse(strArr[0]), strArr[1]);
                }
                return errDic;
            }
        }

    }
}
