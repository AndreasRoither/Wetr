using Common.Dal.Ado;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Wetr.Dal.Ado;
using Wetr.Domain;

namespace Wetr.Test.Dal
{
    [TestClass]
    public class AdoCountryDaoTest : DaoBaseTest
    {
        private static readonly AdoCountryDao countryDao = new AdoCountryDao(DefaultConnectionFactory.FromConfiguration("WETR-Testing"));

        [ClassInitialize]
        public static async Task ClassInitializeAsync(TestContext context)
        {
            Country country = new Country
            {
                CountryId = 3,
                Name = "TestCountry"
            };
            await countryDao.InsertAsync(country);
        }

        [ClassCleanup]
        public static async Task ClassCleanupAsync()
        {
            await countryDao.DeleteAsync(3);
        }

        [TestMethod]
        public async override Task TestDeleteAsync()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                int countryId = 3;
                Assert.IsTrue(await countryDao.DeleteAsync(countryId));

                Country test = await countryDao.FindByIdAsync(countryId);

                Assert.IsNull(test);
            }
        }

        [TestMethod]
        public async override Task TestFindAllAsync()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                Country country = new Country
                {
                    CountryId = 4,
                    Name = "TestCountry"
                };
                Country country2 = new Country
                {
                    CountryId = 5,
                    Name = "TestCountry"
                };
                await countryDao.InsertAsync(country);
                await countryDao.InsertAsync(country2);

                IEnumerable<Country> countries = await countryDao.FindAllAsync();

                CollectionAssert.Contains(countries.ToList(), country);
                CollectionAssert.Contains(countries.ToList(), country2);
            }
        }

        [TestMethod]
        public async override Task TestFindByIdAsync()
        {
            int CountryId = 3;
            Country test = await countryDao.FindByIdAsync(CountryId);
            Assert.IsTrue(test.CountryId == CountryId);
        }

        [TestMethod]
        public async override Task TestInsertAsync()
        {
            
                int CountryId = 6;
                Country country = new Country
                {
                    CountryId = CountryId,
                    Name = "TestCountry"
                };
                await countryDao.InsertAsync(country);

                Country test = await countryDao.FindByIdAsync(CountryId);
                Assert.IsTrue(test.Equals(country));
            
        }

        [TestMethod]
        public async override Task TestUpdateAsync()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                Country update = await countryDao.FindByIdAsync(3);
                update.Name = "TestUpdate";

                Assert.IsTrue(await countryDao.UpdateAsync(update));
                Country test = await countryDao.FindByIdAsync(update.CountryId);

                Assert.IsTrue(test.Equals(update));
            }
        }
    }
}