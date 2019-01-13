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
using System.Web.Http.ModelBinding;
using Wetr.Dal.Factory;
using Wetr.Dal.Interface;
using Wetr.Domain;
using Wetr.Web.BL;
using Wetr.Web.DTOs;
using Wetr.Web.Requests;
using Wetr.Web.Responses;

namespace Wetr.Web.Controllers
{
    [RoutePrefix("v1/stations")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class StationController : ApiController
    {
        [Route("")]
        [HttpPost]
        [JWT]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Invalid Authorization header.", null)]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid json format or invalid request body.", null)]
        [SwaggerResponse(HttpStatusCode.Created, "Station was created successfully.")]
        public async Task<IHttpActionResult> CreateStation(StationDTO station)
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

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            int userId = JwtHelper.Instance.GetUserId(token);

            IStationDao stationDao = AdoFactory.Instance.GetStationDao("wetr");
            IAddressDao addressDao = AdoFactory.Instance.GetAddressDao("wetr");

           

            await addressDao.InsertAsync(new Address()
            {
                CommunityId = station.CommunityId,
                Location = station.Location,
                Zip = "NO_ZIP"
            });

            int newAddressId = Convert.ToInt32((await addressDao.GetNextId()) - 1);

            /* Cleanup */
            station.AddressId = newAddressId;
            station.StationId = 0;
            station.UserId = userId;

            await stationDao.InsertAsync(station.ToStation());

