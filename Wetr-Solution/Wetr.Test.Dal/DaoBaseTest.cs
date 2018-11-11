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

        public abstract Task TestFindByIdAsync();
        public abstract Task TestFindAllAsync();
        public abstract Task TestUpdateAsync();
        public abstract Task TestDeleteAsync();
        public abstract Task TestInsertAsync();
    }
}
