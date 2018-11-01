using System;

namespace Wetr.Domain
{
    public class Measurement
    {
        public int MeasurementId { get; set; }
        public int StationId { get; set; }
        public int MeasurementTypeId { get; set; }
        public int UnitId { get; set; }
        public double Value { get; set; }
        public DateTime TimesStamp { get; set; }

        public override string ToString() =>
            $"[{MeasurementId}] {StationId} {MeasurementTypeId} {UnitId} {Value} {TimesStamp}";
    }
}