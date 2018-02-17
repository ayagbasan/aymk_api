using aymk_database.Repository;
using aymk_database.Repository.Interface;
using aymk_library.Library.Models;
using Microsoft.Owin.Security.OAuth;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace aymk_api.OAuth.Providers
{
    public class TokenProvider : OAuthAuthorizationServerProvider
    {
        // OAuthAuthorizationServerProvider sınıfının client erişimine izin verebilmek için ilgili ValidateClientAuthentication metotunu override ediyoruz.
        public override async System.Threading.Tasks.Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();

        }

        // OAuthAuthorizationServerProvider sınıfının kaynak erişimine izin verebilmek için ilgili GrantResourceOwnerCredentials metotunu override ediyoruz.
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

             

            IAccountBL accountBL = new AccountBL();
            aymkResponse response=  accountBL.Login(context.UserName, context.Password);

            if (response.IsSuccess)
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("sub", context.UserName));
                identity.AddClaim(new Claim("role", "user"));
                context.Validated(identity);

            }
            else
            {
                context.SetError(response.Message, response.Detail);
            }


        }



    }
}