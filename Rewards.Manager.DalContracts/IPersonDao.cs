using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rewards.Manager.Entities;

namespace Rewards.Manager.DalContracts
{
    /// <summary>
    /// интерфейс получения данных для Person
    /// </summary>
    public interface IPersonDao
    {
        // добавление нового человека
        Person Add(Person person);

        // получение всего списка
        IEnumerable<Person> GetAll();

        // получение персоны по идентификатору
        Person GetById(int id);

        // обновление персоны
        bool Update(Person person);
        
        // удаление из списка 
        bool Delete(int id);
    }
}
