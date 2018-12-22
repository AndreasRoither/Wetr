using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TweetSharp;
using Wetr.BusinessLogic.Interface;
using Wetr.Dal.Factory;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.BusinessLogic
{
    public class MeasurementManager : IMeasurementManager
    {
        TwitterService twitter;

        private enum WarningType
        {
            Storm, HeavyRain, DryAir, HighTemperature, LowTemperature
        }

        private class Warning
        {
            public WarningType Type { get; set; }
            public int StationId { get; set; }
            public int TypeId { get; set; }

            public DateTime Timestamp { get; set; }
        }

        private List<Warning> PastWarnings = new List<Warning>();

        private IMeasurementDao measurementDao;
        private IStationDao stationDao;

        private IAddressManager addressManager;

        public MeasurementManager(IMeasurementDao measurementDao, IStationDao stationDao, IAddressManager addressManager)
        {

            this.measurementDao = measurementDao;
            this.stationDao = stationDao;
            this.addressManager = addressManager;

            SetupTwitter();

            /* Twitter Testing  

            Measurement m = new Measurement()
            {
                StationId = 45,
                MeasurementTypeId = 1,
                Value = -23,
                TimesStamp = DateTime.Now,
                UnitId=1
            
            };

            bool res = AddMeasurement(m).Result;

            */

        }

        private void SetupTwitter()
        {
            twitter = new TwitterService("hkqgdQV3Myye16fqVh5zKj75e", "3CmduRd1NQHJlWsBgYW0a2lrBfdV6h7RHFZHUe3zpIpGyFHAYN");
            twitter.AuthenticateWith("1076539766194216961-kczAJ2VkSHBqxPiqiQXu8o4pERimBc", "lbrTQdoIzXvGsELDrCmidt7Zq4XdFY7RFCDMBsga3exDU");
        }

        public async Task<bool> AddMeasurement(Measurement m)
        {
            /* Process for weather warning */

            await CheckForWarning(m);

            return await measurementDao.InsertAsync(m);
        }

        private async Task CheckForWarning(Measurement m)
        {
            Warning warning = null;

            // Temperature
            if (m.MeasurementTypeId == 1)
            {
                if (m.Value < -10)
                {
                    warning = new Warning()
                    {
                        Type = WarningType.LowTemperature,
                        Timestamp = m.TimesStamp,
                        StationId = m.StationId,
                        TypeId = m.MeasurementTypeId


                    };
                }
                else if (m.Value > 38)
                {
                    warning = new Warning()
                    {
                        Type = WarningType.HighTemperature,
                        Timestamp = m.TimesStamp,
                        StationId = m.StationId,
                        TypeId = m.MeasurementTypeId

                    };
                }

            }
            else if (m.MeasurementTypeId == 3)
            {
                if (m.Value >= 35)
                {
                    warning = new Warning()
                    {
                        Type = WarningType.HeavyRain,
                        Timestamp = m.TimesStamp,
                        StationId = m.StationId,
                        TypeId = m.MeasurementTypeId
                    };
                }
            }
            else if (m.MeasurementTypeId == 5)
            {
                if (m.Value >= 65)
                {
                    warning = new Warning()
                    {
                        Type = WarningType.Storm,
                        Timestamp = m.TimesStamp,
                        StationId = m.StationId,
                        TypeId = m.MeasurementTypeId
                    };
                }
            }
            else if (m.MeasurementTypeId == 4)
            {
                if (m.Value >= 45)
                {
                    warning = new Warning()
                    {
                        Type = WarningType.DryAir,
                        Timestamp = m.TimesStamp,
                        StationId = m.StationId,
                        TypeId = m.MeasurementTypeId
                    };
                }
            }

            /* Check warning interval */
            if (warning != null)
            {
                foreach (Warning w in PastWarnings)
                {
                    /* Remove old warnings */
                    if (w.Timestamp < DateTime.Now.AddHours(-1))
                    {
                        PastWarnings.Remove(w);
                    }
                }

                /* If there was alredy a warning of this type for this station in the last hour*/
                if (PastWarnings.Where(w => w.StationId == m.StationId && w.TypeId == m.MeasurementTypeId).Any())
                {
                    return;
                }

                PastWarnings.Add(warning);

                Station station = await stationDao.FindByIdAsync(m.StationId);
                string prov = (await addressManager.GetProvinceForAddressId(station.AddressId)).Name;
                this.Tweet("Warning: " + warning.Type.ToString() + " for province '" + prov + "'");

            }


        }

        private void Tweet(string msg)
        {
            twitter.SendTweet(new SendTweetOptions()
            {
                Status = msg
            });
        }

        #region functions



        public async Task<double[]> GetDashbardTemperaturesAsync()
        {
            try
            {
                return await measurementDao.GetDayAverageOfLastXDaysAsync((int)EMeasurementType.Temperature, 7);
            }
            catch (Common.Dal.Ado.MySqlException ex)
            {
                throw new BusinessSqlException(ex.Message, ex.InnerException);
            }
        }

        public async Task<double[]> GetDashboardRainValuesAsync()
        {
            try
            {
                return await measurementDao.GetDayAverageOfLastXDaysAsync((int)EMeasurementType.Rain, 7);
            }
            catch (Common.Dal.Ado.MySqlException ex)
            {
                throw new BusinessSqlException(ex.Message, ex.InnerException);
            }
        }

        public async Task<long> GetNumberOfMeasurementsAsync()
        {
            try
            {
                return await measurementDao.GetTotalNumberOfMeasurementsAsync();
            }
            catch (Common.Dal.Ado.MySqlException ex)
            {
                throw new BusinessSqlException(ex.Message, ex.InnerException);
            }
        }

        public async Task<long> GetNumberOfMeasurementsOfWeekAsync()
        {
            try
            {
                return await measurementDao.GetNumberOfMeasurementsFromTheLastXDaysAsync(7);
            }
            catch (Common.Dal.Ado.MySqlException ex)
            {
                throw new BusinessSqlException(ex.Message, ex.InnerException);
            }
        }

        public async Task<double[]> GetQueryResult(DateTime start, DateTime end, int measurementTypeId, int reductionTypeId, int groupingTypeId, List<Station> stations)
        {
            return await measurementDao.GetQueryResult(start, end, measurementTypeId, reductionTypeId, groupingTypeId, stations);
        }

        public async Task<double[]> GetQueryResult(DateTime start, DateTime end, int measurementTypeId, int reductionTypeId, int groupingTypeId, List<Station> stations, District disctrict)
        {
            return await measurementDao.GetQueryResult(start, end, measurementTypeId, reductionTypeId, groupingTypeId, stations, disctrict);
        }

        public async Task<double[]> GetQueryResult(DateTime start, DateTime end, int measurementTypeId, int reductionTypeId, int groupingTypeId, List<Station> stations, Province province)
        {
            return await measurementDao.GetQueryResult(start, end, measurementTypeId, reductionTypeId, groupingTypeId, stations, province);
        }

        public async Task<double[]> GetQueryResult(DateTime start, DateTime end, int measurementTypeId, int reductionTypeId, int groupingTypeId, List<Station> stations, Community community)
        {
            return await measurementDao.GetQueryResult(start, end, measurementTypeId, reductionTypeId, groupingTypeId, stations, community);
        }

        public async Task<double[]> GetQueryResult(DateTime start, DateTime end, int measurementTypeId, int reductionTypeId, int groupingTypeId, List<Station> stations, decimal lat, decimal lon, int radius)
        {
            return await measurementDao.GetQueryResult(start, end, measurementTypeId, reductionTypeId, groupingTypeId, stations, lat, lon, radius);
        }

        #endregion functions
    }
}