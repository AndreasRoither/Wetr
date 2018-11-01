namespace Wetr.Domain
{
    public class StationType
    {
        public int StationTypeId { get; set; }
        public string Name { get; set; }

        public override string ToString() =>
            $"[{StationTypeId}] {Name}";
    }
}