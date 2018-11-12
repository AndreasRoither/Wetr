using Common.Dal.Ado;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Wetr.Dal.Ado;
using Wetr.Dal.Factory;
using Wetr.Domain;

namespace Wetr.Test.Dal
{
    [TestClass]
    public class AdoCommunityDaoTest : DaoBaseTest
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
                CountryId = 2,
                Name = "TestCountry"
            };
            await countryDao.InsertAsync(country);

            Province province = new Province
            {
                CountryId = 2,
                ProvinceId = 2,
                Name = "TestProvince"
            };
            await provinceDao.InsertAsync(province);

            District district = new District
            {
                DistrictId = 2,
                ProvinceId = 2,
                Name = "TestDistrict"
            };
            await districtDao.InsertAsync(district);

            Community community = new Community
            {
                DistrictId = 2,
                CommunityId = 2,
                Name = "TestCommunity"
            };
            await communityDao.InsertAsync(community);
        }

        [ClassCleanup]
        public static async Task ClassCleanupAsync()
        {
            await communityDao.DeleteAsync(2);
            await districtDao.DeleteAsync(2);
            await provinceDao.DeleteAsync(2);
            await countryDao.DeleteAsync(2);
        }

        [TestMethod]
        public async override Task TestDeleteAsync()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                int communityId = 2;
                Assert.IsTrue(await communityDao.DeleteAsync(communityId));
                Community test = await communityDao.FindByIdAsync(communityId);
                Assert.IsNull(test);
            }
        }

        [TestMethod]
        public async override Task TestFindAllAsync()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                Community community = new Community
                {
                    DistrictId = 2,
                    CommunityId = 3,
                    Name = "TestCommunity2"
                };

                Community community2 = new Community
                {
                    DistrictId = 2,
                    CommunityId = 4,
                    Name = "TestCommunity3"
                };
                await communityDao.InsertAsync(community);
                await communityDao.InsertAsync(community2);

                IEnumerable<Community> communities = await communityDao.FindAllAsync();

                CollectionAssert.Contains(communities.ToList(), community);
                CollectionAssert.Contains(communities.ToList(), community2);
            }
        }

        [TestMethod]
        public async Task TestFindByDistrictIdAsync()
        {
            int districtId = 2;
            IEnumerable<Community> communities = await communityDao.FindByDistrictIdAsync(districtId);

            foreach (var community in communities)
                Assert.IsTrue(community.DistrictId == districtId);
        }

        [TestMethod]
        public async override Task TestFindByIdAsync()
        {
            int communityId = 2;
            Community test = await communityDao.FindByIdAsync(communityId);
            Assert.IsTrue(test.CommunityId == communityId);
        }

        [TestMethod]
        public async override Task TestInsertAsync()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                int communityId = 3;
                Community community = new Community
                {
                    DistrictId = 2,
                    CommunityId = communityId,
                    Name = "TestCommunity2"
                };

                await communityDao.InsertAsync(community);

                Community test = await communityDao.FindByIdAsync(communityId);

                Assert.IsTrue(test.Equals(community));
            }
        }

        [TestMethod]
        public async override Task TestUpdateAsync()
        {
            int communityId = 2;
            Community community = await communityDao.FindByIdAsync(communityId);
            community.Name = "CommunityUpdate";
            Assert.IsTrue(await communityDao.UpdateAsync(community));

            Community test = await communityDao.FindByIdAsync(communityId);
            Assert.IsTrue(test.Equals(community));
        }
    }
}