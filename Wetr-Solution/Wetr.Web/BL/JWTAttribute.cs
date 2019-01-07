using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Wetr.Web.BL
{
    public class JWTAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            actionContext.Request.Headers.TryGetValues("Authorization", out IEnumerable<string> values);
            if(values != null)
            {
                string token = values.FirstOrDefault();
                return JwtHelper.Instance.IsValid(token);

            }
            return false;
        }
    }
}