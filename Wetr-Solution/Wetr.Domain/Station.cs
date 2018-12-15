using System;
using System.Collections.Generic;

namespace Wetr.Domain
{
    public class Station
    {
        public int StationId { get; set; }
        public int StationTypeId { get; set; }
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }

        public override string ToString() =>
            $"[{StationId}] {StationTypeId} {AddressId} {UserId} {Name} {Longitude} {Latitude}";

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Station temp = (Station)obj;
                return ((this.StationId == temp.StationId) && (this.Name == temp.Name) && (this.StationTypeId == temp.StationTypeId)
                    && (this.AddressId == temp.AddressId) && (this.UserId == temp.UserId)
                    && (this.Longitude == temp.Longitude) && (this.Latitude == temp.Latitude));
            }
        }

        public override int GetHashCode()
        {
            var hashCode = -1136939070;
            hashCode = hashCode * -1521134295 + StationId.GetHashCode();
            hashCode = hashCode * -1521134295 + StationTypeId.GetHashCode();
            hashCode = hashCode * -1521134295 + AddressId.GetHashCode();
            hashCode = hashCode * -1521134295 + UserId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Longitude.GetHashCode();
            hashCode = hashCode * -1521134295 + Latitude.GetHashCode();
            return hashCode;
        }
    }
}