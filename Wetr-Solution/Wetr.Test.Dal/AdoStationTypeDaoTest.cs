using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Dal.Ado;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wetr.Dal.Ado;
using Wetr.Dal.Factory;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.Test.Dal
{
    [TestClass]
    public class AdoStationTypeDaoTest : DaoBaseTest
    {
        private static AdoFactory factory = AdoFactory.Instance;
        private static readonly IStationTypeDao stationTypeDao = factory.GetStationTypeDao();
        private static readonly IList<StationType> stationTypes = new List<StationType>();

        [ClassInitialize]
        public static async Task ClassInitializeAsync(TestContext context)
        {

            StationType t1 = new StationType
            {
                StationTypeId = 2,
                Name = "MyStationType1"
            };

            StationType t2 = new StationType
            {
                StationTypeId = 3,
                Name = "MyStationType2"
            };

            StationType t3 = new StationType
            {
                StationTypeId = 4,
                Name = "MyStationType3"
            };

            stationTypes.Add(t1);
            stationTypes.Add(t2);
            stationTypes.Add(t3);

            foreach (StationType s in stationTypes)
            {
                await stationTypeDao.InsertAsync(s);
            }
        }

        [ClassCleanup]
        public static async Task ClassCleanupAsync()
        {
            foreach (StationType s in stationTypes)
            {
                await stationTypeDao.DeleteAsync(s.StationTypeId);
            }
        }

        [TestMethod]
        public async override Task TestDeleteAsync()
        {
            StationType t = stationTypes[0];
            await stationTypeDao.DeleteAsync(t.StationTypeId);
            StationType s = await stationTypeDao.FindByIdAsync(t.StationTypeId);
            Assert.IsNull(s);
        }

        [TestMethod]
        public async override Task TestFindAllAsync()
        {
            IEnumerable<StationType> fetchedStationTypes = await stationTypeDao.FindAllAsync();
            foreach (StationType t in stationTypes)
                CollectionAssert.Contains(fetchedStationTypes.ToList(), t);
        }

        [TestMethod]
        public async override Task TestFindByIdAsync()
        {
            StationType t = stationTypes[0];
            StationType s = await stationTypeDao.FindByIdAsync(t.StationTypeId);
            Assert.AreEqual(t, s);
        }

        [TestMethod]
        public async override Task TestInsertAsync()
        {
            StationType s = new StationType
            {
                StationTypeId=5,
                Name="NewStationType"
            };

            await stationTypeDao.InsertAsync(s);

            StationType t = await stationTypeDao.FindByIdAsync(s.StationTypeId);

            Assert.AreEqual(s, t);

        }

        [TestMethod]
        public async override Task TestUpdateAsync()
        {
            StationType type = stationTypes[1];
            string newName = "MyNewName";

            StationType t = await stationTypeDao.FindByIdAsync(type.StationTypeId);
            t.Name = newName;
            await stationTypeDao.UpdateAsync(t);

            StationType fetched = await stationTypeDao.FindByIdAsync(type.StationTypeId);
            Assert.AreEqual(newName, fetched.Name);
        }
    }
}