using System;
using Wetr.BusinessLogic.Interface;
using Wetr.Dal.Factory;
using Wetr.Dal.Interface;

namespace Wetr.BusinessLogic
{
    public static class ManagerLocator
    {
        private static AddressManager addressManager;
        private static MeasurementManager measurementManager;
        private static StationManager stationManager;
        private static UserManager userManager;
        private static readonly object padlock = new object();
        private static readonly string databaseName = "wetr";

        public static AddressManager GetAddressManagerInstance
        {
            get
            {
                lock (padlock)
                {
                    if (addressManager == null)
                    {
                        ICountryDao countryDao;
                        IProvinceDao provinceDao;
                        IDistrictDao districtDao;
                        ICommunityDao communityDao;
                        IAddressDao addressDao;

                        try
                        {
                            countryDao = AdoFactory.Instance.GetCountryDao("wetr");
                            provinceDao = AdoFactory.Instance.GetProvinceDao("wetr");
                            districtDao = AdoFactory.Instance.GetDistrictDao("wetr");
                            communityDao = AdoFactory.Instance.GetCommunityDao("wetr");
                            addressDao = AdoFactory.Instance.GetAddressDao("wetr");
                        }
                        catch (Exception ex)
                        {
                            throw new BusinessSqlException(ex.Message, ex.InnerException);
                        }

                        addressManager = new AddressManager(countryDao, provinceDao, districtDao, communityDao, addressDao);
                    }
                    return addressManager;
                }
            }
        }

        public static MeasurementManager GetMeasurementManagerInstance
        {
            get
            {
                lock (padlock)
                {
                    if (measurementManager == null)
                    {
                        IMeasurementDao measurementDao;
                        IStationDao stationDao;
                        IAddressManager addressManager;

                        try
                        {
                            measurementDao = AdoFactory.Instance.GetMeasurementDao("wetr");
                            stationDao = AdoFactory.Instance.GetStationDao("wetr");
                            addressManager = GetAddressManagerInstance;

                        }
                        catch (Exception ex)
                        {
                            throw new BusinessSqlException(ex.Message, ex.InnerException);
                        }

                        measurementManager = new MeasurementManager(measurementDao, stationDao, addressManager);
                    }
                    return measurementManager;
                }
            }
        }

        public static StationManager GetStationManagerInstance
        {
            get
            {
                lock (padlock)
                {
                    if (stationManager == null)
                    {
                        IStationDao stationDao;
                        IStationTypeDao stationTypeDao;
                        IMeasurementDao measurementDao;

                        try
                        {
                            stationDao = AdoFactory.Instance.GetStationDao("wetr");
                            stationTypeDao = AdoFactory.Instance.GetStationTypeDao("wetr");
                            measurementDao = AdoFactory.Instance.GetMeasurementDao("wetr");
                        }
                        catch (Exception ex)
                        {
                            throw new BusinessSqlException(ex.Message, ex.InnerException);
                        }

                        stationManager = new StationManager(stationDao, stationTypeDao, measurementDao);
                    }
                    return stationManager;
                }
            }
        }

        public static UserManager GetUserManagerInstance
        {
            get
            {
                lock (padlock)
                {
                    if (userManager == null)
                    {
                        IUserDao userDao;
                        try
                        {
                            userDao = AdoFactory.Instance.GetUserDao("wetr");
                        }
                        catch (Exception ex)
                        {
                            throw new BusinessSqlException(ex.Message, ex.InnerException);
                        }
                        userManager = new UserManager(userDao);
                    }
                    return userManager;
                }
            }
        }
    }
}