using System;
using System.Collections.Generic;
using Wetr.ApiManager;
using Wetr.Dal.Factory;
using Wetr.Dal.Interface;
using Wetr.Domain;
using Wetr.BusinessLogic;

namespace Wetr.Generator
{
    /// <summary>
    ///
    /// </summary>
    public class Generator
    {
        private IMeasurementDao measurementDao;
        private WetrApiManager wetrApiManager = new WetrApiManager();

        public Generator(IMeasurementDao measurementDao)
        {
            this.measurementDao = measurementDao;
        }

        private static Dictionary<int, int> unitmapping = new Dictionary<int, int>()
        {
            {1 , 4},
            {2 , 2},
            {3 , 4},
            {4 , 6},
            {5 , 1},
            {6 , 7},
        };

        private static Random random = new Random();

        private static double GetRandomNumber(double minimum, double maximum)
        {
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        private static double GetLinearAscNumber(Preset p)
        {
            double total = (p.EndDate - p.StartDate).TotalSeconds;
            double x = (p.CurrentDate - p.StartDate).TotalSeconds;
            double k = (p.MaxValue - p.MinValue) / total;

            return k * x + p.MinValue;
        }

        private static double GetLinearDescNumber(Preset p)
        {
            double total = (p.EndDate - p.StartDate).TotalSeconds;
            double x = Math.Abs((p.CurrentDate - p.EndDate).TotalSeconds);
            double k = (p.MaxValue - p.MinValue) / total;

            return k * x + p.MinValue;
        }

        public static double Map(int value, float fromSource, float toSource, float fromTarget, float toTarget)
        {
            return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
        }

        private  double GetRealisticTempNumber(Preset p)
        {
            int hour = p.CurrentDate.Hour;

            if (hour < 12)
                return Map(hour, 0, 11, (float)p.MinValue, (float)p.MaxValue);

            return Map(hour, 12, 23, (float)p.MaxValue, (float)p.MinValue);
        }

        public void Generate(IEnumerable<Preset> presets, Frequency frequency)
        {
            foreach (Preset p in presets)
            {
                if (p.Frequency == frequency)
                {
                    /* If this is the first meassurement */
                    if (p.CurrentDate == DateTime.MinValue)
                        p.CurrentDate = p.StartDate;

                    /* If the timespan is surpassed */
                    if (p.CurrentDate > p.EndDate)
                        continue;

                    foreach (Station s in p.Stations)
                    {
                        double value = 0;
                        switch (p.Distribution)
                        {
                            case Distribution.Random:
                                value = GetRandomNumber(p.MinValue, p.MaxValue);
                                break;

                            case Distribution.LinearAsc:
                                value = GetLinearAscNumber(p);
                                break;

                            case Distribution.LinearDesc:
                                value = GetLinearDescNumber(p);
                                break;

                            case Distribution.RealisticTemp:
                                value = GetRealisticTempNumber(p);
                                break;
                        }

                        Measurement m = new Measurement()
                        {
                            StationId = s.StationId,
                            MeasurementTypeId = p.MeasurementType.MeasurementTypeId,
                            TimesStamp = p.CurrentDate,
                            Value = value,
                            UnitId = unitmapping[p.MeasurementType.MeasurementTypeId],
                        };

                        if (!p.GeneratedData.ContainsKey(s))
                            p.GeneratedData.Add(s, new List<Measurement>());

                        p.GeneratedData[s].Add(m);

                        // old version with dao
                        // bool? res = measurementDao?.InsertAsync(m).Result;
                        wetrApiManager.PostMeasurement(m);

                        Console.WriteLine(m);
                    }

                    switch (frequency)
                    {
                        case Frequency.Second:
                            p.CurrentDate = p.CurrentDate.AddSeconds(1);

                            break;

                        case Frequency.Minute:
                            p.CurrentDate = p.CurrentDate.AddMinutes(1);

                            break;

                        case Frequency.Hour:
                            p.CurrentDate = p.CurrentDate.AddHours(1);

                            break;

                        case Frequency.Day:
                            p.CurrentDate = p.CurrentDate.AddDays(1);

                            break;

                        case Frequency.Week:
                            p.CurrentDate = p.CurrentDate.AddDays(7);

                            break;
                    }
                }
            }
        }
    }
}