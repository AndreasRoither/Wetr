using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Wetr.Test.Dal
{
    public abstract class DaoBaseTest
    {
        private TransactionScope transaction;

        [TestInitialize]
        public void Setup()
        {
            transaction = new TransactionScope();
        }

        [TestCleanup]
        public void Cleanup()
        {
            transaction.Dispose();
        }

        [TestMethod]
        public abstract Task TestFindByIdAsync();

        [TestMethod]
        public abstract Task TestFindAllAsync();

        [TestMethod]
        public abstract Task TestUpdateAsync();

        [TestMethod]
        public abstract Task TestDeleteAsync();

        [TestMethod]
        public abstract Task TestInsertAsync();
    }
}
