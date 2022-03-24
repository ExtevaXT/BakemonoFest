using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakemonoFest.Controllers
{
    public class MobileContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Nomination> Nominations { get; set; }
        public DbSet<Monster> Monsters { get; set; }
        public DbSet<MonsterType> MonsterTypes { get; set; }

        public MobileContext(DbContextOptions<MobileContext> options)
            : base(options)
        {

        }
    }
}
