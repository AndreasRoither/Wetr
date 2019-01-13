using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Wetr.Web.Requests
{
    public class QueryRequest
    {
        [Range(1,6)]
        public int MeasurementTypeId { get; set; }

        [Range(0, 4)]
        public int GroupingTypeId { get; set; }

        [Range(0, 3)]
        public int ReductionTypeId { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        [Range(1, int.MaxValue)]
        public int StationId { get; set; }

    }
}