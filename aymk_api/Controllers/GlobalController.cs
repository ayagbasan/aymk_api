using aymk_api.OAuth;
using aymk_api.OAuth.Providers;
using aymk_database.Database;
using aymk_database.Repository;
using aymk_database.Repository.Interface;
using aymk_library.Library.Models;
using aymk_library.Library.Util;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Web.Http;

namespace aymk_api.Controllers
{
    public class GlobalController : ApiController
    {
        IAccountBL bl;
        public GlobalController()
        {
            bl = new AccountBL();
        }

        [HttpPost]        
        public aymkResponse Post(string method, Account item)
        {
            method = method.ToLower().Trim();

            if (method == aymkMethodType.Register.GetDescription())
                return bl.Register(item);
            else if (method == aymkMethodType.ForgotPassword.GetDescription())
                return bl.Get(p => p.id == item.id);
            else if (method == aymkMethodType.Catalogs.GetDescription())
                return new GlobalBL().GetCatalogs();
            else
                return new aymkResponse(false, "Unknown method type: " + method);
        }
    }
}