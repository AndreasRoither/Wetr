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
    public class AddressManagerTests
    {
        [TestMethod]
        public void TestGetCountryForAddressId()
        {
            Country country = new Country();
            Province province = new Province();
            District district = new District();
            Community community = new Community();
            Address address  = new Address();

            Mock<IProvinceDao> provinceDao = new Mock<IProvinceDao>(MockBehavior.Loose);
            Task<Province> res1 = new Task<Province>(() => province);
            res1.RunSynchronously();
            provinceDao.Setup(x => x.FindByIdAsync(It.IsAny<int>())).Returns(res1);

            Mock<IDistrictDao> districtDao = new Mock<IDistrictDao>(MockBehavior.Loose);
            Task<District> res2= new Task<District>(() => district);
            res2.RunSynchronously();
            districtDao.Setup(x => x.FindByIdAsync(It.IsAny<int>())).Returns(res2);

            Mock<ICommunityDao> communityDao = new Mock<ICommunityDao>(MockBehavior.Loose);
            Task<Community> res3 = new Task<Community>(() => community);
            res3.RunSynchronously();
            communityDao.Setup(x => x.FindByIdAsync(It.IsAny<int>())).Returns(res3);

            Mock<IAddressDao> addressDao = new Mock<IAddressDao>(MockBehavior.Loose);
            Task<Address> res4 = new Task<Address>(() => address);
            res4.RunSynchronously();
            addressDao.Setup(x => x.FindByIdAsync(It.IsAny<int>())).Returns(res4);

            Mock<ICountryDao> countryDao = new Mock<ICountryDao>(MockBehavior.Loose);
            Task<Country> res5 = new Task<Country>(() => country);
            res5.RunSynchronously();
            countryDao.Setup(x => x.FindByIdAsync(It.IsAny<int>())).Returns(res5);

            AddressManager m = new AddressManager(countryDao.Object, provinceDao.Object, districtDao.Object, communityDao.Object, addressDao.Object);
            Country c = m.GetCountryForAddressId(1).Result;

            provinceDao.Verify(x => x.FindByIdAsync(It.IsAny<int>()), Times.Once);
            addressDao.Verify(x => x.FindByIdAsync(It.IsAny<int>()), Times.Once);
            countryDao.Verify(x => x.FindByIdAsync(It.IsAny<int>()), Times.Once);
            districtDao.Verify(x => x.FindByIdAsync(It.IsAny<int>()), Times.Once);
            communityDao.Verify(x => x.FindByIdAsync(It.IsAny<int>()), Times.Once);

        }

        [TestMethod]
        public void TestGetProvinceForAddressId()
        {
            Province province = new Province();
            District district = new District();
            Community community = new Community();
            Address address = new Address();

            Mock<IProvinceDao> provinceDao = new Mock<IProvinceDao>(MockBehavior.Loose);
            Task<Province> res1 = new Task<Province>(() => province);
            res1.RunSynchronously();
            provinceDao.Setup(x => x.FindByIdAsync(It.IsAny<int>())).Returns(res1);

            Mock<IDistrictDao> districtDao = new Mock<IDistrictDao>(MockBehavior.Loose);
            Task<District> res2 = new Task<District>(() => district);
            res2.RunSynchronously();
            districtDao.Setup(x => x.FindByIdAsync(It.IsAny<int>())).Returns(res2);

            Mock<ICommunityDao> communityDao = new Mock<ICommunityDao>(MockBehavior.Loose);
            Task<Community> res3 = new Task<Community>(() => community);
            res3.RunSynchronously();
            communityDao.Setup(x => x.FindByIdAsync(It.IsAny<int>())).Returns(res3);

            Mock<IAddressDao> addressDao = new Mock<IAddressDao>(MockBehavior.Loose);
            Task<Address> res4 = new Task<Address>(() => address);
            res4.RunSynchronously();
            addressDao.Setup(x => x.FindByIdAsync(It.IsAny<int>())).Returns(res4);


            AddressManager m = new AddressManager(null, provinceDao.Object, districtDao.Object, communityDao.Object, addressDao.Object);
            Province c = m.GetProvinceForAddressId(1).Result;

            provinceDao.Verify(x => x.FindByIdAsync(It.IsAny<int>()), Times.Once);
            addressDao.Verify(x => x.FindByIdAsync(It.IsAny<int>()), Times.Once);
            districtDao.Verify(x => x.FindByIdAsync(It.IsAny<int>()), Times.Once);
            communityDao.Verify(x => x.FindByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void TestGetDistrictForAddressId()
        {
            District district = new District();
            Community community = new Community();
            Address address = new Address();

            Mock<IDistrictDao> districtDao = new Mock<IDistrictDao>(MockBehavior.Loose);
            Task<District> res2 = new Task<District>(() => district);
            res2.RunSynchronously();
            districtDao.Setup(x => x.FindByIdAsync(It.IsAny<int>())).Returns(res2);

            Mock<ICommunityDao> communityDao = new Mock<ICommunityDao>(MockBehavior.Loose);
            Task<Community> res3 = new Task<Community>(() => community);
            res3.RunSynchronously();
            communityDao.Setup(x => x.FindByIdAsync(It.IsAny<int>())).Returns(res3);

            Mock<IAddressDao> addressDao = new Mock<IAddressDao>(MockBehavior.Loose);
            Task<Address> res4 = new Task<Address>(() => address);
            res4.RunSynchronously();
            addressDao.Setup(x => x.FindByIdAsync(It.IsAny<int>())).Returns(res4);


            AddressManager m = new AddressManager(null, null, districtDao.Object, communityDao.Object, addressDao.Object);
            District c = m.GetDistrictForAddressId(1).Result;

            addressDao.Verify(x => x.FindByIdAsync(It.IsAny<int>()), Times.Once);
            districtDao.Verify(x => x.FindByIdAsync(It.IsAny<int>()), Times.Once);
            communityDao.Verify(x => x.FindByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void TestGetCommunityForAddressId()
        {
            Community community = new Community();
            Address address = new Address();          

            Mock<ICommunityDao> communityDao = new Mock<ICommunityDao>(MockBehavior.Loose);
            Task<Community> res3 = new Task<Community>(() => community);
            res3.RunSynchronously();
            communityDao.Setup(x => x.FindByIdAsync(It.IsAny<int>())).Returns(res3);

            Mock<IAddressDao> addressDao = new Mock<IAddressDao>(MockBehavior.Loose);
            Task<Address> res4 = new Task<Address>(() => address);
            res4.RunSynchronously();
            addressDao.Setup(x => x.FindByIdAsync(It.IsAny<int>())).Returns(res4);


            AddressManager m = new AddressManager(null, null, null, communityDao.Object, addressDao.Object);
            Community c = m.GetCommunityForAddressId(1).Result;

            addressDao.Verify(x => x.FindByIdAsync(It.IsAny<int>()), Times.Once);
            communityDao.Verify(x => x.FindByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void TestGetAllCountries()
        {
          
            Mock<ICountryDao> countryDao = new Mock<ICountryDao>(MockBehavior.Loose);
            Task<IEnumerable<Country>> res5 = new Task<IEnumerable<Country>>(() => null);
            res5.RunSynchronously();
            countryDao.Setup(x => x.FindAllAsync()).Returns(res5);

            AddressManager m = new AddressManager(countryDao.Object, null, null, null, null);
            IEnumerable<Country> countries = m.GetAllCountries().Result;
            countryDao.Verify(c => c.FindAllAsync(), Times.Once);

        }

        [TestMethod]
        public void TestGetAllProvinces()
        {
            Mock<IProvinceDao> dao = new Mock<IProvinceDao>(MockBehavior.Loose);
            Task<IEnumerable<Province>> res5 = new Task<IEnumerable<Province>>(() => null);
            res5.RunSynchronously();
            dao.Setup(x => x.FindAllAsync()).Returns(res5);

            AddressManager m = new AddressManager(null, dao.Object, null, null, null);
            IEnumerable<object> os = m.GetAllProvinces().Result;
            dao.Verify(c => c.FindAllAsync(), Times.Once);
        }

        [TestMethod]
        public void TestGetAllDistricts()
        {
            Mock<IDistrictDao> dao = new Mock<IDistrictDao>(MockBehavior.Loose);
            Task<IEnumerable<District>> res5 = new Task<IEnumerable<District>>(() => null);
            res5.RunSynchronously();
            dao.Setup(x => x.FindAllAsync()).Returns(res5);

            AddressManager m = new AddressManager(null, null, dao.Object, null, null);
            IEnumerable<object> os = m.GetAllDistricts().Result;
            dao.Verify(c => c.FindAllAsync(), Times.Once);
        }

        [TestMethod]
        public void TestGetAllCommunities()
        {
            Mock<ICommunityDao> dao = new Mock<ICommunityDao>(MockBehavior.Loose);
            Task<IEnumerable<Community>> res5 = new Task<IEnumerable<Community>>(() => null);
            res5.RunSynchronously();
            dao.Setup(x => x.FindAllAsync()).Returns(res5);

            AddressManager m = new AddressManager(null,  null, null, dao.Object, null);
            IEnumerable<object> os = m.GetAllCommunities().Result;
            dao.Verify(c => c.FindAllAsync(), Times.Once);
        }

        [TestMethod]
        public void TestAddNewAddress()
        {
            Mock<IAddressDao> dao = new Mock<IAddressDao>(MockBehavior.Loose);

            Task<bool> res5 = new Task<bool>(() => true);
            res5.RunSynchronously();
            dao.Setup(x => x.InsertAsync(It.IsAny<Address>())).Returns(res5);

            Task<long> res = new Task<long>(() => 13);
            res.RunSynchronously();
            dao.Setup(x => x.GetNextId()).Returns(res);
            Address address = new Address()
            {
                AddressId=3,
                CommunityId=5,
                Location="AddressxD"
            };

            AddressManager m = new AddressManager(null, null, null,  null, dao.Object);
            long id  = m.AddNewAddress(address).Result;
            Assert.AreEqual(12, id);
            dao.Verify(c => c.GetNextId(), Times.Once);
            dao.Verify(c => c.InsertAsync(address), Times.Once);

        }

     

        [TestMethod]
        public void TestGetAddressStringByAddressId()
        {
            Address address = new Address()
            {
                AddressId = 3,
                CommunityId = 5,
                Location = "AddressxD"
            };

            Mock<IAddressDao> dao = new Mock<IAddressDao>(MockBehavior.Loose);
            Task<Address> res5 = new Task<Address>(() => address);
            res5.RunSynchronously();
            dao.Setup(x => x.FindByIdAsync(It.IsAny<int>())).Returns(res5);

            AddressManager m = new AddressManager(null, null, null, null, dao.Object);
            string a = m.GetAddressStringByAddressId(1).Result;
            dao.Verify(c => c.FindByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void TestGetAddressForId()
        {
            Mock<IAddressDao> dao = new Mock<IAddressDao>(MockBehavior.Loose);
            Task<Address> res5 = new Task<Address>(() => null);
            res5.RunSynchronously();
            dao.Setup(x => x.FindByIdAsync(It.IsAny<int>())).Returns(res5);

            AddressManager m = new AddressManager(null, null, null, null, dao.Object);
            Address a = m.GetAddressForId(1).Result;
            dao.Verify(c => c.FindByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void TestUpdateAddress()
        {
            Mock<IAddressDao> dao = new Mock<IAddressDao>(MockBehavior.Loose);

            Task<bool> res5 = new Task<bool>(() => true);
            res5.RunSynchronously();
            dao.Setup(x => x.UpdateAsync(It.IsAny<Address>())).Returns(res5);
            Address address = new Address()
            {
                AddressId = 3,
                CommunityId = 5,
                Location = "AddressxD"
            };
            AddressManager m = new AddressManager(null, null, null, null, dao.Object);
            bool a = m.UpdateAddress(address).Result;
            dao.Verify(c => c.UpdateAsync(It.IsAny<Address>()), Times.Once);
        }

        [TestMethod]
        public void TestCheckAddress()
        {

            AddressManager m = new AddressManager(null, null, null, null, null);


            Address valid = new Address()
            {
                AddressId = 3,
                CommunityId = 5,
                Location = "AddressxD"
            };

            Address invalid = new Address()
            {
                AddressId = -3,
                Location = "AddressxD"
            };

            Assert.IsTrue(m.CheckAddress(valid));
            Assert.IsFalse(m.CheckAddress(invalid));

        }
    }
}
