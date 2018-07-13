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
        #region Add
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

            Assert.IsNotNull(personLogic.GetById(3), "Person is NULL");

            Assert.IsNotNull(medalLogic.GetById(4), "Medal is NULL");

            Reward reward = new Reward
            {
                awarded = personLogic.GetById(3),
                medal = medalLogic.GetById(4)
            };

            Assert.IsNotNull(rewardLogic.Save(reward), "NULL");
        }
        #endregion

        #region GetAll
        [TestMethod]
        public void GetAllMedal()
        {
            // запуск ninject
            IKernel ninjectKernel = new StandardKernel();
            // регистрация серверов
            Config.RegisterServices(ninjectKernel);
            // создание связанных элементов
            IRewardLogic rewardLogic = ninjectKernel.Get<IRewardLogic>();
            IPersonLogic personLogic = ninjectKernel.Get<IPersonLogic>();
            IMedalLogic medalLogic = ninjectKernel.Get<IMedalLogic>();

            var result = medalLogic.GetAll();

            Assert.IsNotNull(result, "ERROR");
        }

        [TestMethod]
        public void GetAllPerson()
        {
            // запуск ninject
            IKernel ninjectKernel = new StandardKernel();
            // регистрация серверов
            Config.RegisterServices(ninjectKernel);
            // создание связанных элементов
            IRewardLogic rewardLogic = ninjectKernel.Get<IRewardLogic>();
            IPersonLogic personLogic = ninjectKernel.Get<IPersonLogic>();
            IMedalLogic medalLogic = ninjectKernel.Get<IMedalLogic>();

            var result = personLogic.GetAll();

            Assert.AreNotEqual(result.Length, 0, "Result is Empty");

            Assert.IsNotNull(result, "Null");
        }

        [TestMethod]
        public void GetAllReward()
        {
            // запуск ninject
            IKernel ninjectKernel = new StandardKernel();
            // регистрация серверов
            Config.RegisterServices(ninjectKernel);
            // создание связанных элементов
            IRewardLogic rewardLogic = ninjectKernel.Get<IRewardLogic>();
            IPersonLogic personLogic = ninjectKernel.Get<IPersonLogic>();
            IMedalLogic medalLogic = ninjectKernel.Get<IMedalLogic>();

            var result = rewardLogic.GetAll();

            Assert.IsNotNull(result, "Null");
        }
        #endregion

        #region Delete
        [TestMethod]
        public void DeleteMedal()
        {
            // запуск ninject
            IKernel ninjectKernel = new StandardKernel();
            // регистрация серверов
            Config.RegisterServices(ninjectKernel);
            // создание связанных элементов
            IRewardLogic rewardLogic = ninjectKernel.Get<IRewardLogic>();
            IPersonLogic personLogic = ninjectKernel.Get<IPersonLogic>();
            IMedalLogic medalLogic = ninjectKernel.Get<IMedalLogic>();

            var medal = medalLogic.GetById(5);

            Assert.IsNotNull(medal, "medal is Null");

            var result = medalLogic.Delete(5);

            Assert.IsTrue(result, "FALSE");
        }

        [TestMethod]
        public void DeletePerson()
        {
            // запуск ninject
            IKernel ninjectKernel = new StandardKernel();
            // регистрация серверов
            Config.RegisterServices(ninjectKernel);
            // создание связанных элементов
            IRewardLogic rewardLogic = ninjectKernel.Get<IRewardLogic>();
            IPersonLogic personLogic = ninjectKernel.Get<IPersonLogic>();
            IMedalLogic medalLogic = ninjectKernel.Get<IMedalLogic>();

            var person = personLogic.GetById(6);

            Assert.IsNotNull(person, "person is Null");

            var result = personLogic.Delete(person.id);

            Assert.IsTrue(result, "FALSE");
        }

        [TestMethod]
        public void DeleteRewarding()
        {
            // запуск ninject
            IKernel ninjectKernel = new StandardKernel();
            // регистрация серверов
            Config.RegisterServices(ninjectKernel);
            // создание связанных элементов
            IRewardLogic rewardLogic = ninjectKernel.Get<IRewardLogic>();
            IPersonLogic personLogic = ninjectKernel.Get<IPersonLogic>();
            IMedalLogic medalLogic = ninjectKernel.Get<IMedalLogic>();

            Assert.IsNotNull(personLogic.GetById(3), "Person is NULL");

            Assert.IsNotNull(medalLogic.GetById(4), "Medal is NULL");

            Reward reward = new Reward
            {
                awarded = personLogic.GetById(3),
                medal = medalLogic.GetById(4)
            };

            Assert.IsTrue(rewardLogic.Delete(reward), "FALSE");
        }
        #endregion

        #region GetById
        [TestMethod]
        public void GetMedalById()
        {
            // запуск ninject
            IKernel ninjectKernel = new StandardKernel();
            // регистрация серверов
            Config.RegisterServices(ninjectKernel);
            // создание связанных элементов
            IRewardLogic rewardLogic = ninjectKernel.Get<IRewardLogic>();
            IPersonLogic personLogic = ninjectKernel.Get<IPersonLogic>();
            IMedalLogic medalLogic = ninjectKernel.Get<IMedalLogic>();

            var result = medalLogic.GetById(3);

            Assert.IsNotNull(result, "NULL");
        }

        [TestMethod]
        public void GetPersonById()
        {
            // запуск ninject
            IKernel ninjectKernel = new StandardKernel();
            // регистрация серверов
            Config.RegisterServices(ninjectKernel);
            // создание связанных элементов
            IRewardLogic rewardLogic = ninjectKernel.Get<IRewardLogic>();
            IPersonLogic personLogic = ninjectKernel.Get<IPersonLogic>();
            IMedalLogic medalLogic = ninjectKernel.Get<IMedalLogic>();

            var result = personLogic.GetById(4);

            Assert.IsNotNull(result, "Null");
        }

        [TestMethod]
        public void GetReward()
        {
            // запуск ninject
            IKernel ninjectKernel = new StandardKernel();
            // регистрация серверов
            Config.RegisterServices(ninjectKernel);
            // создание связанных элементов
            IRewardLogic rewardLogic = ninjectKernel.Get<IRewardLogic>();
            IPersonLogic personLogic = ninjectKernel.Get<IPersonLogic>();
            IMedalLogic medalLogic = ninjectKernel.Get<IMedalLogic>();

            var result = personLogic.GetById(3);

            Assert.IsNotNull(result, "Null");

            var array = rewardLogic.GetPersonAll(result.id);

            Assert.IsNotNull(array, "Null");

        }
        #endregion
    }
}