using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace проект.Characters
{
    class WoodenWolf:Monster
    {
        private bool woodPower;
        public bool WoodPower
        {
            get { return woodPower; }
            set { woodPower = value; }
        }
        public override string SecondAttack
        {
            get { return "Удар дятла"; }
        }
        public override string ThirdAttack
        {
            get { return "Деревянный рёв"; }
        }
        public override string FourthAttack
        {
            get { return "Лапа  дружбы"; }
        }
        public WoodenWolf(int maxHp, int damage, int neededUlta)
            : base(maxHp, damage, neededUlta)
        {
            Source = new BitmapImage(new Uri(@"Resources/woodenwolf.jpg", UriKind.Relative));
            Dead = new BitmapImage(new Uri(@"Resources/dead_woodenwolf.jpg", UriKind.Relative));
            WoodPower = false;
            Name = "Деревянный волк";
        }
        public override void Atack(Hero hero)
        {
            base.Atack(hero);
            if (Hero.r.Next(0, Hp) > Hp / 2)
            {
                WoodPower = true;
            }
            else
            {
                WoodPower = false;
            }
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
                        PawOfFriendship(Transfer.heroes, Transfer.monsters);
                        VictimName = "всех";
                    }
                    else
                    {
                        bool jot = false;
                        foreach (Hero h in Transfer.heroes)
                        {
                            if (h is Jotun)
                            {
                                BlowOfWoodpecker(h as Jotun);
                                VictimName = h.Name;
                                jot = true;
                                break;
                            }
                        }
                        if (!jot && WoodPower)
                        {
                            WoodenRoar(Transfer.heroes);
                            VictimName = "всех";
                        }
                        else if (!WoodPower)
                        {
                            i = Hero.r.Next(0, Transfer.heroes.Count);
                            Atack(Transfer.heroes[i]);
                            VictimName = Transfer.heroes[i].Name;
                        }
                    }
                }
            }
        }
        public void WoodenRoar(List<Hero> heroes)
        {
            if (WoodPower)
            {
                foreach(Hero h in heroes)
                {
                    h.KritChance -= 10;
                    h.Mana /= 2;
                    h.Hp -= 9;
                }
                WoodPower = false;
            }
        }
        public void PawOfFriendship(List<Hero> heroes,List<Monster> monsters)
        {
            if (Ulta == NeededUlta)
            {
                int d = 0;
                foreach(Monster h in monsters)
                {
                    d += h.Damage + h.Hp/30;
                }
                d = d / monsters.Count;
                foreach (Hero h in heroes)
                {
                    h.Hp-= d;
                }
                Ulta = 0;
            }
        }
        public void BlowOfWoodpecker(Jotun jotun)
        {
            if (Hero.r.Next(0, this.Hp) > jotun.Hp - jotun.KritChance * jotun.Hp)
            {
                jotun.KritChance = 0;
                jotun.Mana -= 20;
                jotun.Hp -= 7;
            }
            else
            {
                this.Hp -= 30;
            }
            if (Hero.r.Next(0, Hp) > Hp / 2 )
            {
                WoodPower = true;
            }
            else
            {
                WoodPower = false;
            }
        }
    }
}
