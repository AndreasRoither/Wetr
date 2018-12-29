using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wetr.Web.BL;
using Wetr.Web.Responses;

namespace Wetr.Test.Web
{
    [TestClass]
    public class JwtHelperTests
    {
        [TestMethod]
        public void TestGenerate()
        {
            TokenResponse resp = JwtHelper.Instance.Generate(1);
            Assert.IsNotNull(resp.Token);
        }

        [TestMethod]
        public void TestValidate()
        {
            TokenResponse resp = JwtHelper.Instance.Generate(1);
            Assert.IsTrue(JwtHelper.Instance.IsValid(resp.Token));
        }

        [TestMethod]
        public void TestGetUserId()
        {
            TokenResponse resp = JwtHelper.Instance.Generate(1);

            int readId = JwtHelper.Instance.GetUserId(resp.Token);
            Assert.AreEqual(1, readId);
        }
    }
}
