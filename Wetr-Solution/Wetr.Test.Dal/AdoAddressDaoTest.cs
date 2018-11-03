using Common.Dal.Ado;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Transactions;
using Wetr.Dal.Ado;
using Wetr.Dal.Interface;


namespace Wetr.Test.Dal
{
    [TestClass]
    public class AdoAddressDaoTest : DaoBaseTest
    {
        private readonly IAddressDao adoAddressDao = new AdoAddressDao(DefaultConnectionFactory.FromConfiguration("MysqlConnection"));

        [TestMethod]
        public void FindByCommunityIdAsync()
        {
        }
        [TestMethod]
        public override Task TestDeleteAsync()
        {
            throw new System.NotImplementedException();
        }

        [TestMethod]
        public override async Task TestFindAllAsync()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var addresses = await adoAddressDao.FindAllAsync();
                foreach (var a in addresses)
                {
                    System.Console.WriteLine(a);
                }
            }
           
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