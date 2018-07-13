using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rewards.Manager.DalContracts;
using Rewards.Manager.Entities;
using Rewards.Manager.Logic;
using Moq;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {
        #region Add
        [TestMethod]
        public void AddMedal()
        {
            var mock = new Mock<IMedalDao>();

            Medal medal = new Medal();

            mock.Setup(item => item.Add(It.IsAny<Medal>())).Returns(medal);

            var logic = new MedalLogic(mock.Object);

            Medal medal2 = new Medal
            {
                id = 1,
                Name = "Bad Boy",
                Material = "Silver"
            };

            var temp = logic.Save(medal2);

            Assert.AreNotEqual(temp, medal);
        }

        [TestMethod]
        public void AddPerson()
        {
            var mock1 = new Mock<IPersonDao>();
            var mock2 = new Mock<IRewardDao>();
            var mock3 = new Mock<IMedalDao>();

            Person person = new Person();

            mock1.Setup(item => item.Add(It.IsAny<Person>())).Returns(person);

            var rewardLogic = new RewardLogic(mock2.Object, mock3.Object, mock1.Object);

            var personLogic = new PersonLogic(mock1.Object, mock3.Object, rewardLogic);

            Person person2 = new Person
            {
                id = 1,
                Birthday = DateTime.Parse("1996-12-04"),
                Adres = "Homelsee",
                Familia = "Дмитриев",
                Imja = "Дмитрий",
                Vozrast = 21
            };

            var temp = personLogic.Save(person2);

            Assert.IsNotNull(temp);
        }

        [TestMethod]
        public void Rewarding()
        {
            var mock1 = new Mock<IPersonDao>();
            var mock2 = new Mock<IRewardDao>();
            var mock3 = new Mock<IMedalDao>();

            Person person = new Person();

            Reward reward = new Reward();

            Medal medal = new Medal();

            mock1.Setup(item => item.Add(It.IsAny<Person>())).Returns(person);
            mock2.Setup(item => item.Add(It.IsAny<Reward>())).Returns(reward);
            mock3.Setup(item => item.Add(It.IsAny<Medal>())).Returns(medal);

            var rewardLogic = new RewardLogic(mock2.Object, mock3.Object, mock1.Object);

            var personLogic = new PersonLogic(mock1.Object, mock3.Object, rewardLogic);

            var medalLogic = new MedalLogic(mock3.Object);

            var temp = rewardLogic.Save(new Reward { awarded = personLogic.GetById(1), medal = medalLogic.GetById(1) });

            Assert.IsNotNull(temp);
        }
        #endregion

        #region GetAll
        [TestMethod]
        public void GetAllMedals()
        {
            var mock = new Mock<IMedalDao>();

            Medal[] medals = new Medal[] { };

            mock.Setup(item => item.GetAll()).Returns(medals);

            var logic = new MedalLogic(mock.Object);

            var result = logic.GetAll();

            Assert.IsNotNull(result, "NULL");
        }

        [TestMethod]
        public void GetAllPersons()
        {
            var mock1 = new Mock<IPersonDao>();
            var mock2 = new Mock<IRewardDao>();
            var mock3 = new Mock<IMedalDao>();

            Person[] person = new Person[] { };

            mock1.Setup(item => item.GetAll()).Returns(person);

            var rewardLogic = new RewardLogic(mock2.Object, mock3.Object, mock1.Object);

            var personLogic = new PersonLogic(mock1.Object, mock3.Object, rewardLogic);

            var temp = personLogic.GetAll();

            Assert.AreNotEqual(temp, person, "Equals");
        }

        [TestMethod]
        public void GetAllRewarding()
        {
            var mock1 = new Mock<IPersonDao>();
            var mock2 = new Mock<IRewardDao>();
            var mock3 = new Mock<IMedalDao>();

            IEnumerable<int[]> resultMock = new List<int[]> { };

            mock2.Setup(item => item.GetAll()).Returns(resultMock);

            var rewardLogic = new RewardLogic(mock2.Object, mock3.Object, mock1.Object);

            var personLogic = new PersonLogic(mock1.Object, mock3.Object, rewardLogic);

            var medalLogic = new MedalLogic(mock3.Object);

            var temp = rewardLogic.GetAll();

            Assert.AreNotEqual(temp, resultMock, "Equals");
        }
        #endregion

        #region Delete
        [TestMethod]
        public void DeleteMedal()
        {
            var mock = new Mock<IMedalDao>();

            bool result = true;

            mock.Setup(item => item.Delete(2)).Returns(result);

            var logic = new MedalLogic(mock.Object);

            var temp = logic.Delete(2);

            Assert.AreEqual(temp, result, "Not Equal");
        }

        [TestMethod]
        public void DeletePerson()
        {
            var mock1 = new Mock<IPersonDao>();
            var mock2 = new Mock<IRewardDao>();
            var mock3 = new Mock<IMedalDao>();

            bool result = true;

            mock1.Setup(item => item.Delete(3)).Returns(result);

            var rewardLogic = new RewardLogic(mock2.Object, mock3.Object, mock1.Object);

            var personLogic = new PersonLogic(mock1.Object, mock3.Object, rewardLogic);

            var temp = personLogic.Delete(3);

            Assert.AreEqual(temp, result, "Not Equal");
        }

        [TestMethod]
        public void DeleteRewarding()
        {
            var mock1 = new Mock<IPersonDao>();
            var mock2 = new Mock<IRewardDao>();
            var mock3 = new Mock<IMedalDao>();

            Person person = new Person();

            bool reward = true;

            Medal medal = new Medal();

            mock2.Setup(item => item.Delete(It.IsAny<Reward>())).Returns(reward);

            var rewardLogic = new RewardLogic(mock2.Object, mock3.Object, mock1.Object);

            var personLogic = new PersonLogic(mock1.Object, mock3.Object, rewardLogic);

            var medalLogic = new MedalLogic(mock3.Object);

            var temp = rewardLogic.Delete(new Reward
            {
                awarded = personLogic.GetById(4),
                medal = medalLogic.GetById(2)
            });

            Assert.AreEqual(temp, reward, "Not Equal");
        }
        #endregion

        #region GetById
        [TestMethod]
        public void GetMedalById()
        {
            var mock = new Mock<IMedalDao>();

            Medal medals = new Medal();

            mock.Setup(item => item.GetById(2)).Returns(medals);

            var logic = new MedalLogic(mock.Object);

            var result = logic.GetById(2);

            Assert.ReferenceEquals(result, medals);
        }

        [TestMethod]
        public void GetPersonById()
        {
            var mock1 = new Mock<IPersonDao>();
            var mock2 = new Mock<IRewardDao>();
            var mock3 = new Mock<IMedalDao>();

            Person person = new Person();

            mock1.Setup(item => item.GetById(4)).Returns(person);

            var rewardLogic = new RewardLogic(mock2.Object, mock3.Object, mock1.Object);

            var personLogic = new PersonLogic(mock1.Object, mock3.Object, rewardLogic);

            var temp = personLogic.GetById(4);

            Assert.ReferenceEquals(temp, person);
        }
        #endregion
    }
}
