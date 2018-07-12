using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Rewards.Manager.NewConfig;
using Rewards.Manager.LogicContracts;
using Rewards.Manager.Entities;

namespace IntegrationTest
{
    [TestClass]
    public class IntegTest
    {
        [TestMethod]
        public void AddPerson()
        {
            // запуск ninject
            IKernel ninjectKernel = new StandardKernel();
            // регистрация серверов
            Config.RegisterServices(ninjectKernel);
            // создание связанных элементов
            IPersonLogic personLogic = ninjectKernel.Get<IPersonLogic>();

            Person person = new Person
            {
                id = 1,
                Birthday = DateTime.Parse("1996-12-04"),
                Adres = "Homelsee",
                Familia = "Ivanov",
                Imja = "Ivan",
                Vozrast = 21
            };

            Assert.IsNotNull(personLogic.Save(person), "NULL");
        }

        [TestMethod]
        public void AddMedal()
        {
            // запуск ninject
            IKernel ninjectKernel = new StandardKernel();
            // регистрация серверов
            Config.RegisterServices(ninjectKernel);
            // создание связанных элементов
            IMedalLogic medalLogic = ninjectKernel.Get<IMedalLogic>();

            Medal medal = new Medal
            {
                id = 1,
                Name = "Good Boy",
                Material = "Gold"
            };

            Assert.IsNotNull(medalLogic.Save(medal), "NULL");
        }

        [TestMethod]
        public void Rewarding()
        {
            // запуск ninject
            IKernel ninjectKernel = new StandardKernel();
            // регистрация серверов
            Config.RegisterServices(ninjectKernel);
            // создание связанных элементов
            IRewardLogic rewardLogic = ninjectKernel.Get<IRewardLogic>();
            IPersonLogic personLogic = ninjectKernel.Get<IPersonLogic>();
            IMedalLogic medalLogic = ninjectKernel.Get<IMedalLogic>();

            Reward reward = new Reward
            {
                awarded = personLogic.GetById(2),
                medal = medalLogic.GetById(3)
            };

            Assert.IsNotNull(rewardLogic.Save(reward), "NULL");
        }
    }
}
