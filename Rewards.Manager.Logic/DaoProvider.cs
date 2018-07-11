using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rewards.Manager.LogicContracts;
using Rewards.Manager.DalContracts;


namespace Rewards.Manager.Logic
{
    internal static class DaoProvider
    {
        static DaoProvider()
        {
        }
        public static IRewardDao RewardDao { get; }
    }

}
