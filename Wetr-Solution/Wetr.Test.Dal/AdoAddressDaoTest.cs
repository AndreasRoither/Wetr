using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Wetr.Dal.Ado;
using Wetr.Dal.Factory;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.Test.Dal
{
    [TestClass]
    public class AdoAddressDaoTest : DaoBaseTest
    {
        private static AdoFactory factory = AdoFactory.Instance;
        private static readonly IAddressDao addressDao = factory.GetAddressDao();
        private static readonly ICommunityDao communityDao = factory.GetCommunityDao();
        private static readonly ICountryDao countryDao = factory.GetCountryDao();
        private static readonly IProvinceDao provinceDao = factory.GetProvinceDao();
        private static readonly IDistrictDao districtDao = factory.GetDistrictDao();

        [ClassInitialize]
        public static async Task ClassInitializeAsync(TestContext context)
        {
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
                Name = "TestProvince"
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

            Address address = new Address
            {
                AddressId = 1,
                CommunityId = 1,
                Location = "TestLocation",
                Zip = "124"
            };
            await addressDao.InsertAsync(address);
        }

        [ClassCleanup]
        public static async Task ClassCleanupAsync()
        {
            await addressDao.DeleteAsync(1);
            await communityDao.DeleteAsync(1);
            await districtDao.DeleteAsync(1);
            await provinceDao.DeleteAsync(1);
            await countryDao.DeleteAsync(1);
        }

        [TestMethod]
        public async Task FindByCommunityIdAsync()
        {
            int communityId = 1;

            IEnumerable<Address> addresses = await addressDao.FindByCommunityIdAsync(communityId);

            foreach (var address in addresses)
                Assert.IsTrue(address.AddressId == communityId);
        }

        [TestMethod]
        public async override Task TestDeleteAsync()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                int addressId = 1;
                Assert.IsTrue(await addressDao.DeleteAsync(addressId));

                Address test = await addressDao.FindByIdAsync(addressId);
                Assert.IsNull(test);
            }
        }

        [TestMethod]
        public async override Task TestFindAllAsync()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                int communityId = 1;

                Address address = new Address
                {
                    AddressId = 2,
                    CommunityId = communityId,
                    Location = "TestLocation2",
                    Zip = "124"
                };

                Address address2 = new Address
                {
                    AddressId = 3,
                    CommunityId = communityId,
                    Location = "TestLocation3",
                    Zip = "124"
                };

                await addressDao.InsertAsync(address);
                await addressDao.InsertAsync(address2);

                IEnumerable<Address> addresses = await addressDao.FindAllAsync();

                CollectionAssert.Contains(addresses.ToList(), address);
                CollectionAssert.Contains(addresses.ToList(), address2);
            }
        }

        [TestMethod]
        public async override Task TestFindByIdAsync()
        {
            int addressId = 1;
            Address address = await addressDao.FindByIdAsync(addressId);

            Assert.IsTrue(address.AddressId == addressId);
        }

        [TestMethod]
        public async override Task TestInsertAsync()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                int addressId = 2;

                Address address = new Address
                {
                    AddressId = addressId,
                    CommunityId = 1,
                    Location = "TestLocation",
                    Zip = "124"
                };
                await addressDao.InsertAsync(address);

                Address test = await addressDao.FindByIdAsync(addressId);
                Assert.IsTrue(test.Equals(address));
            }
        }

        [TestMethod]
        public async override Task TestUpdateAsync()
        {
            int addressId = 1;

            Address address = await addressDao.FindByIdAsync(addressId);
            address.Location = "TestUpdate";

            Assert.IsTrue(await addressDao.UpdateAsync(address));
            Address test = await addressDao.FindByIdAsync(addressId);

            Assert.IsTrue(test.Equals(address));
        }
    }
}