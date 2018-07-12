using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rewards.Manager.LogicContracts;
using Rewards.Manager.DalContracts;
using Rewards.Manager.Entities;

namespace Rewards.Manager.Logic
{
    public class RewardLogic : IRewardLogic
    {
        private IRewardDao _rewardDao;
        private IMedalDao _medalDao;
        private IPersonDao _personDao;

        public RewardLogic(IRewardDao rewardDao, IMedalDao medalDao, IPersonDao personDao)
        {
            _rewardDao = rewardDao;
            _medalDao = medalDao;
            _personDao = personDao;
        }

        // добавление награды
        public Reward Save(Reward reward)
        {
            Reward result;
            if (  (result=_rewardDao.Add(reward)) !=null)
            {
                return result;
            }
            else
                throw new InvalidOperationException("Error on reward saving!");
        }

        // удаление награды
        public bool Delete(Reward reward)
        {
            if (_rewardDao.Delete(reward))
            {
                return true;
            }
            else
                throw new InvalidOperationException("Error on reward deleting!");
        }


        // метод всех награжденных людей
        public Reward[] GetPersonAll(int id)
        {
            List<Reward> list = new List<Reward>();
            Reward[] all = GetAll();
            foreach (Reward r in all)
            {
                if (r.awarded.id == id)
                    list.Add(r);
            }

            return list.ToArray();
        }

        public Reward[] GetAll()
        {
            List<Reward> rewards = new List<Reward>();
            IEnumerable<int[]> rewardsPair = _rewardDao.GetAll();
            foreach (int[] pair in rewardsPair)
            {
                Reward next = new Reward();
                next.awarded = _personDao.GetById(pair[0]);
                next.medal = _medalDao.GetById(pair[1]);
                rewards.Add(next);
            }
            return rewards.ToArray();
        }
    }
}
