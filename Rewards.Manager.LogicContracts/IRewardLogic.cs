using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rewards.Manager.Entities;

namespace Rewards.Manager.LogicContracts
{
    public interface IRewardLogic
    {
        Reward[] GetPersonAll(int id);
        Reward Save(Reward reward);
        Reward[] GetAll();
        bool Delete(Reward reward);
    }
}
