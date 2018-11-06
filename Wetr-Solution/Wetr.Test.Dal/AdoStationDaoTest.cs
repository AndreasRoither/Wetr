using Common.Dal.Ado;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Wetr.Dal.Ado;
using Wetr.Dal.Interface;

namespace Wetr.Test.Dal
{
    [TestClass]
    public class AdoStationDaoTest : DaoBaseTest
    {
        private readonly IStationDao stationDao = new AdoStationDao(DefaultConnectionFactory.FromConfiguration("MysqlConnection"));


        [TestMethod]
        public async Task TestFindByUserIdAsync()
        {
            throw new System.NotImplementedException();

        }

        [TestMethod]
        public async Task TestFindByStationTypeIdAsync()
        {
            throw new System.NotImplementedException();

        }

        [TestMethod]
        public async Task TestFindByAddressIdAsync()
        {
            throw new System.NotImplementedException();

        }

        [TestMethod]
        public async override Task TestFindByIdAsync()
        {
            throw new System.NotImplementedException();
        }

        [TestMethod]
        public async override Task TestFindAllAsync()
        {
            throw new System.NotImplementedException();
        }

        [TestMethod]
        public async override Task TestUpdateAsync()
        {
            throw new System.NotImplementedException();
        }

        [TestMethod]
        public async override Task TestDeleteAsync()
        {
            throw new System.NotImplementedException();
        }

        [TestMethod]
        public async override Task TestInsertAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}