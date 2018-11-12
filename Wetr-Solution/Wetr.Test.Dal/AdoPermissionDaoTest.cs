using Common.Dal.Ado;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Wetr.Dal.Ado;
using Wetr.Dal.Factory;
using Wetr.Dal.Interface;

namespace Wetr.Test.Dal
{
    [TestClass]
    public class AdoPermissionDaoTest : DaoBaseTest
    {

        private static AdoFactory factory = AdoFactory.Instance;
        private static readonly AdoPermissionDao permissionDao = (AdoPermissionDao)factory.GetPermissionDao();

        [TestMethod]
        public async Task TestFindForUserId()
        {
            throw new System.NotImplementedException();

        }

        [TestMethod]
        public async Task TestDeleteForUserId()
        {
            throw new System.NotImplementedException();

        }

        [TestMethod]
        public async Task TestAddForUserId()
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