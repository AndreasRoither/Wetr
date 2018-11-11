using System.Collections.Generic;
using System.Linq;
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
        private readonly IUnitDao unitDao = new AdoUnitDao(DefaultConnectionFactory.FromConfiguration("WETR-Testing"));

        [TestMethod]
        public async override Task TestDeleteAsync()
        {
            Unit unit = new Unit
            {
                UnitId = 1,
                Name = "Celsius"
            };

            await unitDao.InsertAsync(unit);

            Unit insertedUnit = await unitDao.FindByIdAsync(1);

            Assert.AreEqual(unit, insertedUnit);

            await unitDao.DeleteAsync(unit.UnitId);

            insertedUnit = await unitDao.FindByIdAsync(1);

            Assert.IsNull(insertedUnit);

        }

        [TestMethod]
        public async override Task TestFindAllAsync()
        {
            Unit unit1 = new Unit
            {
                UnitId = 1,
                Name = "Celsius"
            };

            Unit unit2 = new Unit
            {
                UnitId = 2,
                Name = "Fahrenheit"
            };

            Unit unit3 = new Unit
            {
                UnitId = 3,
                Name = "Kelvin"
            };

            await unitDao.InsertAsync(unit1);
            await unitDao.InsertAsync(unit2);
            await unitDao.InsertAsync(unit3);


            IEnumerable<Unit> units = await unitDao.FindAllAsync();

            CollectionAssert.Contains(units.ToList(), unit1);
            CollectionAssert.Contains(units.ToList(), unit2);
            CollectionAssert.Contains(units.ToList(), unit3);
        }

        [TestMethod]
        public async override Task TestFindByIdAsync()
        {
            Unit unit = new Unit
            {
                UnitId = 1,
                Name = "Celsius"
            };

            await unitDao.InsertAsync(unit);

            Unit insertedUnit = await unitDao.FindByIdAsync(1);

            Assert.AreEqual(unit, insertedUnit);
        }

        [TestMethod]
        public async override Task TestInsertAsync()
        {
            Unit unit = new Unit
            {
                UnitId = 1,
                Name = "Celsius"
            };

            await unitDao.InsertAsync(unit);

            Unit insertedUnit = await unitDao.FindByIdAsync(1);

            Assert.AreEqual(unit, insertedUnit);
        }

        [TestMethod]
        public async override Task TestUpdateAsync()
        {
            Unit unit = new Unit
            {
                UnitId = 1,
                Name = "Celsius"
            };

            await unitDao.InsertAsync(unit);

            unit.Name = "Fahrenheit";

            await unitDao.UpdateAsync(unit);

            Unit updatedUnit = await unitDao.FindByIdAsync(1);

            Assert.AreEqual("Fahrenheit", updatedUnit.Name);
        }
    }
}