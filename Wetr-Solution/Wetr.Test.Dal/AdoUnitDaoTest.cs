using System.Threading.Tasks;
using Common.Dal.Ado;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wetr.Dal.Ado;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.Test.Dal
{
    [TestClass]
    public class AdoUnitDaoTest : DaoBaseTest
    {
        private readonly IUnitDao unitDao = new AdoUnitDao(DefaultConnectionFactory.FromConfiguration("MysqlConnection"));

        [TestMethod]
        public async override Task TestDeleteAsync()
        {
            throw new System.NotImplementedException();
        }

        [TestMethod]
        public async override Task TestFindAllAsync()
        {
            throw new System.NotImplementedException();
        }

        [TestMethod]
        public async override Task TestFindByIdAsync()
        {
            throw new System.NotImplementedException();
        }

        [TestMethod]
        public async override Task TestInsertAsync()
        {
            Unit unit = new Unit
            {
                UnitId = 0,
                Name = "Celsius"
            };

            await unitDao.InsertAsync(unit);

            Unit insertedUnit = await unitDao.FindByIdAsync(0);

            Assert.AreEqual(unit, insertedUnit);
        }

        [TestMethod]
        public async override Task TestUpdateAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}