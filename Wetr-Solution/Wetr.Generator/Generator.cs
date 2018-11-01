using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Wetr.Generator
{
    class Generator
    {

        private string[] names;

        public enum Season
        {
            SUMMER, WINTER, FALL, SPRING, UKN
        }

        public enum State
        {
            SUNNY, CLOUDY, RAINY, FOGGY, 
        }

        /* Min and May Temp from https://www.klimatabelle.info/europa/oesterreich */
        private Tuple<float,float> GetTempRange(Season season)
        {
            switch (season)
            {
                case Season.SPRING: return Tuple.Create(8.23f,15f);
                case Season.SUMMER: return Tuple.Create(13.2f, 23.76f);
                case Season.FALL: return Tuple.Create(5.23f, 14.1f);
                case Season.WINTER: return Tuple.Create(-3.1f, 4.6f);
                default: return Tuple.Create(0f, 0f);
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
        static Random random = new Random();
        public double GetRandomNumber(double minimum, double maximum)
        {
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        public float GetTemperature(Season s, int hour)
        {
            Tuple<float, float> range = GetTempRange(s);
            if(hour < 12)
                return Map(hour, 0, 11, range.Item1, range.Item2);
            return Map(hour, 12, 23, range.Item2, range.Item1);
        }

        private void Generate()
        {
            /* https://stackoverflow.com/questions/3135569/how-to-change-symbol-for-decimal-point-in-double-tostring */
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");

            /* Generate Temperature Data */
            using (StreamWriter file = new StreamWriter("measurementsTemperature.sql"))
            {
                DateTime beginning = new DateTime(2015, 1, 1, 0, 0, 0);
                DateTime ending = new DateTime(2015, 12,31, 23, 59, 59);

                file.WriteLine("INSERT INTO `measurement` (`measurementId`, `value`, `timestamp`, `stationId`, `unitId`, `measurementTypeId`) VALUES");

                /* For every station*/
                for(int stationId = 1; stationId < 70; stationId++)
                {
                    /* For every hour in one year */
                    for (DateTime t = beginning; t <= ending; t = t.AddHours(1d))
                    {
                        file.WriteLine($"(NULL, '{GetTemperature(GetSeason(t),t.Hour) + GetRandomNumber(-1.25f,1.25f)}', '{getTimesamp(t)}', '{stationId}', '4', '1'),");
                    }
                }


                Console.WriteLine("Done!");
            }


            return;

            /* Loading datapools */
            this.LoadUsernames();

            using (StreamWriter file = new StreamWriter("insert.sql"))
            {
                GenerateUsers(file);
            }
        }

        static void Main(string[] args)
        {
            var generator = new Generator();
            generator.Generate();
        }

        private void LoadUsernames()
        {
            this.names = File.ReadLines("users.txt").ToArray(); ;
            this.names.ToList().ForEach(line => this.names[Array.IndexOf(this.names, line)] = line.Trim());
        }

        private void GenerateUsers(StreamWriter file)
        {

            string password = "$1$YOXunEUT$4X9aCMw9B63FkjCntsoGG0";


            file.WriteLine("INSERT INTO user (userId, firstName, lastName, password) VALUES");

            file.Write($"(0, \"{names[0].Split(' ')[0]}\", \"{names[0].Split(' ')[1]}\", \"{password}\")");

            for (int id = 1; id < names.Count(); id++)
            {
                file.Write($",\n({id}, \"{names[id].Split(' ')[0]}\", \"{names[id].Split(' ')[1]}\", \"{password}\")");
            }

            file.Write(";");

        }
    }
}
