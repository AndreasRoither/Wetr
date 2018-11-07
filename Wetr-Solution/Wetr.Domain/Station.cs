using System;

namespace Wetr.Domain
{
    public class Station
    {
        public int StationId { get; set; }
        public int StationTypeId { get; set; }
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

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
    }
}