using System.Threading.Tasks;
using Common.Dal.Ado;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wetr.Dal.Ado;
using Wetr.Dal.Interface;

namespace Wetr.Test.Dal
{
    [TestClass]
    public class AdoCountryDaoTest : DaoBaseTest
    {
        private readonly ICountryDao countryDao = new AdoCountryDao(DefaultConnectionFactory.FromConfiguration("MysqlConnection"));

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
            throw new System.NotImplementedException();
        }

        [TestMethod]
        public async override Task TestUpdateAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}