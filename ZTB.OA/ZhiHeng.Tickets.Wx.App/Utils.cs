using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ZhiHeng.Tickets.Wx.App.Common;
using ZhiHeng.Tickets.Wx.App.Entity.MsgEntity;
using ZhiHeng.Tickets.Wx.App.Entity.ReceiveEntity;
using ZhiHeng.Tickets.Wx.App.Entity.SendEntity;
using System.Security.Cryptography;
using System.Web.Security;

namespace ZhiHeng.Tickets.Wx.App
{
    public class Utils
    {
        #region Http 请求
        /// <summary>
        /// Http Get方式请求数据
        /// </summary>
        /// <param name="url">请求的地址</param>
        /// <returns>响应数据</returns>
        public static string HttpGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            request.Accept = "*/*";
            string responseStr = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    responseStr = reader.ReadToEnd();
                }
            }
            return responseStr;
        }
        /// <summary>
        /// 以流的方式发送Http Post请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="stream">流</param>
        /// <returns>响应数据</returns>
        public static string HttpPost(string url, Stream stream)
        {
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback((a, b, c, d) => { return true; });
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "*/*";
            request.Timeout = 15000;
            request.AllowAutoRedirect = false;
            string responseStr = string.Empty;
            using (Stream reqstream = request.GetRequestStream())
            {
                stream.Position = 0L;
                stream.CopyTo(reqstream);
            }
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    responseStr = reader.ReadToEnd();
                }
            }
            return responseStr;
        }
        /// <summary>
        /// HTTP POST请求URL。
        /// </summary>
        /// <param name="url">请求的url</param>
        /// <param name="param">请求的参数</param>
        /// <param name="stream">如果响应的是文件，则此参数表示的是文件流</param>
        public static string HttpPost(string url, string param, out FileStreamInfo stream)
        {
            stream = null;
            //当请求为https时，验证服务器证书
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback((a, b, c, d) => { return true; });
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "*/*";
            request.Timeout = 15000;
            request.AllowAutoRedirect = false;
            string responseStr = "";
            using (StreamWriter requestStream = new StreamWriter(request.GetRequestStream()))
            {
                requestStream.Write(param);//将请求的数据写入到请求流中
            }
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {

                if (response.ContentType == "application/octet-stream")//如果响应的是文件流，则不将响应流转换成字符串
                {
                    stream = new FileStreamInfo();
                    response.GetResponseStream().CopyTo(stream);
                    #region 获取响应的文件名
                    Regex reg = new Regex(@"(\w+)\.(\w+)");
                    var result = reg.Match(response.GetResponseHeader("Content-disposition")).Groups;
                    stream.FileName = result[0].Value;
                    #endregion
                    responseStr = "";
                }
                else
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        responseStr = reader.ReadToEnd();//获取响应
                    }
                }

            }
            return responseStr;
        }
        /// <summary>
        /// 带验证证书的POST请求
        /// </summary>
        /// <param name="url">请求url</param>
        /// <param name="data">请求数据</param>
        /// <param name="certpath">证书路径</param>
        /// <param name="certpwd">证书密码</param>
        public static string HttpPost(string url, string data, string certpath = "", string certpwd = "")
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback((a, b, c, d) =>
            {
                if (d == SslPolicyErrors.None)
                    return true;
                return false;
            });
            if (!string.IsNullOrEmpty(certpath) && !string.IsNullOrEmpty(certpwd))
            {
               
                X509Certificate2 cer = new X509Certificate2(certpath, certpwd, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
                request.ClientCertificates.Add(cer);
            }
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "*/*";
            request.Timeout = 15000;
            request.AllowAutoRedirect = false;
            string responseStr = "";
            using (StreamWriter requestStream = new StreamWriter(request.GetRequestStream()))
            {
                requestStream.Write(data);//将请求的数据写入到请求流中
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    responseStr = reader.ReadToEnd();//获取响应
                }
            }
            return responseStr;
        }
        /// <summary>
        /// 发起Get请求，并获取请求返回值
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="url">接口地址</param>
        public static T GetResult<T>(string url)
        {
            return JsonConvert.DeserializeObject<T>(HttpGet(url));
        }
        /// <summary>
        /// 发起Post请求，并获取请求返回值
        /// </summary>
        public static T PostResult<T>(string url, string data)
        {
            return JsonConvert.DeserializeObject<T>(HttpPost(url, data));
        }
        /// <summary>
        /// 发起post请求，并获取请求返回值
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="obj">数据实体</param>
        /// <param name="url">接口地址</param>
        public static T PostResult<T>(List<FormEntity> formEntities, string url)
        {
            var retdata = HttpPostForm(url, formEntities);
            return JsonConvert.DeserializeObject<T>(retdata);
        }
        public static string HttpPostForm(string url, List<FormEntity> formEntities)
        {
            //分割字符串
            var boundary = "----" + DateTime.Now.Ticks.ToString("x");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback((a, b, c, d) => true);
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            MemoryStream stream = new MemoryStream();
            #region 将非文件表单写入到内存流中
            foreach (var entity in formEntities.Where(f => f.IsFile == false))
            {
                var temp = new StringBuilder();
                temp.AppendFormat("\r\n--{0}", boundary);
                temp.AppendFormat("\r\nContent-Disposition: form-data; name=\"{0}\"", entity.Name);
                temp.Append("\r\n\r\n");
                temp.Append(entity.Value);
                byte[] b = Encoding.UTF8.GetBytes(temp.ToString());
                stream.Write(b, 0, b.Length);
            }
            #endregion
            #region 将文件表单写入到内存流
            foreach (var entity in formEntities.Where(f => f.IsFile == true))
            {
                byte[] filedata = null;
                var filename = Path.GetFileName(entity.Value);
                //表示是网络资源
                if (entity.Value.Contains("http"))
                {
                    //处理网络文件
                    using (var client = new WebClient())
                    {
                        filedata = client.DownloadData(entity.Value);
                    }
                }
                else
                {
                    //处理物理路径文件
                    using (FileStream file = new FileStream(entity.Value, FileMode.Open, FileAccess.Read))
                    {

                        filedata = new byte[file.Length];
                        file.Read(filedata, 0, (int)file.Length);
                    }
                }
                var temp = string.Format("\r\n--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\n\r\n",
                   boundary, entity.Name, filename);
                byte[] b = Encoding.UTF8.GetBytes(temp);
                stream.Write(b, 0, b.Length);
                stream.Write(filedata, 0, filedata.Length);
            }
            #endregion
            //结束标记
            byte[] foot_data = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
            stream.Write(foot_data, 0, foot_data.Length);
            Stream reqStream = request.GetRequestStream();
            stream.Position = 0L;
            //将Form表单生成流写入到请求流中
            stream.CopyTo(reqStream);
            stream.Close();
            reqStream.Close();
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    return reader.ReadToEnd();//获取响应
                }
            }
        }
        /// <summary>
        /// 发起post请求，并获取请求返回值
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="obj">数据实体</param>
        /// <param name="url">接口地址</param>
        public static T PostResult<T>(object obj, string url)
        {
            //序列化设置
            var setting = new JsonSerializerSettings();
            //解决枚举类型序列化时，被转换成数字的问题
            setting.Converters.Add(new StringEnumConverter());
            var retdata = HttpPost(url, JsonConvert.SerializeObject(obj, setting));
            return JsonConvert.DeserializeObject<T>(retdata);
        }
        #endregion
        #region URl处理
        /// <summary>
        /// Url 编码
        /// </summary>
        public static string UrlEndcode(string str)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            return HttpContext.Current.Server.UrlEncode(str.Replace("'", ""));
        }
        /// <summary>
        /// Url 解码
        /// </summary>
        public static string UrlDecode(string str)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            return HttpContext.Current.Server.UrlDecode(str);
        }
        #endregion
        public static string Read(string path)
        {
            string result = string.Empty;
            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    StreamReader reader = new StreamReader(fs, Encoding.UTF8);
                    result = reader.ReadToEnd();
                }
            }
            return result;
        }

        public static void Write(string path, string content)
        {
            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Write))
                {
                    StreamWriter writer = new StreamWriter(fs, Encoding.UTF8);
                    writer.Flush();
                    writer.Write(content);
                }
            }
        }

        /// <summary>
        /// datetime转换为unixtime
        /// </summary>
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name=”timeStamp”></param>
        /// <returns></returns>
        public static string GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow).ToString(); ;
        }
        /// <summary>
        /// 获取当前请求的数据
        /// </summary>
        /// <returns></returns>
        public static string GetRequestData()
        {
            using (var stream = HttpContext.Current.Request.InputStream)
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        /// <summary>
        /// 获取微信发送过来的数据包。并处理加解密问题。如果进行了加密，则将密文解密后返回，否则直接返回接收到的字符
        /// </summary>
        /// <param name="param">加解密接入参数</param>
        /// <returns>如果进行了加密，则将密文解密后返回，否则直接返回接收到的字符</returns>
        public static string GetRequestData(EnterParam param)
        {
            var reqdata = GetRequestData();
            var timestamp = HttpContext.Current.Request.QueryString["timestamp"];
            var nonce = HttpContext.Current.Request.QueryString["nonce"];
            var msg_signature = HttpContext.Current.Request.QueryString["msg_signature"];
            var encrypt_type = HttpContext.Current.Request.QueryString["encrypt_type"];
            string postStr = string.Empty;
            if (encrypt_type == "aes")
            {
                //如果进行了加密，则加密成功后，直接发挥解密后的明文。解密失败则返回null
                param.IsAes = true;
                var ret = new WXBizMsgCrypt(param.Token, param.EncodingAESKey, param.Appid);
                if (ret.DecryptMsg(msg_signature, timestamp, nonce, reqdata, ref postStr) != 0)
                {
                    return null;
                }
                return postStr;
            }
            else
            {
                param.IsAes = false;
                return reqdata;
            }
        }
        /// <summary>
        /// 将微信xml数据转换成实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlstr"></param>
        /// <returns></returns>
        public static T ConvertObj<T>(string xmlstr)
        {
            try
            {
                XElement xdoc = XElement.Parse(xmlstr);
                //获取转换的数据类型
                var type = typeof(T);
                //创建实例
                var t = Activator.CreateInstance<T>();
                #region 基础属性赋值
                var toUserName = type.GetProperty("ToUserName");
                toUserName.SetValue(t, Convert.ChangeType(xdoc.Element("ToUserName").Value, toUserName.PropertyType), null);
                xdoc.Element("ToUserName").Remove();

                var fromUserName = type.GetProperty("FromUserName");
                fromUserName.SetValue(t, Convert.ChangeType(xdoc.Element("FromUserName").Value, fromUserName.PropertyType), null);
                xdoc.Element("FromUserName").Remove();

                var createTime = type.GetProperty("CreateTime");
                createTime.SetValue(t, Convert.ChangeType(xdoc.Element("CreateTime").Value, createTime.PropertyType), null);
                xdoc.Element("CreateTime").Remove();

                var msgType = type.GetProperty("MsgType");
                string msgtype = xdoc.Element("MsgType").Value.ToUpper();
                msgType.SetValue(t, (MsgType)Enum.Parse(typeof(MsgType), msgtype), null);
                xdoc.Element("MsgType").Remove();

                //判断消息类型是否是事件
                if (msgtype == "EVENT")
                {
                    //获取事件类型
                    var eventType = type.GetProperty("Event");
                    string eventtype = xdoc.Element("Event").Value.ToUpper();
                    eventType.SetValue(t, (EventType)Enum.Parse(typeof(EventType), eventtype), null);
                    xdoc.Element("Event").Remove();
                }
                #endregion


                //遍历XML节点
                foreach (XElement element in xdoc.Elements())
                {
                    //根据XML的节点名称，获取实体的属性
                    if (msgtype == "EVENT")
                    {
                        if (element.Name == "ScanCodeInfo")
                        {
                            type.GetProperty("ScanType").SetValue(t, Convert.ChangeType(element.Element("ScanType").Value, TypeCode.String), null);
                            type.GetProperty("ScanResult").SetValue(t, Convert.ChangeType(element.Element("ScanResult").Value, TypeCode.String), null);
                            continue;
                        }
                        if (element.Name == "SendPicsInfo")
                        {
                            type.GetProperty("Count").SetValue(t, Convert.ChangeType(element.Element("Count").Value, TypeCode.Int32), null);
                            List<string> picMd5List = new List<string>();
                            foreach (XElement xElement in element.Element("PicList").Elements())
                            {
                                picMd5List.Add(xElement.Element("PicMd5Sum").Value);
                            }
                            type.GetProperty("PicMd5SumList").SetValue(t, picMd5List, null);
                            continue;
                        }
                    }
                    var pr = type.GetProperty(element.Name.ToString());
                    //给属性赋值
                    pr.SetValue(t, Convert.ChangeType(element.Value, pr.PropertyType), null);
                }

                return t;
            }
            catch (Exception)
            {
                return default(T);
            }
        }
        #region 微信支付相关
        /// <summary>
        /// 获取微信的版本号 为0时，说明是、不是微信客户端
        /// 版本低于5.0用户调用微信支付功能将无效
        /// </summary>
        /// <returns></returns>
        public static decimal GetWxVersion()
        {
            var ua = HttpContext.Current.Request.UserAgent;
            Regex reg = new Regex(@"MicroMessenger/(\d+).(\d+)");
            var result = reg.Match(ua).Groups;
            var temp = result[0].Value.Split('/');
            return temp.Length == 2 ? decimal.Parse(temp[1]) : 0;
        }
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="pwd">要加密的字符串</param>
        /// <param name="encoding">字符编码方法。默认utf-8</param>
        /// <returns>加密后的密文</returns>
        public static string MD5(string pwd, string encoding = "utf-8")
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = Encoding.GetEncoding(encoding).GetBytes(pwd);
            byte[] md5data = md5.ComputeHash(data);
            md5.Clear();
            string str = string.Empty;
            for (int i = 0; i < md5data.Length; i++)
            {
                str += md5data[i].ToString("x").PadLeft(2, '0');
            }
            return str;
        }
        /// <summary>
        ///  将实体对象转换成键值对。移除值为null的属性
        /// </summary>
        /// <param name="obj">参数实体</param>
        /// <param name="index"></param>
        /// <returns>数据键值对</returns>
        public static Dictionary<string, string> EntityToDictionary(object obj, int? index = null)
        {
            var type = obj.GetType();
            var dic = new Dictionary<string, string>();
            var pis = type.GetProperties();
            foreach (var pi in pis)
            {
                //获取属性的值
                var val = pi.GetValue(obj, null);
                //移除值为null和字符串类型的值为空字符的，签名字符本身不参与签名，在验证签名正确性是需移除sign
                if (val == null || val.ToString() == "" || pi.Name == "sign" || pi.PropertyType.IsGenericType) continue;
                if (index != null)
                    dic.Add(pi.Name + "_" + index, val.ToString());
                else
                    dic.Add(pi.Name, val.ToString());
            }
            var classlist = pis.Where(p => p.PropertyType.IsGenericType);
            foreach (var info in classlist)
            {
                var val = info.GetValue(obj, null);
                int count = Convert.ToInt32(info.PropertyType.GetProperty("Count").GetValue(val, null));
                if (val != null && count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        object ol = info.PropertyType.GetMethod("get_Item").Invoke(val, new object[] { i });
                        var temp = EntityToDictionary(ol, i);
                        foreach (var t in temp)
                        {
                            dic.Add(t.Key, t.Value);
                        }
                    }
                }
            }
            return dic;
        }
        /// <summary>
        /// 获取支付签名
        /// </summary>
        /// <param name="dictionary">数据集合</param>
        /// <param name="key">支付密匙</param>
        /// <returns>签名</returns>
        public static string GetPaySign(Dictionary<string, string> dictionary, string key)
        {
            var arr = dictionary.OrderBy(d => d.Key).Select(d => string.Format("{0}={1}", d.Key, d.Value)).ToArray();
            string stringA = string.Join("&", arr) + "&key=" + key;
            return MD5(stringA);
        }
        /// <summary>
        /// 获取支付签名
        /// </summary>
        /// <param name="obj">对象实体</param>
        /// <param name="key">支付密匙</param>
        /// <returns></returns>
        public static string GetPaySign(object obj, string key)
        {
            return GetPaySign(EntityToDictionary(obj), key);
        }
        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="obj">对象实体</param>
        /// <param name="sign">签名</param>
        /// <param name="key">支付密匙</param>
        /// <returns></returns>
        public static bool ValidSign(object obj, string sign, string key)
        {
            var temp = GetPaySign(obj, key);
            return temp.ToUpper() == sign;
        }
        /// <summary>
        /// 获取参数值签名
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetParamSign(object obj)
        {
            var type = obj.GetType();
            var pis = type.GetProperties();
            var arr = pis.Select(pi => { var o = pi.GetValue(obj, null); return o == null ? "" : o.ToString(); }).ToArray();
            Array.Sort(arr);
            var temp = string.Join("", arr);
            return FormsAuthentication.HashPasswordForStoringInConfigFile(temp, "SHA1");
        }
        /// <summary>
        /// 将数据集合转换成XML
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string ParseXML(Dictionary<string, string> param)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<xml>");
            var arr = param.Select(p => string.Format(Regex.IsMatch(p.Value, @"^[0-9.]$") ? "<{0}>{1}</{0}>" : "<{0}><![CDATA[{1}]]></{0}>", p.Key, p.Value));
            sb.Append(string.Join("", arr));
            sb.Append("</xml>");
            return sb.ToString();

        }
        /// <summary>
        /// 将XML字符串转化成对象
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T XmlToEntity<T>(string xml)
        {
            var type = typeof(T);
            //创建对象
            var t = Activator.CreateInstance<T>();
            var pr = type.GetProperties();
            var xdoc = XElement.Parse(xml);
            var eles = xdoc.Elements();
            var ele = eles.Where(e => new Regex(@"_\d{1,}$").IsMatch(e.Name.ToString()));//获取带下标的节点
            if (ele.Count() > 0)
            {
                var selele = ele.Select(e =>
                {
                    var temp = e.Name.ToString().Split('_');
                    var index = int.Parse(temp[temp.Length - 1]);
                    return new { Index = index, Property = e.Name.ToString().Replace("_" + index.ToString(), ""), Value = e.Value };
                });//转换为匿名对象
                var max = selele.Max(m => m.Index);
                var infos = pr.FirstOrDefault(f => f.PropertyType.IsGenericType);//获取类型为泛型的属性
                if (infos != null)
                {
                    var infotype = infos.PropertyType.GetGenericArguments().First();//获取泛型的真实类型
                    Type listType = typeof(List<>).MakeGenericType(new[] { infotype });//创建泛型列表
                    var datalist = Activator.CreateInstance(listType);//创建对象
                    var infoprs = infotype.GetProperties();
                    for (int j = 0; j <= max; j++)
                    {
                        var temp = Activator.CreateInstance(infotype);
                        var list = selele.Where(s => s.Index == 0);
                        foreach (var v in list)
                        {
                            var p = infoprs.FirstOrDefault(f => f.Name == v.Property);
                            if (p == null) continue;
                            p.SetValue(temp, v.Value, null);
                        }
                        listType.GetMethod("Add").Invoke((object)datalist, new[] { temp });//将对象添加到列表中
                    }
                    infos.SetValue(t, datalist, null);//最后给泛型属性赋值
                }
                ele.Remove();//将有下标的节点从集合中移除
            }
            foreach (var element in eles)
            {
                var p = pr.First(f => f.Name == element.Name);
                p.SetValue(t, Convert.ChangeType(element.Value, p.PropertyType), null);
            }

            return t;
        }

        public static string GetGuid()
        {
            return Guid.NewGuid().ToString("N");
        }
        /// <summary>
        /// 获取当前请求的IP
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            string result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.UserHostAddress;
            if (string.IsNullOrEmpty(result) || !Regex.IsMatch(result, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$"))
                return "127.0.0.1";
            return result;
        }
        /// <summary>
        /// 得到当前完整主机头
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentFullHost()
        {
            HttpRequest req = HttpContext.Current.Request;
            if (!req.Url.IsDefaultPort)
            {
                return string.Format("{0}:{1}", req.Url.Host, req.Url.Port.ToString());
            }
            return req.Url.Host;
        }
        #endregion
    }
}
