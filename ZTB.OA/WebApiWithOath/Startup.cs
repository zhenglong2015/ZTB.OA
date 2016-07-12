// ==========================================
// Author                  :    WIN-JH13BJM99UM 
// Create Time           :    2016/7/12 11:05:10
// Update Time          :    2016/7/12 11:05:10
// ==========================================
// Class Description     :    
// ==========================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using WebApiWithOath;
using WebApiWithOath.Oath;

[assembly: OwinStartup(typeof(Startup))]
namespace WebApiWithOath
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            ConfigreOAuth(app);//注册Oauth认证
            WebApiConfig.Register(config);
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
            GlobalConfiguration.Configure(WebApiConfig.Register);//注册Helppage
            AreaRegistration.RegisterAllAreas();
        }
        public void ConfigreOAuth(IAppBuilder app)
        {
            var oauthServerOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new OwinAuthorizationServerProvider()
            };

            app.UseOAuthAuthorizationServer(oauthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}