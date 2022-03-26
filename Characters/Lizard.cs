using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace проект
{
    class Lizard:Monster
    {
        private int slime;
        public int Slime
        {
            get { return slime; }
            set
            {
                slime = value;
                if (slime < 0) slime = 0;
            }
        }
        public override string SecondAttack
        {
            get { return "Мах хвостом"; }
        }
        public override string ThirdAttack
        {
            get { return "Монстр из слизи"; }
        }
        public override string FourthAttack
        {
            get { return "Супер слиз"; }
        }
        public Lizard(int maxHp, int damage,int neededUlta)
            :base(maxHp, damage, neededUlta)
        {
            Source = new BitmapImage(new Uri(@"Resources/lizard.jpg",UriKind.Relative));
            Dead = new BitmapImage(new Uri(@"Resources/dead_lizard.jpg", UriKind.Relative));
            Slime = 0;
            Name = "Ящерица";
        }
        public override void ChooseAttatck()
        {
            if (Transfer.heroes.Count != 0 && Transfer.monsters.Count != 0)
            {
                int i = Monster.r.Next(0, 100);
                if (i >= 50)
                {
                    i = Hero.r.Next(0, Transfer.heroes.Count);
                    Atack(Transfer.heroes[i]);
                    VictimName = Transfer.heroes[i].Name;
                }
                else
                {
                    if (Ulta == NeededUlta)
                    {
                        i = Hero.r.Next(0, Transfer.heroes.Count);
                        SuperLick(Transfer.heroes[i]);
                        VictimName = Transfer.heroes[i].Name;
                    }
                    else
                    {
                        if (Slime >= 20)
                        {
                            SlimeMonstr(Transfer.heroes);
                            VictimName = "всех";
                        }
                        else
                        {
                            i = Hero.r.Next(0, Transfer.heroes.Count);
                            TailWave(Transfer.heroes[i]);
                            VictimName = Transfer.heroes[i].Name;
                        }
                    }
                }
            }
        }
        public override void Atack(Hero hero)
        {
            base.Atack(hero);
            Slime += 10;
        } 
        public void SuperLick(Hero hero)
        {
            if (Ulta == NeededUlta)
            {
                hero.Hp -= 2 * Damage;
                Ulta = 0;
            }
            else
            {
                Hp -= Damage / 2;
            }
        }
        public void TailWave(Hero hero)
        {
            hero.Hp -= 10;
            hero.Mana -= 10;
            hero.KritChance -= 8;
        }
        public void SlimeMonstr(List<Hero> heroes)
        {
            if (slime >=20)
            {
                Slime -= 20;
                foreach(Hero h in heroes)
                {
                    h.Hp -= 5;
                    h.Damage -= 3;
                }
            }
        }
    }
}
