using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wetr.Domain;

namespace Wetr.Web.DTOs
{
    public class StationDTO : Station
    {

        public int CommunityId { get; set; }
        public int DistrictId { get; set; }
        public int ProvinceId { get; set; }
        public int CountryId { get; set; }

        public StationDTO() : base()
        {
        }

        public StationDTO(Station station)
        {
            this.AddressId = station.AddressId;
            this.Latitude = station.Latitude;
            this.Longitude = station.Longitude;
            this.StationId = station.StationId;
            this.StationTypeId = station.StationTypeId;
            this.UserId = station.UserId;
            this.Name = station.Name;
        }
    }
}