namespace Wetr.BusinessLogic
{
    public static class ManagerLocator
    {
        private static AddressManager addressManager;
        private static MeasurementManager measurementManager;
        private static StationManager stationManager;
        private static UserManager userManager;
        private static readonly object padlock = new object();

        public static AddressManager GetAddressManagerInstance
        {
            get
            {
                lock (padlock)
                {
                    if (addressManager == null)
                    {
                        addressManager = new AddressManager();
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
                        measurementManager = new MeasurementManager();
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
                        stationManager = new StationManager();
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
                        userManager = new UserManager();
                    }
                    return userManager;
                }
            }
        }
    }
}