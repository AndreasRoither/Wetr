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
    public class StationManagerTests
    {

        [TestMethod]
        public void TestGetAllStations()
        {
            Station s = new Station()
            {
                AddressId = 1,
                Latitude = 1.2m,
                Longitude = 2.3m,
                Name = "MyStation",
                UserId = 4,
                StationId = 3,
                StationTypeId = 2
            };

            Mock<IStationDao> dao = new Mock<IStationDao>(MockBehavior.Strict);
            Task<IEnumerable<Station>> result = new Task<IEnumerable<Station>>(() => new List<Station>()
            {
                s
            });
            result.RunSynchronously();

            dao.Setup(d => d.FindAllAsync()).Returns(result);

            StationManager m = new StationManager(dao.Object, null, null);
            IEnumerable<Station> stations = m.GetAllStations().Result;

            CollectionAssert.Contains(stations.ToList(), s);
            dao.Verify(d => d.FindAllAsync(), Times.Once);

        }

        [TestMethod]
        public void TestGetStationsForUser()
        {
            Station s = new Station()
            {
                AddressId = 1,
                Latitude = 1.2m,
                Longitude = 2.3m,
                Name = "MyStation",
                UserId = 4,
                StationId = 3,
                StationTypeId = 2
            };

            Mock<IStationDao> dao = new Mock<IStationDao>(MockBehavior.Strict);
            Task<IEnumerable<Station>> result = new Task<IEnumerable<Station>>(() => new List<Station>()
            {
                s
            });
            result.RunSynchronously();

            dao.Setup(d => d.FindByUserIdAsync(It.IsAny<int>())).Returns(result);

            StationManager m = new StationManager(dao.Object, null, null);
            IEnumerable<Station> stations = m.GetStationsForUser(1).Result;

            CollectionAssert.Contains(stations.ToList(), s);
            dao.Verify(d => d.FindByUserIdAsync(1), Times.Once);
        }

        [TestMethod]
        public void TestUpdateStation()
        {
            Mock<IStationDao> dao = new Mock<IStationDao>(MockBehavior.Strict);
            Task<bool> result = new Task<bool>(() => true);
            result.RunSynchronously();

            dao.Setup(d => d.UpdateAsync(It.IsAny<Station>())).Returns(result);

            StationManager m = new StationManager(dao.Object, null, null);

            Station s = new Station()
            {
                AddressId = 1,
                Latitude = 1.2m,
                Longitude = 2.3m,
                Name = "MyStation",
                UserId = 4,
                StationId = 3,
                StationTypeId = 2
            };

            bool res = m.UpdateStation(s).Result;
            Assert.IsTrue(res);
            dao.Verify(d => d.UpdateAsync(s), Times.Once);
        }

        [TestMethod]
        public void TestDeleteStation()
        {
            Mock<IStationDao> dao = new Mock<IStationDao>(MockBehavior.Strict);
            Task<bool> result = new Task<bool>(() => true);
            result.RunSynchronously();

            dao.Setup(d => d.DeleteAsync(It.IsAny<int>())).Returns(result);

            StationManager m = new StationManager(dao.Object, null, null);

            Station s = new Station()
            {
                AddressId = 1,
                Latitude = 1.2m,
                Longitude = 2.3m,
                Name = "MyStation",
                UserId = 4,
                StationId = 3,
                StationTypeId = 2
            };

            bool res = m.DeleteStation(s).Result;
            Assert.IsTrue(res);
            dao.Verify(d => d.DeleteAsync(s.StationId), Times.Once);
        }

        [TestMethod]
        public void TestHasMeasurements()
        {
            Mock<IMeasurementDao> dao = new Mock<IMeasurementDao>(MockBehavior.Strict);
            Task<IEnumerable<Measurement>> result = new Task<IEnumerable<Measurement>>(() => new List<Measurement>());
            result.RunSynchronously();

            dao.Setup(d => d.FindByStationIdAsync(It.IsAny<int>())).Returns(result);

            StationManager m = new StationManager(null, null, dao.Object);

            Station s = new Station()
            {
                AddressId = 1,
                Latitude = 1.2m,
                Longitude = 2.3m,
                Name = "MyStation",
                UserId = 4,
                StationId = 3,
                StationTypeId = 2
            };

            bool res = m.HasMeasurements(s).Result;
            Assert.IsFalse(res);
            dao.Verify(d => d.FindByStationIdAsync(s.StationId), Times.Once);
        }

        [TestMethod]
        public void TestAddStation()
        {
            Mock<IStationDao> dao = new Mock<IStationDao>(MockBehavior.Strict);
            Task<bool> result = new Task<bool>(() => true);
            result.RunSynchronously();

            dao.Setup(d => d.InsertAsync(It.IsAny<Station>())).Returns(result);

            StationManager m = new StationManager(dao.Object, null, null);

            Station s = new Station()
            {
                AddressId = 1,
                Latitude = 1.2m,
                Longitude = 2.3m,
                Name = "MyStation",
                UserId = 4,
                StationId = 3,
                StationTypeId = 2
            };

            bool res = m.AddStation(s).Result;
            Assert.IsTrue(res);
            dao.Verify(d => d.InsertAsync(s), Times.Once);
        }

        [TestMethod]
        public void TestGetNumberOfStations()
        {

            Mock<IStationDao> dao = new Mock<IStationDao>(MockBehavior.Strict);
            Task<long> result = new Task<long>(() => 10);
            result.RunSynchronously();

            dao.Setup(d => d.GetTotalNumberOfStationsAsync()).Returns(result);

            StationManager m = new StationManager(dao.Object, null, null);
            long stations = m.GetNumberOfStations().Result;

            dao.Verify(d => d.GetTotalNumberOfStationsAsync(), Times.Once);
            Assert.AreEqual(10, stations);

        }

        [TestMethod]
        public void TestGetStationTypes()
        {
            StationType s = new StationType()
            {
                Name = "MyType",
                StationTypeId = 2
            };

            Mock<IStationTypeDao> dao = new Mock<IStationTypeDao>(MockBehavior.Strict);
            Task<IEnumerable<StationType>> result = new Task<IEnumerable<StationType>>(() => new List<StationType>()
            {
                s
            });
            result.RunSynchronously();

            dao.Setup(d => d.FindAllAsync()).Returns(result);

            StationManager m = new StationManager(null, dao.Object, null);
            IEnumerable<StationType> stationTypes = m.GetStationTypes().Result;

            CollectionAssert.Contains(stationTypes.ToList(), s);
            dao.Verify(d => d.FindAllAsync(), Times.Once);

        }

        [TestMethod]
        public void TestGetStationTypesForStationTypeId()
        {
            StationType s = new StationType()
            {
                Name = "MyType",
                StationTypeId = 2
            };

            Mock<IStationTypeDao> dao = new Mock<IStationTypeDao>(MockBehavior.Strict);
            Task<StationType> result = new Task<StationType>(() => s);
            result.RunSynchronously();

            dao.Setup(d => d.FindByIdAsync(It.IsAny<int>())).Returns(result);

            StationManager m = new StationManager(null, dao.Object, null);
            StationType stationType = m.GetStationTypesForStationTypeId(s.StationTypeId).Result;

            Assert.AreEqual(s, stationType);
            dao.Verify(d => d.FindByIdAsync(s.StationTypeId), Times.Once);
        }

        [TestMethod]
        public void TestCheckStation()
        {
            Station valid = new Station()
            {
                AddressId = 1,
                Latitude = 46.3m,
                Longitude = 13.4m,
                Name = "MyStation",
                UserId = 4,
                StationId = 3,
                StationTypeId = 2
            };

            Station invalid = new Station()
            {
                AddressId = 1,
                Latitude = 46.3m,
                Longitude = 13.4m,
                UserId = 4,
                StationId = 3,
            };

            StationManager m = new StationManager(null, null, null);
            Assert.IsTrue(m.CheckStation(valid));
            Assert.IsFalse(m.CheckStation(invalid));

        }
    }
}
