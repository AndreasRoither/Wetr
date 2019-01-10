using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Wetr.Domain;

namespace Wetr.Web.DTOs
{
    public class MeasurementDTO
    {
        [Range(1,int.MaxValue)]
        public int StationId { get; set; }

        [Range(double.MinValue, double.MaxValue)]
        public double Value { get; set; }

        [Range(1, int.MaxValue)]
        public int MeasurementTypeId { get; set; }

        [Range(0, int.MaxValue)]
        public int MeasurementId { get; set; }

        [Range(1, int.MaxValue)]
        public int UnitId { get; set; }

        [Required]
        public DateTime TimesStamp { get; set; }

        public Measurement ToMeasurement()
        {
            return new Measurement()
            {
                MeasurementId = 0,
                MeasurementTypeId = this.MeasurementTypeId,
                StationId = this.StationId,
                TimesStamp = this.TimesStamp,
                UnitId = this.UnitId,
                Value = this.Value
            };
        }

        public void SetMeasurementDTO(Measurement m)
        {
            this.MeasurementId = m.MeasurementId;
            this.MeasurementTypeId = m.MeasurementTypeId;
            this.StationId = m.StationId;
            this.TimesStamp = m.TimesStamp;
            this.UnitId = m.UnitId;
            this.Value = m.Value;
        }
    }
}