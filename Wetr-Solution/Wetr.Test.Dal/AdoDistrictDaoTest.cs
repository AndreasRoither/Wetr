using Common.Dal.Ado;
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
    public class AdoDistrictDaoTest : DaoBaseTest
    {
        private static AdoFactory factory = AdoFactory.Instance;
        private static readonly AdoCommunityDao communityDao = (AdoCommunityDao)factory.GetCommunityDao();
        private static readonly AdoCountryDao countryDao = (AdoCountryDao)factory.GetCountryDao();
        private static readonly AdoProvinceDao provinceDao = (AdoProvinceDao)factory.GetProvinceDao();
        private static readonly AdoDistrictDao districtDao = (AdoDistrictDao)factory.GetDistrictDao();

        [ClassInitialize]
        public static async Task ClassInitializeAsync(TestContext context)
        {
            Country country = new Country
            {
                CountryId = 7,
                Name = "TestCountry"
            };
            await countryDao.InsertAsync(country);

            Province province = new Province
            {
                CountryId = 7,
                ProvinceId = 4,
                Name = "TestProvince"
            };
            await provinceDao.InsertAsync(province);

            District district = new District
            {
                DistrictId = 4,
                ProvinceId = 4,
                Name = "TestDistrict"
            };
            await districtDao.InsertAsync(district);
        }

        [ClassCleanup]
        public static async Task ClassCleanupAsync()
        {
            await districtDao.DeleteAsync(4);
            await provinceDao.DeleteAsync(4);
            await countryDao.DeleteAsync(7);
        }

        [TestMethod]
        public async override Task TestDeleteAsync()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                int districtId = 4;
                await districtDao.DeleteAsync(districtId);
                District district = await districtDao.FindByIdAsync(districtId);
                Assert.IsNull(district);
            }
        }

        [TestMethod]
        public async override Task TestFindAllAsync()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                District district = new District
                {
                    DistrictId = 10,
                    ProvinceId = 4,
                    Name = "TestDistrict"
                };

                District district2 = new District
                {
                    DistrictId = 11,
                    ProvinceId = 4,
                    Name = "TestDistrict"
                };
                await districtDao.InsertAsync(district);
                await districtDao.InsertAsync(district2);

                IEnumerable<District> districts = await districtDao.FindAllAsync();

                CollectionAssert.Contains(districts.ToList(), district);
                CollectionAssert.Contains(districts.ToList(), district2);
            }
        }

        [TestMethod]
        public async override Task TestFindByIdAsync()
        {
            int districtId = 4;
            District district = await districtDao.FindByIdAsync(districtId);
            Assert.IsTrue(district.DistrictId == districtId);
        }

        [TestMethod]
        public async Task TestFindByProvinceIdAsync()
        {
            int provinceId = 4;
            IEnumerable<District> districts = await districtDao.FindByProvinceIdAsync(provinceId);

            foreach (var district in districts)
                Assert.IsTrue(district.ProvinceId == provinceId);
        }

        [TestMethod]
        public async override Task TestInsertAsync()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                int districtId = 5;
                District district = new District
                {
                    DistrictId = districtId,
                    ProvinceId = 4,
                    Name = "TestDistrict"
                };
                Assert.IsTrue(await districtDao.InsertAsync(district));

                District test = await districtDao.FindByIdAsync(districtId);
                Assert.IsTrue(test.Equals(district));
            }
        }

        [TestMethod]
        public async override Task TestUpdateAsync()
        {
            int districtId = 4;

            District district = await districtDao.FindByIdAsync(districtId);
            district.Name = "DistrictUpdate";
            await districtDao.UpdateAsync(district);

            District test = await districtDao.FindByIdAsync(districtId);
            Assert.IsTrue(test.Name.Equals("DistrictUpdate"));
        }
    }
}