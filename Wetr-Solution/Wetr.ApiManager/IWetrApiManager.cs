using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Wetr.Domain;

namespace Wetr.ApiManager
{
    public interface IWetrApiManager
    {
        Task<IEnumerable<Station>> GetStations();
        Task<HttpStatusCode> PostMeasurement(Measurement measurement);
    }
}
