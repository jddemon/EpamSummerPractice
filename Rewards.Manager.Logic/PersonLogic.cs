using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rewards.Manager.Entities;
using Rewards.Manager.LogicContracts;
using Rewards.Manager.DalContracts;

namespace Rewards.Manager.Logic
{
    /// <summary>
    /// реализация уровня логики Person
    /// </summary>
    public class PersonLogic : IPersonLogic
    {

        private IPersonDao _personDao;
        private IMedalDao _medalDao;
        private IRewardLogic _rewardLogic;

        public PersonLogic(IPersonDao personDao, IMedalDao medalDao, IRewardLogic rewardLogic)
        {
            _personDao = personDao;
            _medalDao= medalDao;
            _rewardLogic = rewardLogic;
        }

        // получение списка персон
        public Person[] GetAll()
        {
            return _personDao.GetAll().ToArray();
        }

        // сохранение персоны
        public Person Save(Person person)
        {
            if (string.IsNullOrWhiteSpace(person.Imja)
                || string.IsNullOrWhiteSpace(person.Familia)
                || string.IsNullOrWhiteSpace(person.Adres)
                )
            {
                throw new ArgumentException("Note text cannot be null or whitespace.", nameof(person));
            }

            Person result;
            if ((result=_personDao.Add(person)) != null)
            {
                return result;
            }
            else
                throw new InvalidOperationException("Error on person saving!");
        }

        // обновление персоны
        public Person Update(Person person)
        {
            if (string.IsNullOrWhiteSpace(person.Imja)
                || string.IsNullOrWhiteSpace(person.Familia)
                || string.IsNullOrWhiteSpace(person.Adres)
                )
            {
                throw new ArgumentException("Text cannot be null or whitespace.", nameof(person));
            }


            if (_personDao.Update(person))
            {
                return person;
            }
            else
                throw new InvalidOperationException("Error on person updating!");
        }

        // удаление из списка
        public bool Delete(int id)
        {
            // связь наград с персоной
            Reward[] rewards = _rewardLogic.GetPersonAll(id);
            foreach (Reward r in rewards)
                _rewardLogic.Delete(r);
             return _personDao.Delete(id);
        }

        // метод получения списка персон по id
        public Person GetById(int id)
        {
            return _personDao.GetById(id);
        }

    }
}
