using aymk_database.Database;
using aymk_database.Repository;
using aymk_database.Repository.Interface;
using aymk_library.Library.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace aymk_api.Controllers
{
    public class AlertController : ApiController
    {
        IAlertBL bl;
        List<string> incTables = new List<string>();
        public AlertController()
        {
            bl = new AlertBL();
            incTables.Add("CatalogMarket.CatalogExchange");
        }

        [HttpPost]
        public aymkResponse Post(string method, Alert item)
        {
            if (method.ToLower() == "add")
                return bl.Add(item);
            else if (method.ToLower() == "get")
                return bl.Get(p => p.id == item.id, incTables);
            else if (method.ToLower() == "getlist")           
                return bl.GetList(p => p.accountId == item.accountId && p.active==item.active, incTables);
            else if (method.ToLower() == "update")
                return bl.Update(item);
            else if (method.ToLower() == "delete")
                return bl.Delete(item);
            else
                return new aymkResponse(false, "Unknown method type: " + method);
        } 



    }
}