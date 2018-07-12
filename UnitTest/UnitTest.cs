using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rewards.Manager.DalContracts;
using Rewards.Manager.Entities;
using Rewards.Manager.Logic;
using Moq;

namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {
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

            Reward reward = new Reward();

            Medal medal = new Medal();

            mock1.Setup(item => item.Add(It.IsAny<Person>())).Returns(person);
            mock2.Setup(item => item.Add(It.IsAny<Reward>())).Returns(reward);
            mock3.Setup(item => item.Add(It.IsAny<Medal>())).Returns(medal);

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
    }
}
