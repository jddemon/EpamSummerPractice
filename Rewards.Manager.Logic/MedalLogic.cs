using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rewards.Manager.LogicContracts;
using Rewards.Manager.Entities;
using Rewards.Manager.DalContracts;

namespace Rewards.Manager.Logic
{
    /// <summary>
    /// логика медали
    /// </summary>
    public class MedalLogic:IMedalLogic
    {
        private IMedalDao _medalDao;
        public MedalLogic(IMedalDao  medalDao)
        {
            _medalDao = medalDao;
        }

        // метод получения списка медалей
        public Medal[] GetAll()
        {
            return _medalDao.GetAll().ToArray();
        }

        // метод получения списка медалей по id
        public Medal GetById(int id)
        {
            return GetAll().SingleOrDefault(medal=>medal.id==id);
        }

        // сохранение медали
        public Medal Save(Medal medal)
        {
            if (string.IsNullOrWhiteSpace(medal.Name)
                || string.IsNullOrWhiteSpace(medal.Material)                
                )
            {
                throw new ArgumentException("text cannot be null or whitespace.", nameof(medal));
            }


            Medal result;
            if ( (result=_medalDao.Add(medal))!=null)
            {
                return medal;
            }
            else
                throw new InvalidOperationException("Error on medal saving!");
        }

        // удаление медали
        public bool Delete(int id)
        {
            if (_medalDao.Delete(id))
                return true;
            else
                return false;
        }
    }
}
