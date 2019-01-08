using Swashbuckle.Examples;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Wetr.Dal.Factory;
using Wetr.Dal.Interface;
using Wetr.Domain;
using Wetr.Web.BL;
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
        [SwaggerResponse(HttpStatusCode.OK, "Successful login.", typeof(TokenResponse))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(TokenResponseExample))]

        [SwaggerResponse(HttpStatusCode.Unauthorized, "Invalid credentials.", null)]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid json format or invalid request body.", typeof(Dictionary<string, string[]>))]

        public async Task<IHttpActionResult> Login(LoginRequest request)
        {
            /* Check if model is valid */
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, ModelState.ToDictionary(
                                                             kvp => kvp.Key,
                                                             kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                               ));
            }

            /* Validate credentials agains database */
            IUserDao userDao = AdoFactory.Instance.GetUserDao("wetr");
            User user = await userDao.FindByEmailAsync(request.Email);

            /* If the user is not found or the password is invalid */
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return Content(HttpStatusCode.Unauthorized, new object());
            }

            return Ok(JwtHelper.Instance.Generate(user.UserId));

        }

        #endregion

    }
}
