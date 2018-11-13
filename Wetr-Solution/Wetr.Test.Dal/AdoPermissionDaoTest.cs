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
    public class AdoPermissionDaoTest : DaoBaseTest
    {

        private static AdoFactory factory = AdoFactory.Instance;
        private static readonly IPermissionDao permissionDao = factory.GetPermissionDao();
        private static readonly IUserDao userDao = factory.GetUserDao();

        private static IList<Permission> permissions = new List<Permission>();
        private static User user;
        private static IList<Permission> userPermissions = new List<Permission>();


        [ClassInitialize]
        public static async Task ClassInitializeAsync(TestContext context)
        {
            Permission p1 = new Permission
            {
                PermissionId = 1,
                Description = "A random permission.",
                Name = "ReadStations"
            };

            Permission p2 = new Permission
            {
                PermissionId = 2,
                Description = "A random permission.",
                Name = "QueryMeasurements"
            };

            Permission p3 = new Permission
            {
                PermissionId = 3,
                Description = "A random permission.",
                Name = "DeleteStations"
            };

            user = new User
            {
                UserId = 99,
                Email = "e@e.at",
                FirstName = "Daniel",
                LastName = "Englisch",
                Password = "dl§idnsins27s28"
            };

            permissions.Add(p1);
            permissions.Add(p2);
            permissions.Add(p3);

            await userDao.InsertAsync(user);

            foreach (var p in permissions)
            {
                await permissionDao.InsertAsync(p);
            }

            userPermissions.Add(p2);
            userPermissions.Add(p3);

            foreach (var p in userPermissions)
            {
                await permissionDao.AddForUserId(p.PermissionId, user.UserId);
            }

        }

        [ClassCleanup]
        public static async Task ClassCleanupAsync()
        {
            foreach (var p in userPermissions)
            {
                await permissionDao.DeleteForUserId(p.PermissionId, user.UserId);
            }

            foreach (Permission p in permissions)
            {
                await permissionDao.DeleteAsync(p.PermissionId);
            }

            await userDao.DeleteAsync(user.UserId);
        }

        [TestMethod]
        public async Task TestFindForUserId()
        {
            IEnumerable<Permission> fetchedPermissions = await permissionDao.FindForUserId(user.UserId);
            foreach (var p in fetchedPermissions)
                CollectionAssert.Contains(userPermissions.ToList(), p);
        }

        [TestMethod]
        public async Task TestDeleteForUserId()
        {
            Permission p = userPermissions[0];
            await permissionDao.DeleteForUserId(p.PermissionId, user.UserId);
            IEnumerable<Permission> fetchedPermissions = await permissionDao.FindForUserId(user.UserId);
            CollectionAssert.DoesNotContain(fetchedPermissions.ToList(), p);
        }

        [TestMethod]
        public async Task TestAddForUserId()
        {
            Permission p = permissions[0];
            await permissionDao.AddForUserId(p.PermissionId, user.UserId);

            IEnumerable<Permission> newPermissions = await permissionDao.FindForUserId(user.UserId);
            CollectionAssert.Contains(newPermissions.ToList(), p);
        }

        [TestMethod]
        public async override Task TestFindByIdAsync()
        {
            Permission p = permissions[2];
            Permission fetched = await permissionDao.FindByIdAsync(p.PermissionId);
            Assert.AreEqual(p, fetched);
        }

        [TestMethod]
        public async override Task TestFindAllAsync()
        {
            IEnumerable<Permission> fetchedPermissions = await permissionDao.FindAllAsync();
            foreach (var p in permissions)
                CollectionAssert.Contains(fetchedPermissions.ToList(), p);
        }

        [TestMethod]
        public async override Task TestUpdateAsync()
        {
            Permission p = permissions[1];
            string newDescription = "New Decription";

            Permission fetched = await permissionDao.FindByIdAsync(p.PermissionId);
            fetched.Description = newDescription;
            await permissionDao.UpdateAsync(fetched);

            fetched = await permissionDao.FindByIdAsync(p.PermissionId);
            Assert.AreEqual(newDescription, fetched.Description);

        }

        [TestMethod]
        public async override Task TestDeleteAsync()
        {
            Permission p = permissions[0];
            await permissionDao.DeleteAsync(p.PermissionId);

            Permission deleted = await permissionDao.FindByIdAsync(p.PermissionId);
            Assert.IsNull(deleted);
        }

        [TestMethod]
        public async override Task TestInsertAsync()
        {
            Permission p = new Permission()
            {
                PermissionId = 4,
                Name = "NAME",
                Description = "Description"
            };

            await permissionDao.InsertAsync(p);

            Permission inserted = await permissionDao.FindByIdAsync(p.PermissionId);
            Assert.AreEqual(p, inserted);
        }
    }
}