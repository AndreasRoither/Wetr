using Common.Dal.Ado;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wetr.Dal.Ado;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.Test.Dal
{
    [TestClass]
    public class AdoUserDaoTest : DaoBaseTest
    {
        private readonly IUserDao adoUserDao = new AdoUserDao(DefaultConnectionFactory.FromConfiguration("MysqlConnection"));

        [TestMethod]
        public override async Task TestDeleteAsync()
        {
            // Setup

            User a = new User
            {
                UserId = 1,
                FirstName = "Daniel",
                LastName = "Englisch",
                Email = "daniel.englisch@outlook.com",
                Password = "SomeHash"
            };

            await adoUserDao.InsertAsync(a);

            // Act

            await adoUserDao.DeleteAsync(a.UserId);

            User b = await adoUserDao.FindByIdAsync(a.UserId);

            // Assert

            Assert.IsNull(b);
        }

        [TestMethod]
        public async override Task TestFindAllAsync()
        {
            // Setup

            User a = new User
            {
                UserId = 1,
                FirstName = "Daniel",
                LastName = "Englisch",
                Email = "daniel.englisch@outlook.com",
                Password = "SomeHash"
            };

            User b = new User
            {
                UserId = 2,
                FirstName = "Stefan",
                LastName = "Granzer",
                Email = "stef.gra@outlook.com",
                Password = "SomeHash"
            };

            User c = new User
            {
                UserId = 3,
                FirstName = "Markus",
                LastName = "Persson",
                Email = "mc.person@outlook.com",
                Password = "SomeHash"
            };

            // Act

            await adoUserDao.InsertAsync(a);
            await adoUserDao.InsertAsync(b);
            await adoUserDao.InsertAsync(c);

            IEnumerable<User> fetchedUsers = await adoUserDao.FindAllAsync();

            // Assert
            CollectionAssert.Contains(fetchedUsers.ToList(), a);
            CollectionAssert.Contains(fetchedUsers.ToList(), b);
            CollectionAssert.Contains(fetchedUsers.ToList(), c);
        }

        [TestMethod]
        public async Task TestFindByEmailAsync()
        {
            // Setup

            User a = new User
            {
                UserId = 1,
                FirstName = "Daniel",
                LastName = "Englisch",
                Email = "daniel.englisch@outlook.com",
                Password = "SomeHash"
            };

            // Act

            await adoUserDao.InsertAsync(a);

            User b = await adoUserDao.FindByEmailAsync(a.Email);

            // Assert

            Assert.AreEqual(a, b);
        }

        [TestMethod]
        public async override Task TestFindByIdAsync()
        {
            // Setup

            User a = new User
            {
                UserId = 1,
                FirstName = "Daniel",
                LastName = "Englisch",
                Email = "daniel.englisch@outlook.com",
                Password = "SomeHash"
            };

            // Act

            await adoUserDao.InsertAsync(a);

            User b = await adoUserDao.FindByIdAsync(a.UserId);

            // Assert

            Assert.AreEqual(b.Email, a.Email);
        }

        [TestMethod]
        public async override Task TestInsertAsync()
        {
            // Setup

            User a = new User
            {
                UserId = 1,
                FirstName = "Daniel",
                LastName = "Englisch",
                Email = "daniel.englisch@outlook.com",
                Password = "SomeHash"
            };

            // Act

            await adoUserDao.InsertAsync(a);

            User b = await adoUserDao.FindByEmailAsync(a.Email);

            // Assert

            Assert.AreEqual(a, b);
        }

        [TestMethod]
        public override async Task TestUpdateAsync()
        {
            // Setup

            User a = new User
            {
                UserId = 1,
                FirstName = "Daniel",
                LastName = "Englisch",
                Email = "daniel.englisch@outlook.com",
                Password = "SomeHash"
            };

            await adoUserDao.InsertAsync(a);

            // Act

            a.Email = "newMail@hagenberg.at";
            await adoUserDao.UpdateAsync(a);

            // Assert
            User b = await adoUserDao.FindByIdAsync(a.UserId);

            Assert.AreEqual(b.Email, "newMail@hagenberg.at");
        }

    }
}