            return Content(HttpStatusCode.OK, new object());
        }

        [Route("")]
        [HttpPut]
        [JWT]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Invalid Authorization header.", null)]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid json format or invalid request body.", typeof(Dictionary<string, string[]>))]
        [SwaggerResponse(HttpStatusCode.Forbidden, "You do not have permission to edit this station.", null)]
        [SwaggerResponse(HttpStatusCode.OK, "Edit request successful.", null)]
        public async Task<IHttpActionResult> EditStationAsync(StationDTO station)
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
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            int userId = JwtHelper.Instance.GetUserId(token);

            IStationDao stationDao = AdoFactory.Instance.GetStationDao("wetr");
            Station stationToEdit = await stationDao.FindByIdAsync(station.StationId);

            /* If the station doesn't belong to the user */
            if (stationToEdit.UserId != userId)
            {
                return Content(HttpStatusCode.Forbidden, new object());
            }

            /* Create new address */
            IAddressDao addressDao = AdoFactory.Instance.GetAddressDao("wetr");

            await addressDao.UpdateAsync(new Address()
            {
                AddressId = stationToEdit.AddressId,
                CommunityId = station.CommunityId,
                Location = station.Location,
                Zip = "NO_ZIP"

            });

            int newAddressId = Convert.ToInt32((await addressDao.GetNextId()) - 1);
            station.AddressId = newAddressId;


            /* Edit Station */
            await stationDao.UpdateAsync(station.ToStation());

            return Content(HttpStatusCode.OK, new object());
        }

        [Route("{stationId}")]
        [HttpDelete]
        [JWT]

        [SwaggerResponse(HttpStatusCode.Unauthorized, "Invalid Authorization header.", null)]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid json format or invalid request body.", null)]
        [SwaggerResponse(HttpStatusCode.OK, "Delete request successful.", null)]
        [SwaggerResponse(HttpStatusCode.Forbidden, "Stations to delete cannot have associated measurmenets.", null)]

        public async Task<IHttpActionResult> DeleteStation(int stationId)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            int userId = JwtHelper.Instance.GetUserId(token);

            IStationDao stationDao = AdoFactory.Instance.GetStationDao("wetr");
            IEnumerable<Station> stations = await stationDao.FindByUserIdAsync(userId);

            if (stations.Where(s => s.StationId == stationId).Count() == 0)
            {
                /* No station found so it might have been already deleted. */
                return Content(HttpStatusCode.Forbidden, new object());
            }

            IMeasurementDao measurementDao = AdoFactory.Instance.GetMeasurementDao("wetr");
            if ((await measurementDao.FindByStationIdAsync(stationId)).Count() > 0)
            {
                /* There must not be any measurmenets associated with this station. */
                return Content(HttpStatusCode.Forbidden, new object());
            }

            await stationDao.DeleteAsync(stationId);

            return Content(HttpStatusCode.OK, new object());
        }

        [Route("community/{communityId}")]
        [HttpGet]
        //[JWT]

        /* Responses */
        [SwaggerResponse(HttpStatusCode.OK, "List of all stations matching a query", typeof(List<StationDTO>))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Invalid credentials.", null)]

        public async Task<IHttpActionResult> GetStations(int communityId)
        {
          
            IStationDao stationDao = AdoFactory.Instance.GetStationDao("wetr");
            IAddressDao addressDao = AdoFactory.Instance.GetAddressDao("wetr");
            ICommunityDao communitDao = AdoFactory.Instance.GetCommunityDao("wetr");
            IDistrictDao districtDao = AdoFactory.Instance.GetDistrictDao("wetr");
            IProvinceDao provinceDao = AdoFactory.Instance.GetProvinceDao("wetr");
            ICountryDao countryDao = AdoFactory.Instance.GetCountryDao("wetr");

            IEnumerable<Station> stations = null;

            stations = await stationDao.FindAllAsync();

            List<StationDTO> convertedStations = new List<StationDTO>();

            /* Infer location ids for convenience */
            foreach (var s in stations)
            {
                StationDTO station = new StationDTO(s);

                station.CommunityId = (await addressDao.FindByIdAsync(station.AddressId)).CommunityId;
                station.DistrictId = (await communitDao.FindByIdAsync(station.CommunityId)).DistrictId;
                station.ProvinceId = (await districtDao.FindByIdAsync(station.DistrictId)).ProvinceId;
                station.CountryId = (await provinceDao.FindByIdAsync(station.ProvinceId)).CountryId;
                station.Location = (await addressDao.FindByIdAsync(station.AddressId)).Location;

                convertedStations.Add(station);
            }

            if(communityId != 0)
            {
                convertedStations.RemoveAll(s => s.CommunityId != communityId);
            }

            return Content(HttpStatusCode.OK, convertedStations);
        }

        [Route("my")]
        [HttpGet]
        [JWT]

        /* Responses */
        [SwaggerResponse(HttpStatusCode.OK, "List of my stations.", typeof(List<StationDTO>))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Invalid credentials.", null)]

        public async Task<IHttpActionResult> GetMyStations()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            int userId = JwtHelper.Instance.GetUserId(token);

            IStationDao stationDao = AdoFactory.Instance.GetStationDao("wetr");
            IAddressDao addressDao = AdoFactory.Instance.GetAddressDao("wetr");
            ICommunityDao communitDao = AdoFactory.Instance.GetCommunityDao("wetr");
            IDistrictDao districtDao = AdoFactory.Instance.GetDistrictDao("wetr");
            IProvinceDao provinceDao = AdoFactory.Instance.GetProvinceDao("wetr");
            ICountryDao countryDao = AdoFactory.Instance.GetCountryDao("wetr");

            IEnumerable<Station> myStations = null;

            myStations = await stationDao.FindByUserIdAsync(userId);

            List<StationDTO> convertedStations = new List<StationDTO>();

            /* Infer location ids for convenience */
            foreach (var s in myStations)
            {
                StationDTO station = new StationDTO(s);

                station.CommunityId = (await addressDao.FindByIdAsync(station.AddressId)).CommunityId;
                station.DistrictId = (await communitDao.FindByIdAsync(station.CommunityId)).DistrictId;
                station.ProvinceId = (await districtDao.FindByIdAsync(station.DistrictId)).ProvinceId;
                station.CountryId = (await provinceDao.FindByIdAsync(station.ProvinceId)).CountryId;
                station.Location = (await addressDao.FindByIdAsync(station.AddressId)).Location;

                convertedStations.Add(station);
            }

            return Content(HttpStatusCode.OK, convertedStations);
        }

        [Route("{id}")]
        [HttpGet]
        //[JWT]

        /* Responses */
        [SwaggerResponse(HttpStatusCode.OK, "Get a single station.", typeof(StationDTO))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Invalid credentials.", null)]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid station id.", null)]

        public async Task<IHttpActionResult> GetStation(int id)
        {
           
            IStationDao stationDao = AdoFactory.Instance.GetStationDao("wetr");
            IAddressDao addressDao = AdoFactory.Instance.GetAddressDao("wetr");
            ICommunityDao communitDao = AdoFactory.Instance.GetCommunityDao("wetr");
            IDistrictDao districtDao = AdoFactory.Instance.GetDistrictDao("wetr");
            IProvinceDao provinceDao = AdoFactory.Instance.GetProvinceDao("wetr");
            ICountryDao countryDao = AdoFactory.Instance.GetCountryDao("wetr");

            Station myStations = await stationDao.FindByIdAsync(id);
            if(myStations == null)
            {
                return Content(HttpStatusCode.BadRequest, new object());
            }

            StationDTO station = new StationDTO(myStations);

            station.CommunityId = (await addressDao.FindByIdAsync(station.AddressId)).CommunityId;
            station.DistrictId = (await communitDao.FindByIdAsync(station.CommunityId)).DistrictId;
            station.ProvinceId = (await districtDao.FindByIdAsync(station.DistrictId)).ProvinceId;
            station.CountryId = (await provinceDao.FindByIdAsync(station.ProvinceId)).CountryId;
            station.Location = (await addressDao.FindByIdAsync(station.AddressId)).Location;

            return Content(HttpStatusCode.OK, station);
        }
    }
}
