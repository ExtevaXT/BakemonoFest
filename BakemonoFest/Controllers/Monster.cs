using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakemonoFest.Controllers
{
    public class Monster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int NominationId { get; set; }
        public Nomination Nomination { get; set; }
        public int MonsterTypeId { get; set; }
        public MonsterType MonsterType { get; set; }
        public DateTime Birthday { get; set; }
        public string Job { get; set; }
        public string Photo { get; set; }
        public double Rating { get; set; }

    }
}
