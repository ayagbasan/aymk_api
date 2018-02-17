using aymk_api.OAuth.Providers;
using aymk_database.Database;
using aymk_database.Repository;
using aymk_database.Repository.Interface;
using aymk_library.Library.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace aymk_api.Controllers
{
    [aymkAuth]
    public class AccountController : ApiController
    {
        IAccountBL bl;
        List<string> includeTables = new List<string>();
        public AccountController()
        {
            bl = new AccountBL();
            includeTables.Add("Notification");
            includeTables.Add("Alert.CatalogMarket.CatalogExchange");
            includeTables.Add("Exchange.CatalogExchange");
        }

        [HttpPost]        
        public aymkResponse Post(string method, Account item)
        {
            if (method.ToLower() == "getById")
                return bl.Get(p => p.id == item.id);
            else if (method.ToLower() == "getAllData")
                return bl.Get(p => p.id == item.id, includeTables);
            else if (method.ToLower() == "update")
                return bl.Update(item);
            else if (method.ToLower() == "changestatus")
                return bl.Update(item);
            else
                return new aymkResponse(false, "Unknown method type: " + method);
        }
    }
}