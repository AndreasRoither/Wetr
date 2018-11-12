using Common.Dal.Ado;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wetr.Dal.Ado;
using Wetr.Dal.Factory;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.Test.Dal
{
    [TestClass]
    public class AdoUserDaoTest : DaoBaseTest
    {
        private static AdoFactory factory = AdoFactory.Instance;
        private static readonly IUserDao adoUserDao = factory.GetUserDao();
        private static IList<User> users; 

        [ClassInitialize]
        public static async Task ClassInitializeAsync(TestContext context)
        {

            users = new List<User>();

            User a = new User
            {
                UserId = 2,
                FirstName = "Daniel",
                LastName = "Englisch",
                Email = "daniel.englisch@outlook.com",
                Password = "SomeHash"
            };

            User b = new User
            {
                UserId = 3,
                FirstName = "Stefan",
                LastName = "Granzer",
                Email = "stef.gra@outlook.com",
                Password = "SomeHash"
            };

            User c = new User
            {
                UserId = 4,
                FirstName = "Markus",
                LastName = "Persson",
                Email = "mc.person@outlook.com",
                Password = "SomeHash"
            };

            users.Add(a);
            users.Add(b);
            users.Add(c);

            foreach (User u in users)
                await adoUserDao.InsertAsync(u);

        }

        [ClassCleanup]
        public static async Task ClassCleanupAsync()
        {
            foreach (User u in users)
                await adoUserDao.DeleteAsync(u.UserId);
        }

        [TestMethod]
        public override async Task TestDeleteAsync()
        {
            await adoUserDao.DeleteAsync(3);
            User u = await adoUserDao.FindByIdAsync(3);
            Assert.IsNull(u);
        }

        [TestMethod]
        public async override Task TestFindAllAsync()
        {
            IEnumerable<User> fetchedUsers = await adoUserDao.FindAllAsync();
            foreach (User u in users)
                CollectionAssert.Contains(fetchedUsers.ToList(), u);

        }

        [TestMethod]
        public async Task TestFindByEmailAsync()
        {
            User u = await adoUserDao.FindByEmailAsync("mc.person@outlook.com");
            Assert.IsNotNull(u);
            Assert.AreEqual(4, u.UserId);
        }

        [TestMethod]
        public async override Task TestFindByIdAsync()
        {
            User u = await adoUserDao.FindByIdAsync(2);
            Assert.AreEqual("daniel.englisch@outlook.com", u.Email);
        }

        [TestMethod]
        public async override Task TestInsertAsync()
        {
            User d = new User
            {
                UserId = 5,
                FirstName = "Anna",
                LastName = "Losbichler",
                Email = "anna.l@gmx.com",
                Password = "SomeHash"
            };

            await adoUserDao.InsertAsync(d);

            User u = await adoUserDao.FindByIdAsync(5);

            Assert.AreEqual(d,u);
        }

        [TestMethod]
        public override async Task TestUpdateAsync()
        {
            string newEmail = "new@email.at";

            User u = await adoUserDao.FindByIdAsync(2);
            u.Email = newEmail;
            await adoUserDao.UpdateAsync(u);

            u = await adoUserDao.FindByIdAsync(2);

            Assert.AreEqual(newEmail, u.Email);
        }

    }
}