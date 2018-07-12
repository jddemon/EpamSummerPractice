using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewards.Manager.Entities
{
    /// <summary>
    /// сущность медаль
    /// </summary>
    public class Medal
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Material { get; set; }
    }
}
