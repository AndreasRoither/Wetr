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
                        addressManager = new AddressManager(databaseName);
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
                        measurementManager = new MeasurementManager(databaseName);
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
                        stationManager = new StationManager(databaseName);
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
                        userManager = new UserManager(databaseName);
                    }
                    return userManager;
                }
            }
        }
    }
}