using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Rewards.Manager.Entities
{
    /// <summary>
    /// сущность персона
    /// </summary>
    public class Person
    {
        public int id { get; set; }
        public string Imja { get; set; }
        public string Familia { get; set; }
        public DateTime Birthday { get; set; }
        public int Vozrast { get; set; }
        public string Adres { get; set; }
    }
}
