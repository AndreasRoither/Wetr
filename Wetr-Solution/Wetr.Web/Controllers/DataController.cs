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
    [RoutePrefix("v1/data")]
    public class DataController : ApiController
    {

        [Route("countries")]
        [HttpGet]
        [JWT]

        /* Responses */
        [SwaggerResponse(HttpStatusCode.OK, "List of countries.", typeof(List<Country>))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Invalid credentials.", null)]

        public async Task<IHttpActionResult> GetCountries()
        {
            ICountryDao dao = AdoFactory.Instance.GetCountryDao("wetr");
            return Ok(await dao.FindAllAsync());
        }

        [Route("provinces")]
        [HttpGet]
        [JWT]

        /* Responses */
        [SwaggerResponse(HttpStatusCode.OK, "List of provinces.", typeof(List<Province>))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Invalid credentials.", null)]

        public async Task<IHttpActionResult> GetProvinces()
        {
            IProvinceDao dao = AdoFactory.Instance.GetProvinceDao("wetr");
            return Ok(await dao.FindAllAsync());
        }

        [Route("districts")]
        [HttpGet]
        [JWT]

        /* Responses */
        [SwaggerResponse(HttpStatusCode.OK, "List of districts.", typeof(List<District>))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Invalid credentials.", null)]

        public async Task<IHttpActionResult> GetDistricts()
        {
            IDistrictDao dao = AdoFactory.Instance.GetDistrictDao("wetr");
            return Ok(await dao.FindAllAsync());
        }

        [Route("communities")]
        [HttpGet]
        [JWT]

        /* Responses */
        [SwaggerResponse(HttpStatusCode.OK, "List of communities.", typeof(List<Community>))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Invalid credentials.", null)]

        public async Task<IHttpActionResult> GetCommunities()
        {
            ICommunityDao dao = AdoFactory.Instance.GetCommunityDao("wetr");
            return Ok(await dao.FindAllAsync());
        }

        

        [Route("stationtypes")]
        [HttpGet]
        [JWT]

        /* Responses */
        [SwaggerResponse(HttpStatusCode.OK, "List of stationtypes.", typeof(List<StationType>))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Invalid credentials.", null)]

        public async Task<IHttpActionResult> GetStationTypes()
        {
            IStationTypeDao dao = AdoFactory.Instance.GetStationTypeDao("wetr");
            return Ok(await dao.FindAllAsync());
        }

        [Route("measurementtypes")]
        [HttpGet]
        [JWT]

        /* Responses */
        [SwaggerResponse(HttpStatusCode.OK, "List of measurementtypes.", typeof(List<MeasurementType>))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Invalid credentials.", null)]

        public async Task<IHttpActionResult> GetMeasuremenetTypes()
        {
            IMeasurementTypeDao dao = AdoFactory.Instance.GetMeasurementTypeDao("wetr");
            return Ok(await dao.FindAllAsync());
        }
    }
}
