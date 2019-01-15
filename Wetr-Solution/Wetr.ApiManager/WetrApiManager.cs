using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Wetr.Domain;
using Wetr.Web.DTOs;

namespace Wetr.ApiManager
{
    /// <summary>
    /// Wetr Api Manager for Wetr.Simulator.Wpf
    /// </summary>
    /// <see cref="Wetr.Simulator.Wpf"/>
    public class WetrApiManager : IDisposable, IWetrApiManager
    {
        static HttpClient client = new HttpClient();

        private static readonly string apiConnectionString = "http://localhost:5000/v1/";
        private static readonly string apiToken = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJpZCAiOjEsImV4cCI6IjA3LjAxLjIwMjkgMTk6MjE6MjcifQ.VjgJkguMQhZPjXfmL7BtsDJsrJtC1nbX9H74yiF6V8Q";

        public WetrApiManager()
        {
            client.BaseAddress = new Uri(apiConnectionString);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(apiToken);
        }

        /// <summary>
        /// Get all wetr stations
        /// </summary>
        /// <returns>IEnumerable<Station> or null if failed or no stations</returns>
        public async Task<IEnumerable<Station>> GetStations()
        {
            IEnumerable<Station> stations = null;
            HttpResponseMessage response = await client.GetAsync("stations/community/0");
             
            if (response.IsSuccessStatusCode)
            {
                // .ReadAsAsync is an Extension method found in the WebApi.Client nugget package
                // https://www.nuget.org/packages/Microsoft.AspNet.WebApi.Client/
                stations = await response.Content.ReadAsAsync<IEnumerable<Station>>();
            }

            return stations;
        }

        /// <summary>
        /// Post Measurement
        /// </summary>
        /// <param name="measurement">measurement that should be posted to the web api</param>
        /// <returns>HttpStatusCode</returns>
        public async Task<HttpStatusCode> PostMeasurement(Measurement measurement)
        {
            MeasurementDTO m = new MeasurementDTO();
            m.SetMeasurementDTO(measurement);

            HttpResponseMessage response = await client.PostAsJsonAsync("measurements", m);

            // will throw an exception if not successful
            response.EnsureSuccessStatusCode();

            return response.StatusCode;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                client.Dispose();
            }
            // free native resources if there are any.
        }
    }
}
