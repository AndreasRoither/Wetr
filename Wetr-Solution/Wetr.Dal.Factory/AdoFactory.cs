using Common.Dal.Ado;
using Wetr.Dal.Ado;
using Wetr.Dal.Interface;

namespace Wetr.Dal.Factory
{
    public class AdoFactory : IDaoFactory
    {
        private const string connectionStringConfigName_factory = "WETR-Testing";

        private static AdoFactory instance = null;
        private static readonly object padlock = new object();

        private AdoFactory() { }

        // thread safe singleton
        public static AdoFactory Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new AdoFactory();
                    }
                    return instance;
                }
            }
        }

        public IAddressDao GetAddressDao(string connectionStringConfigName = connectionStringConfigName_factory)
        {
            return new AdoAddressDao(DefaultConnectionFactory.FromConfiguration(connectionStringConfigName));
        }

        public ICommunityDao GetCommunityDao(string connectionStringConfigName = connectionStringConfigName_factory)
        {
            return new AdoCommunityDao(DefaultConnectionFactory.FromConfiguration(connectionStringConfigName));
        }

        public ICountryDao GetCountryDao(string connectionStringConfigName = connectionStringConfigName_factory)
        {
            return new AdoCountryDao(DefaultConnectionFactory.FromConfiguration(connectionStringConfigName));
        }

        public IDistrictDao GetDistrictDao(string connectionStringConfigName = connectionStringConfigName_factory)
        {
            return new AdoDistrictDao(DefaultConnectionFactory.FromConfiguration(connectionStringConfigName));
        }

        public IMeasurementDao GetMeasurementDao(string connectionStringConfigName = connectionStringConfigName_factory)
        {
            return new AdoMeasurementDao(DefaultConnectionFactory.FromConfiguration(connectionStringConfigName));
        }

        public IMeasurementTypeDao GetMeasurementTypeDao(string connectionStringConfigName = connectionStringConfigName_factory)
        {
            return new AdoMeasurementTypeDao(DefaultConnectionFactory.FromConfiguration(connectionStringConfigName));
        }

        public IPermissionDao GetPermissionDao(string connectionStringConfigName = connectionStringConfigName_factory)
        {
            return new AdoPermissionDao(DefaultConnectionFactory.FromConfiguration(connectionStringConfigName));
        }

        public IProvinceDao GetProvinceDao(string connectionStringConfigName = connectionStringConfigName_factory)
        {
            return new AdoProvinceDao(DefaultConnectionFactory.FromConfiguration(connectionStringConfigName));
        }

        public IStationDao GetStationDao(string connectionStringConfigName = connectionStringConfigName_factory)
        {
            return new AdoStationDao(DefaultConnectionFactory.FromConfiguration(connectionStringConfigName));
        }

        public IStationTypeDao GetStationTypeDao(string connectionStringConfigName = connectionStringConfigName_factory)
        {
            return new AdoStationTypeDao(DefaultConnectionFactory.FromConfiguration(connectionStringConfigName));
        }

        public IUnitDao GetUnitDao(string connectionStringConfigName = connectionStringConfigName_factory)
        {
            return new AdoUnitDao(DefaultConnectionFactory.FromConfiguration(connectionStringConfigName));
        }

        public IUserDao GetUserDao(string connectionStringConfigName = connectionStringConfigName_factory)
        {
            return new AdoUserDao(DefaultConnectionFactory.FromConfiguration(connectionStringConfigName));
        }
    }
}
