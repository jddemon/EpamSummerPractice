using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rewards.Manager.Entities;

namespace Rewards.Manager.LogicContracts
{
    public interface IPersonLogic
    {
        Person[] GetAll();
        Person Save(Person person);
        bool Delete(int id);
        Person Update(Person person);
        Person GetById(int id);
    }
}
