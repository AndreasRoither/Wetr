using Swashbuckle.Examples;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Wetr.Web.Requests;
using Wetr.Web.Responses;

namespace Wetr.Web.Controllers
{
    [RoutePrefix("v1/auth")]
    public class AuthController : ApiController
    {

        #region Login

        [Route("")]
        [HttpPost]

        /* Requests */
        [SwaggerRequestExample(typeof(LoginRequest), typeof(LoginRequestExample))]

        /* Responses */
        [SwaggerResponse(HttpStatusCode.OK, "Successful login.", typeof(LoginResponse))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(LoginResponseExample))]

        [SwaggerResponse(HttpStatusCode.Unauthorized, "Invalid credentials.", null)]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid json format or invalid request body.", null)]

        public IHttpActionResult Login(LoginRequest request)
        {
            /* Check if model is valid */
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, new object());
            }

            return Ok(new LoginResponse()
            {
                Token = "foeuwihgfi8ophewopug"
            });
        }

        #endregion

    }
}
