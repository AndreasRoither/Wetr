using System;
using System.Collections.Generic;
using Wetr.Domain;

namespace Wetr.Cockpit.Wpf.Model
{

    public enum Distribution
    {
        Linear, Random, Cubic
    }

    public enum Frequency
    {
        Second, Minute, Hour, Day, Week
    }

    public class Preset
    {
        private static int NextId = 0;
        public static int NextInt()
        {
            return NextId++;
        }

        public int Id { get; set; }
        public Distribution Distribution { get; set; }
        public Frequency Frequency { get; set; }

        public string Name { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }

        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public MeasurementType MeasurementType { get; set; }

        public List<Station> Stations { get; set; } = new List<Station>();

    }
}