using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Wetr.Domain;

namespace Wetr.Web.DTOs
{
    public class StationDTO
    {

        [Range(1, int.MaxValue, ErrorMessage = "Der Wert muss größer als 0 sein.")]
        public int CommunityId { get; set; } = 1;

        [Range(1, int.MaxValue, ErrorMessage = "Der Wert muss größer als 0 sein.")]
        public int DistrictId { get; set; } = 1;

        [Range(1, int.MaxValue, ErrorMessage = "Der Wert muss größer als 0 sein.")]
        public int ProvinceId { get; set; } = 1;

        [Range(1, int.MaxValue, ErrorMessage = "Der Wert muss größer als 0 sein.")]
        public int CountryId { get; set; } = 1;

        [Range(1, int.MaxValue, ErrorMessage = "Der Wert muss größer als 0 sein.")]
        public int StationId { get; set; } = 1;

        [Range(1, int.MaxValue, ErrorMessage = "Der Wert muss größer als 0 sein.")]
        public int UserId { get; set; } = 1;

        [Range(1, int.MaxValue, ErrorMessage = "Der Wert muss größer als 0 sein.")]
        public int AddressId { get; set; } = 1;

        [Range(1, int.MaxValue, ErrorMessage = "Der Wert muss größer als 0 sein.")]
        public int StationTypeId { get; set; } = 1;

        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        [Range(-90.0,90.0, ErrorMessage = "Kein valider Wert für eine Latitude.")]
        public decimal Latitude { get; set; }

        [Range(-180.0, 180.0, ErrorMessage = "Kein valider Wert für eine Longitude.")]
        public decimal Longitude { get; set; }

        public StationDTO()
        {
        }

        public Station ToStation()
        {
            return new Station()
            {
                StationId = StationId,
                AddressId = AddressId,
                Latitude = Latitude,
                Longitude = Longitude,
                Name = Name,
                StationTypeId = StationTypeId,
                UserId = UserId
            };
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