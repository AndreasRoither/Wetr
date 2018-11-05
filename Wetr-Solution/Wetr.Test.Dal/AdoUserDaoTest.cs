using Common.Dal.Ado;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Transactions;
using Wetr.Dal.Ado;
using Wetr.Dal.Interface;

namespace Wetr.Test.Dal
{
    [TestClass]
    public class AdoUserDaoTest : DaoBaseTest
    {
        private readonly IUserDao asoUserDao = new AdoUserDao(DefaultConnectionFactory.FromConfiguration("MysqlConnection"));

        public override Task TestDeleteAsync()
        {
            throw new System.NotImplementedException();
        }

        [TestMethod]
        public async override Task TestFindAllAsync()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var users = await asoUserDao.FindAllAsync();
                foreach (var u in users)
                {
                    System.Console.WriteLine(u);
                }
            }
        }

        [TestMethod]
        public void TestFindByEmailAsync()
        {
        }

        [TestMethod]
        public override Task TestFindByIdAsync()
        {
            throw new System.NotImplementedException();
        }

        [TestMethod]
        public override Task TestInsertAsync()
        {
            throw new System.NotImplementedException();
        }

        [TestMethod]
        public override Task TestUpdateAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}