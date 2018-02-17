using aymk_database.Database;
using aymk_database.Repository.Base;
using aymk_database.Repository.Interface;
using aymk_library.Library.Models;
using aymk_library.Library.Util;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace aymk_database.Repository
{
    public class GlobalBL
    {

        public aymkResponse GetCatalogs()
        {
            try
            {
                 
                aymkResponse excList = new CatalogExchangeBL().GetList();
                aymkResponse marketList = new CatalogMarketBL().GetList(null, new List<string>{ "CatalogExchange" });
                var data = new { catalogExchanges = excList.Data, catalogMarkets = marketList.Data };

                return new aymkResponse(data);
            }
            catch (System.Exception ex)
            {
                return new aymkResponse(aymkError.GeneralError, "Global BL", ex);
            }
        }
    }
}
