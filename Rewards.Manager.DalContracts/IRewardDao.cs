using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rewards.Manager.Entities;

namespace Rewards.Manager.DalContracts
{
    /// <summary>
    /// интерфейс получения данных для Reward
    /// </summary>
    public interface IRewardDao
    {
        // добавление нового награды
        Reward Add(Reward reward);

        // получение всего списка
        IEnumerable<int[]> GetAll();

        // удаление награды 
        bool Delete(Reward reward);

    }
}
