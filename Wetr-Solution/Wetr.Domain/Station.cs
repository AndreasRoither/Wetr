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
    }
}