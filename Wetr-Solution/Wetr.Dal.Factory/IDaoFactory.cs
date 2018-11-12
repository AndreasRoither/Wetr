using Wetr.Dal.Interface;

namespace Wetr.Dal.Factory
{
    interface IDaoFactory
    {
        IAddressDao           GetAddressDao(string connectionStringConfigName);
        ICommunityDao         GetCommunityDao(string connectionStringConfigName);
        ICountryDao           GetCountryDao(string connectionStringConfigName);
        IDistrictDao          GetDistrictDao(string connectionStringConfigName);
        IMeasurementDao       GetMeasurementDao(string connectionStringConfigName);
        IMeasurementTypeDao   GetMeasurementTypeDao(string connectionStringConfigName);
        IPermissionDao        GetPermissionDao(string connectionStringConfigName);
        IProvinceDao          GetProvinceDao(string connectionStringConfigName);
        IStationDao           GetStationDao(string connectionStringConfigName);
        IStationTypeDao       GetStationTypeDao(string connectionStringConfigName);
        IUnitDao              GetUnitDao(string connectionStringConfigName);
        IUserDao              GetUserDao(string connectionStringConfigName);
    }
}
