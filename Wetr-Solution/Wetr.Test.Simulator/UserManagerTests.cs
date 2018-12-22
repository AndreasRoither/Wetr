using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.BusinessLogic;
using Wetr.BusinessLogic.Interface;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.Test.Simulator
{
    [TestClass]
    public class UserManagerTests
    {
        [TestMethod]
        public void TestUserCredentialValidation()
        {
            Mock<IUserDao> dao = new Mock<IUserDao>(MockBehavior.Strict);

            User u = new User()
            {
                Email = "daniel.englisch@outlook.com",
                Password = "$2a$04$edqHEEpJsR.mUlbO1bhI4e21mxXdEeEB1o9gZiJsRazsNtpclw/0y"
            };

            Task<User> result = new Task<User>(() =>
            {
                return u;
            });
            result.RunSynchronously();

            Task<User> nullResult = new Task<User>(() =>
            {
                return null;
            });
            nullResult.RunSynchronously();

            dao.Setup(d => d.FindByEmailAsync(It.IsAny<string>())).Returns(nullResult);
            dao.Setup(d => d.FindByEmailAsync(It.Is<string>((s) => s == "daniel.englisch@outlook.com"))).Returns(result);

            IUserManager m = new UserManager(dao.Object);

            Assert.AreEqual(u, m.UserCredentialValidation(u.Email, "20manue01").Result);
        }

        [TestMethod]
        public void TestCheckUser()
        {
            User invalid = new User()
            {
                Email = "dd",
                Password = "dd",
                FirstName = "",
                LastName = "Englisch"
            };

            User valid = new User()
            {
                Email = "d@e.at",
                Password = "12321412fefe",
                FirstName = "Daniel",
                LastName = "Englisch"
            };

            Mock<IUserDao> dao = new Mock<IUserDao>(MockBehavior.Strict);
            IUserManager m = new UserManager(dao.Object);

            Assert.IsTrue(m.CheckUser(valid));
            Assert.IsFalse(m.CheckUser(invalid));

        }


    }
}
