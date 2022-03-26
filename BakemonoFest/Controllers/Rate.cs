using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakemonoFest.Controllers
{
    public class Rate
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int MonsterId { get; set; }
        public Monster Monster { get; set; }
    }
}
