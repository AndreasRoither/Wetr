using Swashbuckle.Examples;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Wetr.Dal.Factory;
using Wetr.Dal.Interface;
using Wetr.Domain;
using Wetr.Web.BL;
using Wetr.Web.DTOs;
using Wetr.Web.Requests;

namespace Wetr.Web.Controllers
{
    [RoutePrefix("v1/measurements")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MeasurementController : ApiController
    {

        [Route("")]
        [HttpPost]
        [JWT]

        [SwaggerResponse(HttpStatusCode.Unauthorized, "Invalid Authorization header.", null)]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid json format or invalid request body.", typeof(Dictionary<string, string[]>))]
        [SwaggerResponse(HttpStatusCode.OK, "Measurement was posted successfully.", null)]

        public async Task<IHttpActionResult> PostMeasurement(MeasurementDTO measurement)
        {
            /* Check if model is valid */
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                                                             kvp => kvp.Key,
                                                             kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                               );
                return Content(HttpStatusCode.BadRequest, errors);
            }

            IMeasurementDao dao = AdoFactory.Instance.GetMeasurementDao("wetr");
            await dao.InsertAsync(measurement.ToMeasurement());

            return Content(HttpStatusCode.OK, new object());
        }

        [Route("query")]
        [HttpPost]
        //[JWT]

        [SwaggerResponse(HttpStatusCode.Unauthorized, "Invalid Authorization header.", null)]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid json format or invalid request body.", typeof(Dictionary<string, string[]>))]
        [SwaggerResponse(HttpStatusCode.OK, "Measurement data of query.", typeof(double[]))]

        public async Task<IHttpActionResult> QueryMeasurements(QueryRequest query)
        {
            /* Check if model is valid */
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                                                             kvp => kvp.Key,
                                                             kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                               );
                return Content(HttpStatusCode.BadRequest, errors);
            }

            IMeasurementDao dao = AdoFactory.Instance.GetMeasurementDao("wetr");
            IStationDao stationDao = AdoFactory.Instance.GetStationDao("wetr");
            Station s = await stationDao.FindByIdAsync(query.StationId);
            double[] result = await dao.GetQueryResult(query.Start, query.End, query.MeasurementTypeId, query.ReductionTypeId, query.GroupingTypeId,
                new List<Station>() { s });

            return Content(HttpStatusCode.OK, result);
        }

    }
}
