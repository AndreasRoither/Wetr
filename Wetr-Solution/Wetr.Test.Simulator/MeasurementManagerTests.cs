using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.BusinessLogic;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.Test.Simulator
{
    [TestClass]
    public class MeasurementManagerTests
    {
        [TestMethod]
        public void TestGetDashbardTemperaturesAsync()
        {
            Mock<IMeasurementDao> dao = new Mock<IMeasurementDao>(MockBehavior.Strict);
            Task<double[]> result = new Task<double[]>(() => new double[] { 2.3,4.33});
            result.RunSynchronously();

            dao.Setup(d => d.GetDayAverageOfLastXDaysAsync(It.IsAny<int>(), It.IsAny<int>())).Returns(result);
            MeasurementManager m = new MeasurementManager(dao.Object);
            double[] res = m.GetDashbardTemperaturesAsync().Result;
            Assert.AreEqual(result.Result, res);

        }

        [TestMethod]
        public void TestGetDashboardRainValuesAsync()
        {
            Mock<IMeasurementDao> dao = new Mock<IMeasurementDao>(MockBehavior.Strict);
            Task<double[]> result = new Task<double[]>(() => new double[] { 2.3, 4.33 });
            result.RunSynchronously();

            dao.Setup(d => d.GetDayAverageOfLastXDaysAsync(It.IsAny<int>(), It.IsAny<int>())).Returns(result);
            MeasurementManager m = new MeasurementManager(dao.Object);
            double[] res = m.GetDashboardRainValuesAsync().Result;
            Assert.AreEqual(result.Result, res);
        }

        [TestMethod]
        public void TestGetNumberOfMeasurementsAsync()
        {
            Mock<IMeasurementDao> dao = new Mock<IMeasurementDao>(MockBehavior.Strict);
            Task<long> result = new Task<long>(() => 100);
            result.RunSynchronously();

            dao.Setup(d => d.GetTotalNumberOfMeasurementsAsync()).Returns(result);
            MeasurementManager m = new MeasurementManager(dao.Object);
            long res = m.GetNumberOfMeasurementsAsync().Result;
            Assert.AreEqual(result.Result, res);
        }

        [TestMethod]
        public void TestGetNumberOfMeasurementsOfWeekAsync()
        {
            Mock<IMeasurementDao> dao = new Mock<IMeasurementDao>(MockBehavior.Strict);
            Task<long> result = new Task<long>(() => 100);
            result.RunSynchronously();

            dao.Setup(d => d.GetNumberOfMeasurementsFromTheLastXDaysAsync(It.IsAny<int>())).Returns(result);
            MeasurementManager m = new MeasurementManager(dao.Object);
            long res = m.GetNumberOfMeasurementsOfWeekAsync().Result;
            Assert.AreEqual(result.Result, res);
        }

        [TestMethod]
        public void TestGetQueryResultCommunity()
        {
            Mock<IMeasurementDao> dao = new Mock<IMeasurementDao>(MockBehavior.Strict);
            Task<double[]> result = new Task<double[]>(() => new double[] { 2.3, 4.33 });
            result.RunSynchronously();
            dao.Setup(d => d.GetQueryResult(
                It.IsAny<DateTime>(),
                It.IsAny<DateTime>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<List<Station>>(),
                It.IsAny<Community>()
                )).Returns(result);

            MeasurementManager m = new MeasurementManager(dao.Object);
            double[] res = m.GetQueryResult(DateTime.Now, DateTime.Now, 0, 0, 0, null, null).Result;
            Assert.AreEqual(result.Result, res);
        }

        [TestMethod]
        public void TestGetQueryResultLatLon()
        {
            Mock<IMeasurementDao> dao = new Mock<IMeasurementDao>(MockBehavior.Strict);
            Task<double[]> result = new Task<double[]>(() => new double[] { 2.3, 4.33 });
            result.RunSynchronously();

            dao.Setup(d => d.GetQueryResult(
                It.IsAny<DateTime>(),
                It.IsAny<DateTime>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<List<Station>>(),
                It.IsAny<decimal>(),
                 It.IsAny<decimal>(),
                It.IsAny<int>()

                )).Returns(result);

            MeasurementManager m = new MeasurementManager(dao.Object);
            double[] res = m.GetQueryResult(DateTime.Now, DateTime.Now, 0, 0, 0, null, 0,0,0).Result;
            Assert.AreEqual(result.Result, res);
        }
    }
}
