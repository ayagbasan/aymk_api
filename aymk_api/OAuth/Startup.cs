using aymk_api.OAuth.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http; 

[assembly: OwinStartup(typeof(aymk_api.OAuth.Startup))]
namespace aymk_api.OAuth
{
    public class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration httpConfiguration = new HttpConfiguration();

            ConfigureOAuth(appBuilder);

            WebApiConfig.Register(httpConfiguration);
            appBuilder.UseWebApi(httpConfiguration);
            appBuilder.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            httpConfiguration.SuppressDefaultHostAuthentication();
            httpConfiguration.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

        }

        private void ConfigureOAuth(IAppBuilder appBuilder)
        {
            //OAuthAuthorizationServerOptions oAuthAuthorizationServerOptions = new OAuthAuthorizationServerOptions()
            //{
            //    TokenEndpointPath = new Microsoft.Owin.PathString("/api/token"), 
            //    AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
            //    AllowInsecureHttp = true,                
            //    Provider = new TokenProvider()
            //};

            //appBuilder.UseOAuthAuthorizationServer(oAuthAuthorizationServerOptions);
            //appBuilder.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());


            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/api/login"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(60000),
                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true
            };

            // Enable the application to use bearer tokens to authenticate users
            appBuilder.UseOAuthAuthorizationServer(OAuthOptions);
            appBuilder.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}