// ==========================================
// Author                  :    WIN-JH13BJM99UM 
// Create Time           :    2016/7/12 11:10:36
// Update Time          :    2016/7/12 11:10:36
// ==========================================
// Class Description     :    
// ==========================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security.OAuth;

namespace WebApiWithOath.Oath
{
    /// <summary>
    /// OAuth验证客户端Id
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public class OwinAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            await base.ValidateClientAuthentication(context);
        }
        /// <summary>
        /// OAuth验证用户名和密码
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            if (string.IsNullOrEmpty(context.UserName) || string.IsNullOrEmpty(context.Password))
            {
                context.SetError("invalid_grant", "非法请求！");
                return;
            }
            //查询数据库
            //  Operator oper = Operation.OperatorOperation.GetSingle(context.UserName, context.Password);

            //if (oper == null)
            //{
            //    context.SetError("invalid_grant", "非法用户！");
            //    return;
            //}

            //if (oper.Used == 0)
            //{
            //    context.SetError("invalid_grant", "用户已禁用！");
            //    return;
            //}
            //var identity = new ClaimsIdentity(new GenericIdentity(oper.OperCode, OAuthDefaults.AuthenticationType));//返回用户名
           // context.Validated(identity);
        }
    }
}