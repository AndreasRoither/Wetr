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
    public class MeasurementManagerTests
    {
        [TestMethod]
        public void TestGetDashbardTemperaturesAsync()
        {
            Mock<IMeasurementDao> dao = new Mock<IMeasurementDao>(MockBehavior.Loose);
            Mock<IStationDao> stat = new Mock<IStationDao>(MockBehavior.Loose);
            Mock<IAddressManager> mgr = new Mock<IAddressManager>(MockBehavior.Loose);

            Task<Station> statres = new Task<Station>(() => new Station());
            stat.Setup(s => s.FindByIdAsync(It.IsAny<int>())).Returns(statres);

            Task<double[]> result = new Task<double[]>(() => new double[] { 2.3,4.33});
            result.RunSynchronously();

            dao.Setup(d => d.GetDayAverageOfLastXDaysAsync(It.IsAny<int>(), It.IsAny<int>())).Returns(result);
            MeasurementManager m = new MeasurementManager(dao.Object, stat.Object, mgr.Object);
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
            MeasurementManager m = new MeasurementManager(dao.Object, null, null);
            double[] res = m.GetDashboardRainValuesAsync().Result;
            Assert.AreEqual(result.Result, res);
        }

        [TestMethod]
        public void TestAddMeasurement()
        {
            Mock<IMeasurementDao> dao = new Mock<IMeasurementDao>(MockBehavior.Loose);
            Task<bool> result = new Task<bool>(() => true);
            result.RunSynchronously();

            Measurement me = new Measurement()
            {
                MeasurementId = 3,
                MeasurementTypeId = 3,
                TimesStamp = DateTime.Now,
                UnitId = 1,
                Value = 3.4d,
                StationId = 3,
            };

            dao.Setup(d => d.InsertAsync(It.IsAny<Measurement>())).Returns(result);
            MeasurementManager m = new MeasurementManager(dao.Object, null, null);
            var res = m.AddMeasurement(me).Result;
            dao.Verify(d => d.InsertAsync(me), Times.Once);
        }

        [TestMethod]
        public void TestGetNumberOfMeasurementsAsync()
        {
            Mock<IMeasurementDao> dao = new Mock<IMeasurementDao>(MockBehavior.Strict);
            Task<long> result = new Task<long>(() => 100);
            result.RunSynchronously();

            dao.Setup(d => d.GetTotalNumberOfMeasurementsAsync()).Returns(result);
            MeasurementManager m = new MeasurementManager(dao.Object, null, null);
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
            MeasurementManager m = new MeasurementManager(dao.Object, null, null);
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

            MeasurementManager m = new MeasurementManager(dao.Object, null, null);
            double[] res = m.GetQueryResult(DateTime.Now, DateTime.Now, 0, 0, 0, null, new Community()).Result;
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

            MeasurementManager m = new MeasurementManager(dao.Object, null, null);
            double[] res = m.GetQueryResult(DateTime.Now, DateTime.Now, 0, 0, 0, null, 0,0,0).Result;
            Assert.AreEqual(result.Result, res);
        }


        [TestMethod]
        public void TestGetQueryResultDistrict()
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
                It.IsAny<District>()
                )).Returns(result);

            MeasurementManager m = new MeasurementManager(dao.Object, null, null);
            double[] res = m.GetQueryResult(DateTime.Now, DateTime.Now, 0, 0, 0, null, new District()).Result;
            Assert.AreEqual(result.Result, res);
        }

        [TestMethod]
        public void TestGetQueryResultProvince()
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
                It.IsAny<Province>()
                )).Returns(result);

            MeasurementManager m = new MeasurementManager(dao.Object, null, null);
            double[] res = m.GetQueryResult(DateTime.Now, DateTime.Now, 0, 0, 0, null, new Province()).Result;
            Assert.AreEqual(result.Result, res);
        }

        [TestMethod]
        public void TestGetQueryResult()
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
                It.IsAny<List<Station>>()
                )).Returns(result);

            MeasurementManager m = new MeasurementManager(dao.Object, null, null);
            double[] res = m.GetQueryResult(DateTime.Now, DateTime.Now, 0, 0, 0, null).Result;
            Assert.AreEqual(result.Result, res);
        }

    }
}
