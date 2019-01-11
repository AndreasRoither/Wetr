using System;
using System.Globalization;
using System.IO;
using System.Threading;

namespace Wetr.Generator
{
    internal class Generator
    {
        private int dataCount = 0;

        public enum Season
        {
            SUMMER, WINTER, FALL, SPRING, UKN
        }

        /* Min and May Temp from https://www.klimatabelle.info/europa/oesterreich */

        private Tuple<float, float> GetTempRange(Season season)
        {
            switch (season)
            {
                case Season.SPRING: return Tuple.Create(8.23f, 15f);
                case Season.SUMMER: return Tuple.Create(13.2f, 23.76f);
                case Season.FALL: return Tuple.Create(5.23f, 14.1f);
                case Season.WINTER: return Tuple.Create(-3.1f, 4.6f);
                default: return Tuple.Create(0f, 0f);
            }
        }

        /* Min and May Temp from https://www.klimatabelle.info/europa/oesterreich */

        private int GetAvgDownfall(Season season)
        {
            switch (season)
            {
                case Season.SPRING: return 308;
                case Season.SUMMER: return 476;
                case Season.FALL: return 260;
                case Season.WINTER: return 170;
                default: return 0;
            }
        }

        /* Min and May Temp from https://www.klimatabelle.info/europa/oesterreich */

        private float GetAvgHumidity(Season season)
        {
            switch (season)
            {
                case Season.SPRING: return 84.3f;
                case Season.SUMMER: return 83.3f;
                case Season.FALL: return 85.3f;
                case Season.WINTER: return 91.3f;
                default: return 0f;
            }
        }

        private Season GetSeason(DateTime date)
        {
            switch (date.Month)
            {
                case 1: return Season.WINTER;
                case 2: return Season.WINTER;
                case 3: return Season.SPRING;
                case 4: return Season.SPRING;
                case 5: return Season.SPRING;
                case 6: return Season.SUMMER;
                case 7: return Season.SUMMER;
                case 8: return Season.SUMMER;
                case 9: return Season.FALL;
                case 10: return Season.FALL;
                case 11: return Season.FALL;
                case 12: return Season.WINTER;
                default: return Season.UKN;
            }
        }

        /* https://stackoverflow.com/questions/7833030/c-sharp-store-datetime-to-timestamp-column-in-mysql */

        public string getTimesamp(DateTime dateTime) => dateTime.ToString("yyyy-MM-dd HH:mm:ss");

        /* https://stackoverflow.com/questions/14353485/how-do-i-map-numbers-in-c-sharp-like-with-map-in-arduino */

        public float Map(int value, float fromSource, float toSource, float fromTarget, float toTarget)
        {
            return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
        }

        /* https://stackoverflow.com/questions/1064901/random-number-between-2-double-numbers */
        private static Random random = new Random();

