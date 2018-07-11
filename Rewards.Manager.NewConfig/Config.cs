using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Ninject;
using Rewards.Manager.LogicContracts;
using Rewards.Manager.Logic;
using Rewards.Manager.DalContracts;
using Rewards.Manager.FileDal;


namespace Rewards.Manager.NewConfig
{
    /// <summary>
    ///  класс позднего связывания 
    /// </summary>
    public static class Config
    {
        public static void RegisterServices(IKernel kernel)
        {
            kernel
                .Bind<IRewardLogic>()
                .To<RewardLogic>();
            kernel
                .Bind<IPersonLogic>()
                .To<PersonLogic>();
            kernel
                .Bind<IMedalLogic>()
                .To<MedalLogic>();
            // выбор из файла конфигурации программы строки "DaoType"
            string daoType = ConfigurationManager.AppSettings["DaoType"];
            switch (daoType)
            {
                 // связываем 3 пары сущности Reward,Person,Medal 
                case "DataBase":
                    kernel
                        .Bind<IRewardDao>()
                        .To<BaseRewardDao>();
                    kernel
                        .Bind<IPersonDao>()
                        .To<BasePersonDao>();
                    kernel
                         .Bind<IMedalDao>()
                         .To<BaseMedalDao>();
                    break;
                default:
                    throw new ArgumentException("Wrong DAO type in config file");
            }
        }
    }
}
