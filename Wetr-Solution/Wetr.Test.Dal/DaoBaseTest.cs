using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wetr.Test.Dal
{
    public abstract class DaoBaseTest
    {
        public abstract Task TestFindByIdAsync();

        public abstract Task TestFindAllAsync();

        public abstract Task TestUpdateAsync();

        public abstract Task TestDeleteAsync();

        public abstract Task TestInsertAsync();
    }
}
