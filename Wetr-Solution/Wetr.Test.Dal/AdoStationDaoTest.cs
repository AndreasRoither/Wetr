using Common.Dal.Ado;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Wetr.Dal.Ado;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.Test.Dal
{
    [TestClass]
    public class AdoStationDaoTest : DaoBaseTest
    {
        private static readonly IStationDao stationDao = new AdoStationDao(DefaultConnectionFactory.FromConfiguration("MysqlConnection"));
        private static readonly AdoUserDao userDao = new AdoUserDao(DefaultConnectionFactory.FromConfiguration("MysqlConnection"));
        private static readonly AdoCountryDao countryDao = new AdoCountryDao(DefaultConnectionFactory.FromConfiguration("MysqlConnection"));
        private static readonly AdoProvinceDao provinceDao = new AdoProvinceDao(DefaultConnectionFactory.FromConfiguration("MysqlConnection"));
        private static readonly AdoCommunityDao communityDao = new AdoCommunityDao(DefaultConnectionFactory.FromConfiguration("MysqlConnection"));
        private static readonly AdoDistrictDao districtDao = new AdoDistrictDao(DefaultConnectionFactory.FromConfiguration("MysqlConnection"));
        private static readonly AdoAddressDao addressDao = new AdoAddressDao(DefaultConnectionFactory.FromConfiguration("MysqlConnection"));
        private static readonly AdoStationTypeDao stationTypeDao = new AdoStationTypeDao(DefaultConnectionFactory.FromConfiguration("MysqlConnection"));

        [ClassInitialize]
        public static async Task ClassInitializeAsync(TestContext context)
        {
            User user = new User
            {
                UserId = 1,
                FirstName = "Andreas",
                LastName = "Roither",
                Email = "test.test@outlook.com",
                Password = "SomeHash"
            };
            await userDao.InsertAsync(user);

            Country country = new Country
            {
                CountryId = 1,
                Name = "TestCountry"
            };
            await countryDao.InsertAsync(country);

            Province province = new Province
            {
                CountryId = 1,
                ProvinceId = 1,
                Name = "TestCommunity"
            };
            await provinceDao.InsertAsync(province);

            District district = new District
            {
                DistrictId = 1,
                ProvinceId = 1,
                Name = "TestDistrict"
            };
            await districtDao.InsertAsync(district);

            Community community = new Community
            {
                DistrictId = 1,
                CommunityId = 1,
                Name = "TestCommunity"
            };
            await communityDao.InsertAsync(community);

            Address a = new Address
            {
                AddressId = 1,
                CommunityId = 1,
                Location = "TestLocation",
                Zip = "124"
            };
            await addressDao.InsertAsync(a);

            StationType type = new StationType
            {
                StationTypeId = 1,
                Name = "TestType"
            };
            await stationTypeDao.InsertAsync(type);

            Station station = new Station
            {
                StationId = 1,
                Name = "Station 1",
                Longitude = 12,
                Latitude = 12,
                StationTypeId = 1,
                AddressId = 1,
                UserId = 1
            };
            await stationDao.InsertAsync(station);
        }

        [ClassCleanup]
        public static async Task ClassCleanupAsync()
        {
            await stationDao.DeleteAsync(1);
            await stationTypeDao.DeleteAsync(1);
            await addressDao.DeleteAsync(1);
            await communityDao.DeleteAsync(1);
            await districtDao.DeleteAsync(1);
            await provinceDao.DeleteAsync(1);
            await countryDao.DeleteAsync(1);
            await userDao.DeleteAsync(1);
        }

        [TestMethod]
        public async Task TestFindByUserIdAsync()
        {
            int UserId = 1;
            IEnumerable<Station> stations = await stationDao.FindByUserIdAsync(UserId);

            foreach (var station in stations)
                Assert.IsTrue(station.UserId == UserId);
        }

        [TestMethod]
        public async Task TestFindByStationTypeIdAsync()
        {
            int StationTypeId = 1;
            IEnumerable<Station> stations = await stationDao.FindByStationTypeIdAsync(StationTypeId);

            foreach (var station in stations)
                Assert.IsTrue(station.StationTypeId == StationTypeId);
        }

        [TestMethod]
        public async Task TestFindByAddressIdAsync()
        {
            int AddressId = 1;
            IEnumerable<Station> stations = await stationDao.FindByAddressIdAsync(AddressId);

            foreach (var station in stations)
                Assert.IsTrue(station.AddressId == AddressId);
        }

        [TestMethod]
        public async override Task TestFindByIdAsync()
        {
            int StationId = 1;
            Station station = await stationDao.FindByIdAsync(StationId);
            Assert.IsTrue(station.StationId == StationId);
        }

        [TestMethod]
        public async override Task TestFindAllAsync()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                Station station1 = new Station
                {
                    StationId = 3,
                    Name = "Station 1",
                    Longitude = 12,
                    Latitude = 12,
                    StationTypeId = 1,
                    AddressId = 1,
                    UserId = 1
                };

                Station station2 = new Station
                {
                    StationId = 4,
                    Name = "Station 2",
                    Longitude = 12,
                    Latitude = 12,
                    StationTypeId = 1,
                    AddressId = 1,
                    UserId = 1
                };
                await stationDao.InsertAsync(station1);
                await stationDao.InsertAsync(station2);

                IEnumerable<Station> stations = await stationDao.FindAllAsync();

                CollectionAssert.Contains(stations.ToList(), station1);
                CollectionAssert.Contains(stations.ToList(), station2);

                // Never called since a rollback of the db is desired
                // transaction.Complete();
            }
        }

        [TestMethod]
        public async override Task TestUpdateAsync()
        {
            int StationId = 1;
            Station station = await stationDao.FindByIdAsync(StationId);

            station.Name = "TestUpdate";
            station.Latitude = 1;
            station.Longitude = 1;

            await stationDao.UpdateAsync(station);

            Station station2 = await stationDao.FindByIdAsync(StationId);

            Assert.IsTrue(station.Equals(station2));
        }

        [TestMethod]
        public async override Task TestDeleteAsync()
        {
            int StationId = 1;
            await stationDao.DeleteAsync(StationId);
            Station station = await stationDao.FindByIdAsync(StationId);

            Assert.IsNull(station);
        }

        [TestMethod]
        public async override Task TestInsertAsync()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                int StationId = 3;
                Station station = new Station
                {
                    StationId = StationId,
                    Name = "Station 3",
                    Longitude = 12,
                    Latitude = 12,
                    StationTypeId = 1,
                    AddressId = 1,
                    UserId = 1
                };
                await stationDao.InsertAsync(station);

                Station test = await stationDao.FindByIdAsync(StationId);

                Assert.IsTrue(station.Equals(test));

                // Never called since a rollback of the db is desired
                // transaction.Complete();
            }
        }
    }
}