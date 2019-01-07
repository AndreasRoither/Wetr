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
    [RoutePrefix("v1/stations")]
    public class StationController : ApiController
    {


        [Route("")]
        [HttpGet]
        [JWT]
       

        /* Responses */
        [SwaggerResponse(HttpStatusCode.OK, "Successful login.", typeof(List<Station>))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Invalid credentials.", null)]

        public async Task<IHttpActionResult> GetMyStations()
        {
            return Content(HttpStatusCode.OK, new List<Station>()
            {
                new Station(){},
                new Station(){},
                new Station(){},

            });


        }


    }
}
