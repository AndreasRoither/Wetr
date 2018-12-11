using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Wetr.Domain;

namespace Wetr.Simulator.Wpf.Model
{
    /// <summary>
    /// Enum: Types of distribution
    /// </summary>
    public enum Distribution
    {
        Linear, Random, Cubic
    }

    /// <summary>
    /// Enum: Types of frequencies
    /// </summary>
    public enum Frequency
    {
        Second, Minute, Hour, Day, Week
    }

    /// <summary>
    /// Class that holds Informations about a preset for a station
    /// </summary>
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
        public DateTime CurrentDate { get; set; }

        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public MeasurementType MeasurementType { get; set; }

        public ObservableCollection<Station> Stations { get; set; } = new ObservableCollection<Station>();

        public Dictionary<Station, List<Measurement>> GeneratedData { get; set; }

    }
}