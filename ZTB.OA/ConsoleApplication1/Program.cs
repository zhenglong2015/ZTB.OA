using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            //MailMessage myMail = new MailMessage();                //发送端到接收端的邮箱地址  
            //myMail = new MailMessage("18612081264@163.com", "2850720872@qq.com");
            //myMail.Subject = "1";
            ////建立发送对象client,验证邮件服务器，服务器端口，用户名，以及密码  
            //SmtpClient client = new SmtpClient("123.125.50.133", 25);
            //client.Credentials = new NetworkCredential("18612081264@163.com", "zl000123");
            //myMail.Body = "adadjkwedjqiwofhnkjahduiqwfjn";
            //client.Send(myMail);
            logger.Info("分享快乐");
            Console.ReadKey();
        }
    }
}