        public double GetRandomNumber(double minimum, double maximum)
        {
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        public float GetTemperature(Season s, int hour)
        {
            Tuple<float, float> range = GetTempRange(s);
            if (hour < 12)
                return Map(hour, 0, 11, range.Item1, range.Item2);
            return Map(hour, 12, 23, range.Item2, range.Item1);
        }

        private void Generate()
        {
            /* https://stackoverflow.com/questions/3135569/how-to-change-symbol-for-decimal-point-in-double-tostring */
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");

            // Temperatur Luftdruck Feuchtigkeit Niederschlag Windstärke Windrichtung
            this.GenerateTemperaturData();
            this.GenerateHumidityData();
            this.GenerateDownfallData();
            this.GeneratePreassureData();
            this.GenerateWindData();
            this.GenerateWindDirectionData();

            Console.WriteLine($"Generated {dataCount} data entries!");
            Console.ReadLine();

        }

        private void GenerateDownfallData()
        {
            /* Generate Downfall Data */
            using (StreamWriter file = new StreamWriter("measurementsDownfall.bulk"))
            {
                DateTime beginning = new DateTime(2018, 1, 1, 0, 0, 0);
                DateTime ending = new DateTime(2019, 12, 31, 23, 59, 59);

                /* For every station*/
                for (int stationId = 1; stationId <= 70; stationId++)
                {
                    /* For every hour in one year */
                    for (DateTime t = beginning; t <= ending; t = t.AddDays(1d))
                    {
                        file.WriteLine($"'NULL', '{GetRandomNumber(0, (GetAvgDownfall(GetSeason(t))/365d)*2)}', '{getTimesamp(t)}', '{stationId}', '3', '3'");
                        dataCount++;
                    }
                }
            }
        }

        private void GeneratePreassureData()
        {
            /* Generate Preassure Data */
            using (StreamWriter file = new StreamWriter("measurementsPreassure.bulk"))
            {
                DateTime beginning = new DateTime(2018, 1, 1, 0, 0, 0);
                DateTime ending = new DateTime(2019, 12, 31, 23, 59, 59);

                /* For every station*/
                for (int stationId = 20; stationId <= 50; stationId++)
                {
                    /* For every hour in one year */
                    for (DateTime t = beginning; t <= ending; t = t.AddHours(1d))
                    {
                        file.WriteLine($"'NULL', '{GetRandomNumber(900, 1100)}', '{getTimesamp(t)}', '{stationId}', '2', '2'");
                        dataCount++;

                    }
                }
            }
        }

        private void GenerateWindData()
        {
            /* Generate Wind Data */
            using (StreamWriter file = new StreamWriter("measurementsWind.bulk"))
            {
                DateTime beginning = new DateTime(2018, 1, 1, 0, 0, 0);
                DateTime ending = new DateTime(2019, 12, 31, 23, 59, 59);

                /* For every station*/
                for (int stationId = 10; stationId <= 30; stationId++)
                {
                    /* For every hour in one year */
                    for (DateTime t = beginning; t <= ending; t = t.AddHours(1d))
                    {
                        file.WriteLine($"'NULL', '{GetRandomNumber(0, 20)}', '{getTimesamp(t)}', '{stationId}', '1', '5'");
                        dataCount++;

                    }
                }
            }
        }

        private void GenerateWindDirectionData()
        {
            /* Generate Wind Direction Data */
            using (StreamWriter file = new StreamWriter("measurementsWindDirection.bulk"))
            {
                DateTime beginning = new DateTime(2018, 1, 1, 0, 0, 0);
                DateTime ending = new DateTime(2019, 12, 31, 23, 59, 59);

                /* For every station*/
                for (int stationId = 10; stationId <= 30; stationId++)
                {
                    /* For every hour in one year */
                    for (DateTime t = beginning; t <= ending; t = t.AddHours(1d))
                    {
                        file.WriteLine($"'NULL', '{GetRandomNumber(0, 360)}', '{getTimesamp(t)}', '{stationId}', '7', '6'");
                        dataCount++;

                    }
                }
            }
        }

        private void GenerateHumidityData()
        {
            /* Generate Humidity Data */
            using (StreamWriter file = new StreamWriter("measurementsHumidity.bulk"))
            {
                DateTime beginning = new DateTime(2018, 1, 1, 0, 0, 0);
                DateTime ending = new DateTime(2019, 12, 31, 23, 59, 59);

                /* For every station*/
                for (int stationId = 40; stationId <= 70; stationId++)
                {
                    /* For every hour in one year */
                    for (DateTime t = beginning; t <= ending; t = t.AddHours(1d))
                    {
                        file.WriteLine($"'NULL', '{GetAvgHumidity(GetSeason(t)) + GetRandomNumber(-10f, 10f)}', '{getTimesamp(t)}', '{stationId}', '6', '4'");
                        dataCount++;

                    }
                }
            }
        }

        private void GenerateTemperaturData()
        {
            /* Generate Temperature Data */
            using (StreamWriter file = new StreamWriter("measurementsTemperature.bulk"))
            {
                DateTime beginning = new DateTime(2018, 1, 1, 0, 0, 0);
                DateTime ending = new DateTime(2019, 12, 31, 23, 59, 59);

                /* For every station*/
                for (int stationId = 1; stationId <= 30; stationId++)
                {
                    /* For every hour in one year */
                    for (DateTime t = beginning; t <= ending; t = t.AddHours(1d))
                    {
                        file.WriteLine($"'NULL', '{GetTemperature(GetSeason(t), t.Hour) + GetRandomNumber(-1.25f, 1.25f)}', '{getTimesamp(t)}', '{stationId}', '4', '1'");
                        dataCount++;

                    
                    }
                }
            }
        }

        private static void Main(string[] args)
        {
            var generator = new Generator();
            generator.Generate();
        }
    }
}