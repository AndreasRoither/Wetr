namespace Wetr.Domain
{
    public class MeasurementType
    {
        public int MeasurementTypeId { get; set; }
        public string Name { get; set; }

        public override string ToString() =>
            $"[{MeasurementTypeId}] {Name}";
    }
}