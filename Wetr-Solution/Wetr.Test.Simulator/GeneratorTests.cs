using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Wetr.BusinessLogic;
using Wetr.Dal.Factory;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.Test.Simulator
{
    [TestClass]
    public class GeneratorTests
    {
        [TestMethod]
        public void TestLinearAscGeneration()
        {

            Station s = new Station()
            {
                StationId = 1,
                Name = "MyStation",
                AddressId = 1,
                Latitude = 1.2m,
                Longitude = -4.3m,
                StationTypeId = 2,
                UserId = 2
            };

            Preset p = new Preset()
            {
                Frequency = Frequency.Second,
                Distribution = Distribution.LinearAsc,
                StartDate = DateTime.Parse("1/1/2000 12:00:00"),
                EndDate = DateTime.Parse("1/1/2000 12:00:10"),
                Id = 1,
                MinValue = -10,
                MaxValue = 10,
                MeasurementType = new MeasurementType() { MeasurementTypeId = 1, Name = "Temperatur" },
                Name = "MyPreset"
            };

            List<Preset> presets = new List<Preset>();
            p.Stations.Add(s);
            presets.Add(p);

            Mock<IMeasurementDao> dao = new Mock<IMeasurementDao>(MockBehavior.Strict);
            Task<bool> t = new Task<bool>(() => true);
            t.RunSynchronously();
            dao.Setup(d => d.InsertAsync(It.IsAny<Measurement>())).Returns(t);

            Generator gen = new Generator(dao.Object);
        
            while (p.CurrentDate <= p.EndDate)
            {
                gen.Generate(presets, Frequency.Second);
            }

            List<double> values = new List<double>();
            double last = 0;
            bool first = true;
            foreach (Measurement m in p.GeneratedData[s])
            {
                if (!first)
                {
                    Assert.IsTrue(last <= m.Value);
                }

                last = m.Value;
                first = false;
                values.Add(m.Value);
            }

            for (int i = -10; i <= 10; i += 2)
            {
                CollectionAssert.Contains(values, (double)i);
            }

            dao.Verify(d => d.InsertAsync(It.IsAny<Measurement>()), Times.Exactly(11));
        }

        [TestMethod]
        public void TestLinearDescGeneration()
        {

            Station s = new Station()
            {
                StationId = 1,
                Name = "MyStation",
                AddressId = 1,
                Latitude = 1.2m,
                Longitude = -4.3m,
                StationTypeId = 2,
                UserId = 2
            };

            Preset p = new Preset()
            {
                Frequency = Frequency.Second,
                Distribution = Distribution.LinearDesc,
                StartDate = DateTime.Parse("1/1/2000 12:00:00"),
                EndDate = DateTime.Parse("1/1/2000 12:00:10"),
                Id = 1,
                MinValue = -10,
                MaxValue = 10,
                MeasurementType = new MeasurementType() { MeasurementTypeId = 1, Name = "Temperatur" },
                Name = "MyPreset"
            };

            List<Preset> presets = new List<Preset>();
            p.Stations.Add(s);
            presets.Add(p);

            Mock<IMeasurementDao> dao = new Mock<IMeasurementDao>(MockBehavior.Strict);
            Task<bool> t = new Task<bool>(() => true);
            t.RunSynchronously();
            dao.Setup(d => d.InsertAsync(It.IsAny<Measurement>())).Returns(t);
            Generator gen = new Generator(dao.Object);

            while (p.CurrentDate <= p.EndDate)
            {
                gen.Generate(presets, Frequency.Second);
            }

            List<double> values = new List<double>();
            double last = 0;
            bool first = true;
            foreach (Measurement m in p.GeneratedData[s])
            {
                if (!first)
                {
                    Assert.IsTrue(last >= m.Value);
                }
                first = false;
                last = m.Value;

                values.Add(m.Value);
            }

            for (int i = -10; i <= 10; i += 2)
            {
                CollectionAssert.Contains(values, (double)i);
            }

            dao.Verify(d => d.InsertAsync(It.IsAny<Measurement>()), Times.Exactly(11));


        }

        [TestMethod]
        public void TestRandomGeneration()
        {

            Station s = new Station()
            {
                StationId = 1,
                Name = "MyStation",
                AddressId = 1,
                Latitude = 1.2m,
                Longitude = -4.3m,
                StationTypeId = 2,
                UserId = 2
            };

            Preset p = new Preset()
            {
                Frequency = Frequency.Second,
                Distribution = Distribution.Random,
                StartDate = DateTime.Parse("1/1/2000 12:00:00"),
                EndDate = DateTime.Parse("1/1/2000 12:00:10"),
                Id = 1,
                MinValue = -10,
                MaxValue = 10,
                MeasurementType = new MeasurementType() { MeasurementTypeId = 1, Name = "Temperatur" },
                Name = "MyPreset"
            };

            Mock<IMeasurementDao> dao = new Mock<IMeasurementDao>(MockBehavior.Strict);
            Task<bool> t = new Task<bool>(() => true);
            t.RunSynchronously();
            dao.Setup(d => d.InsertAsync(It.IsAny<Measurement>())).Returns(t);

            List<Preset> presets = new List<Preset>();
            p.Stations.Add(s);
            presets.Add(p);

            Generator gen = new Generator(dao.Object);

            while (p.CurrentDate <= p.EndDate)
            {
                gen.Generate(presets, Frequency.Second);
            }

            foreach (Measurement m in p.GeneratedData[s])
            {
                Assert.IsTrue(m.Value >= -10 && m.Value < 10);
                Console.WriteLine(m.Value);
            }
            dao.Verify(d => d.InsertAsync(It.IsAny<Measurement>()), Times.Exactly(11));


        }

        [TestMethod]
        public void TestRealisticTemperature()
        {

            Station s = new Station()
            {
                StationId = 1,
                Name = "MyStation",
                AddressId = 1,
                Latitude = 1.2m,
                Longitude = -4.3m,
                StationTypeId = 2,
                UserId = 2
            };

            Preset p = new Preset()
            {
                Frequency = Frequency.Hour,
                Distribution = Distribution.RealisticTemp,
                StartDate = DateTime.Parse("1/1/2000 00:00:00"),
                EndDate = DateTime.Parse("1/1/2000 23:00:00"),
                Id = 1,
                MinValue = 0,
                MaxValue = 12,
                MeasurementType = new MeasurementType() { MeasurementTypeId = 1, Name = "Temperatur" },
                Name = "MyPreset"
            };

            List<Preset> presets = new List<Preset>();
            p.Stations.Add(s);
            presets.Add(p);

            Mock<IMeasurementDao> dao = new Mock<IMeasurementDao>(MockBehavior.Strict);
            Task<bool> t = new Task<bool>(() => true);
            t.RunSynchronously();
            dao.Setup(d => d.InsertAsync(It.IsAny<Measurement>())).Returns(t);

            Generator gen = new Generator(dao.Object);

            while (p.CurrentDate <= p.EndDate)
            {
                gen.Generate(presets, Frequency.Hour);
            }

            List<double> vormittag = new List<double>();
            List<double> nachmittag = new List<double>();

            foreach (Measurement m in p.GeneratedData[s])
            {

                if (m.TimesStamp.Hour < 12)
                {
                    vormittag.Add(m.Value);
                }
                else
                {
                    nachmittag.Add(m.Value);
                }

            }

            bool first = true;
            double last = 0;
            foreach (double b in vormittag)
            {
                if (!first)
                {
                    Assert.IsTrue(last <= b);
                }

                last = b;
                first = false;
            }

            first = true;
            last = 0;
            foreach (double b in nachmittag)
            {
                if (!first)
                {
                    Assert.IsTrue(last >= b);
                }

                last = b;
                first = false;
            }


            dao.Verify(d => d.InsertAsync(It.IsAny<Measurement>()), Times.Exactly(24));


        }
    }
}
