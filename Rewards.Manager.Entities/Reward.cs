using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewards.Manager.Entities
{
    /// <summary>
    /// сущность награда
    /// </summary>
    public class Reward
    {
        public Person awarded { get; set; }
        public Medal medal { get; set; }
    }
}
