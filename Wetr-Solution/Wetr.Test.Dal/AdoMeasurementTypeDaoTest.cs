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
    public class AdoMeasurementTypeDaoTest : DaoBaseTest
    {
        private static AdoFactory factory = AdoFactory.Instance;
        private static readonly IMeasurementTypeDao measurementTypeDao = factory.GetMeasurementTypeDao();
        private static readonly IList<MeasurementType> measurementTypes = new List<MeasurementType>();

        [ClassInitialize]
        public static async Task ClassInitializeAsync(TestContext context)
        {
            MeasurementType t1 = new MeasurementType
            {
                MeasurementTypeId = 1,
                Name = "MyMeasurementType1"
            };

            MeasurementType t2 = new MeasurementType
            {
                MeasurementTypeId = 2,
                Name = "MyMeasurementType2"
            };

            MeasurementType t3 = new MeasurementType
            {
                MeasurementTypeId = 3,
                Name = "MyMeasurementType3"
            };

            measurementTypes.Add(t1);
            measurementTypes.Add(t2);
            measurementTypes.Add(t3);


            foreach (MeasurementType t in measurementTypes)
            {
                await measurementTypeDao.InsertAsync(t);
            }
        }

        [ClassCleanup]
        public static async Task ClassCleanupAsync()
        {
            foreach (MeasurementType m in measurementTypes)
            {
                await measurementTypeDao.DeleteAsync(m.MeasurementTypeId);
            }
        }

        [TestMethod]
        public async override Task TestDeleteAsync()
        {
            MeasurementType t = measurementTypes[0];
            await measurementTypeDao.DeleteAsync(t.MeasurementTypeId);
            MeasurementType s = await measurementTypeDao.FindByIdAsync(t.MeasurementTypeId);
            Assert.IsNull(s);
        }

        [TestMethod]
        public async override Task TestFindAllAsync()
        {
            IEnumerable<MeasurementType> fetchesMeasurementTypes = await measurementTypeDao.FindAllAsync();
            foreach (MeasurementType t in measurementTypes)
                CollectionAssert.Contains(fetchesMeasurementTypes.ToList(), t);
        }

        [TestMethod]
        public async override Task TestFindByIdAsync()
        {
            MeasurementType t = measurementTypes[0];
            MeasurementType s = await measurementTypeDao.FindByIdAsync(t.MeasurementTypeId);
            Assert.AreEqual(t,s);
        }

        [TestMethod]
        public async override Task TestInsertAsync()
        {
            MeasurementType s = new MeasurementType
            {
                MeasurementTypeId = 4,
                Name = "NewMeasurementType"
            };

            await measurementTypeDao.InsertAsync(s);

            MeasurementType t = await measurementTypeDao.FindByIdAsync(s.MeasurementTypeId);

            Assert.AreEqual(s, t);
        }

        [TestMethod]
        public async override Task TestUpdateAsync()
        {
            MeasurementType type = measurementTypes[1];
            string newName = "MyNewName";

            MeasurementType t = await measurementTypeDao.FindByIdAsync(type.MeasurementTypeId);
            t.Name = newName;
            await measurementTypeDao.UpdateAsync(t);

            MeasurementType fetched = await measurementTypeDao.FindByIdAsync(type.MeasurementTypeId);
            Assert.AreEqual(newName, fetched.Name);

        }
    }
}