
using aymk_library.Library.Models;
using aymk_library.Library.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace aymk_api.OAuth.Providers
{
    public class aymkAuth : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            JToken json = JObject.FromObject(new aymkResponse(aymkError.GetError, "yetkisiz"));
            actionContext.Response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Forbidden,
                Content = new JsonContent(json)
            };
        }
    }
}