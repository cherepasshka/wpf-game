using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace проект
{
    public static class Transfer
    {
        public static string userName,infoMouseEnter,records;
        public static bool wasMouseEnter, wasMouseLeave,helpedRatatosk;
        public static List<Hero> heroes = new List<Hero>();
        public static List<Monster> monsters = new List<Monster>();
        public static Monster monster;
        public static Hero hero;
        public static int maxHp, maxDamage;
    }
}
