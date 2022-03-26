using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace проект
{
    class LavenderMonster : Monster
    {
        private int lavanda;
        public int Lavanda
        {
            get { return lavanda; }
            set
            {
                lavanda = value;
                if (lavanda < 0) lavanda = 0;
            }
        }
        public override string SecondAttack
        {
            get { return "Запах слабака"; }
        }
        public override string ThirdAttack
        {
            get { return "Повышение урона"; }
        }
        public override string FourthAttack
        {
            get { return "Прожёг медика"; }
        }
        public override string Info
        {
            get
            {
                return base.Info + "\nКоличество лаванды: " + Lavanda;
            }
        }
        public LavenderMonster(int maxHp, int damage, int neededUlta) : base(maxHp, damage, neededUlta)
        {
            Source = new BitmapImage(new Uri(@"Resources/lavendermonster.jpg", UriKind.Relative));
            Dead = new BitmapImage(new Uri(@"Resources/dead_lavendermonster.jpg", UriKind.Relative));
            Lavanda = 0;
            Name = "Лавандовый монстр";
        }
        public override void Atack(Hero hero)
        {
            base.Atack(hero);
            lavanda += 1;
        }
        public override void ChooseAttatck()
        {
            if(Transfer.heroes.Count!=0 && Transfer.monsters.Count != 0) {
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
                        foreach (Hero h in Transfer.heroes)
                        {
                            if (h is Assyrian)
                            {
                                AnnihilationOfMedico(h as Assyrian);
                                VictimName = h.Name;
                            }
                        }
                    }
                    else
                    {
                        if (Lavanda > 20)
                        {
                            SmeelOfDwimp(Transfer.heroes);
                            VictimName = "всех";
                        }
                        else
                        {
                            bool can = false;
                            foreach (Monster m in Transfer.monsters)
                            {
                                if (m.Damage + 20 < Transfer.maxDamage)
                                {
                                    can = true;
                                }
                            }
                            if (can)
                            {
                                DamageRestoring(Transfer.monsters);
                                VictimName = "никого, восстанавливает урон монстрам";
                            }
                            else
                            {
                                i = Hero.r.Next(0, Transfer.heroes.Count);
                                Atack(Transfer.heroes[i]);
                                VictimName = Transfer.heroes[i].Name;
                            }
                        }
                    }
                }
            }
        }
        public void DamageRestoring(List<Monster> monsters)
        {
            foreach (Monster m in monsters)
            {
                m.Damage += 20;
            }
            lavanda += 3;
        }
        public void SmeelOfDwimp(List<Hero> heroes)
        {
            if (lavanda > 20)
            {
                foreach (Hero h in heroes)
                {
                    h.KritChance = 0;
                    h.Damage -= 10;
                }
                lavanda -= 20;
            }
        }
        public void AnnihilationOfMedico(Assyrian assyrian)
        {
            if (Ulta == NeededUlta)
            {
                assyrian.KritChance = 0;
                assyrian.Hp /= 2;
                assyrian.Mana -= 20;
                Ulta = 0;
            }
        }
    }
}
