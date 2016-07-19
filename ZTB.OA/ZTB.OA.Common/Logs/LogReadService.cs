// ==========================================
// Author                  :    WIN-JH13BJM99UM 
// Create Time           :    2016/7/8 10:43:44
// Update Time          :    2016/7/8 10:43:44
// ==========================================
// Class Description     :    
// ==========================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ZTB.OA.Common.Logs
{
    public static class LogReadService
    {
        public static GetDirFilesResponse GetDirFiles(string dirPath, string filter)
        {
            GetDirFilesResponse response = new GetDirFilesResponse();
            if (Directory.Exists(dirPath))
            {
                Task task = Task.Run(() =>
                {
                    string[] files = Directory.GetFiles(dirPath, filter);
                    response.Model = new List<LogFile>();
                    if (files.Length > 0)
                    {
                        foreach (string fullName in files)
                        {
                            FileInfo inf = new FileInfo(fullName);
                            float value = inf.Length / 1024;
                            response.Model.Add(new LogFile
                            {
                                fullName = fullName,
                                fileName = inf.Name,
                                fileSize = value + "KB",
                                LastWrite = inf.LastWriteTime,
                                lastWriteTime = inf.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss")
                            });
                        }
                    }
                    response.Model = response.Model.OrderByDescending(m => m.LastWrite).ToList();
                    response.Success = true;
                });
                Task.WaitAll(task);
            }
            else
            {
                response.Success = false;
                response.Message = "找不到该目录";
            }
            return response;
        }


        public static dynamic ReadContent(string path)
        {
            dynamic dy = new { Success = false, Message = "文件不存在" };

            path = HttpUtility.UrlDecode(path);
            if (File.Exists(path))
            {
                StringBuilder sb = new StringBuilder();
                using (StreamReader sr = new StreamReader(path, Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        sb.Append(line.ToString() + "\r\n");
                    }
                }

                dy = new { Success = true, Message = "读取成功！", Content = sb.ToString() };
            }
            return  dy;
        }

        public static dynamic Delete(string path)
        {
            dynamic dy = new { Success = false, Message = "文件不存在" };
            path = HttpUtility.UrlDecode(path);
            if (File.Exists(path))
            {
                FileInfo file = new FileInfo(path);
                if (file.IsReadOnly)
                {
                    dy = new { Success = false, Message = "文件只读不可删除！" };
                }
                else
                {
                    file.Delete();
                    dy = new { Success = true, Message = "删除成功！" };
                }
            }

            return  dy;
        }

    }


    public class GetDirFilesResponse
    {
        public GetDirFilesResponse()
        {
            this.Success = false;
            this.Message = string.Empty;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public IList<LogFile> Model { get; set; }
    }

    public class LogFile
    {
        public string fullName { get; set; }
        public string fileName { get; set; }
        public string fileSize { get; set; }
        public string lastWriteTime { get; set; }
        public DateTime LastWrite { get; set; }
    }
}
