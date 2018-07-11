using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rewards.Manager.Entities;

namespace Rewards.Manager.LogicContracts
{
    public interface IMedalLogic
    {
        Medal[] GetAll();
        Medal Save(Medal medal);
        bool Delete(int id);
        Medal GetById(int id);
    }

}
