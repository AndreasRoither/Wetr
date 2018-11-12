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
    public class AdoProvinceDaoTest : DaoBaseTest
    {
        private static AdoFactory factory = AdoFactory.Instance;
        private static readonly AdoCountryDao countryDao = (AdoCountryDao)factory.GetCountryDao();
        private static readonly AdoProvinceDao provinceDao = (AdoProvinceDao)factory.GetProvinceDao();


        [ClassInitialize]
        public static async Task ClassInitializeAsync(TestContext context)
        {
            Country country = new Country
            {
                CountryId = 8,
                Name = "TestCountry"
            };
            await countryDao.InsertAsync(country);

            Province province = new Province
            {
                CountryId = 8,
                ProvinceId = 5,
                Name = "TestProvince"
            };
            await provinceDao.InsertAsync(province);
        }

        [ClassCleanup]
        public static async Task ClassCleanupAsync()
        {
            await provinceDao.DeleteAsync(5);
            await countryDao.DeleteAsync(8);
        }

        [TestMethod]
        public async override Task TestDeleteAsync()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                int provinceId = 5;
                Assert.IsTrue(await provinceDao.DeleteAsync(provinceId));
                Province province = await provinceDao.FindByIdAsync(provinceId);
                Assert.IsNull(province);
            }
        }

        [TestMethod]
        public async override Task TestFindAllAsync()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                Province province = new Province
                {
                    CountryId = 8,
                    ProvinceId = 10,
                    Name = "Province 2"
                };

                Province province2 = new Province
                {
                    CountryId = 8,
                    ProvinceId = 11,
                    Name = "Province 3"
                };

                await provinceDao.InsertAsync(province);
                await provinceDao.InsertAsync(province2);

                IEnumerable<Province> provinces = await provinceDao.FindAllAsync();

                CollectionAssert.Contains(provinces.ToList(), province);
                CollectionAssert.Contains(provinces.ToList(), province2);
            }
        }

        [TestMethod]
        public async Task TestFindByCountryIdAsync()
        {
            int countryId = 8;
            IEnumerable<Province> provinces = await provinceDao.FindByCountryIdAsync(countryId);

            foreach (var province in provinces)
                Assert.IsTrue(province.CountryId == countryId);
        }

        [TestMethod]
        public async override Task TestFindByIdAsync()
        {
            int provinceId = 5;
            Province province = await provinceDao.FindByIdAsync(provinceId);
            Assert.IsTrue(province.ProvinceId == provinceId);
        }

        [TestMethod]
        public async override Task TestInsertAsync()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                int provinceId = 12;
                Province province = new Province
                {
                    CountryId = 8,
                    ProvinceId = provinceId,
                    Name = "Province 2"
                };
                await provinceDao.InsertAsync(province);

                Province test = await provinceDao.FindByIdAsync(provinceId);

                Assert.IsTrue(test.Equals(province));
            }
        }

        [TestMethod]
        public async override Task TestUpdateAsync()
        {
            int provinceId = 5;
            Province province = await provinceDao.FindByIdAsync(provinceId);
            province.Name = "ProvinceUpdate";
            Assert.IsTrue(await provinceDao.UpdateAsync(province));

            Province test = await provinceDao.FindByIdAsync(provinceId);

            Assert.IsTrue(test.Equals(province));
        }
    }
}