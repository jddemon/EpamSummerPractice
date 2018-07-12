using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rewards.Manager.Entities;

namespace Rewards.Manager.DalContracts
{
    /// <summary>
    /// интерфейс получения данных для Medal
    /// </summary>
    public interface IMedalDao
    {
        // добавление новой медали
        Medal Add(Medal medal);

        // обновление медали
        bool Update(Medal medal);

        // получение всего списка
        IEnumerable<Medal> GetAll();

        // получение медали по идентификатору
        Medal GetById(int id);

        // удаление медали
        bool Delete(int id);
    }
}
