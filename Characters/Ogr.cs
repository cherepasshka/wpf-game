using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

namespace проект
{
    class Ogr:Monster
    {
        public override string SecondAttack
        {
            get { return "Вонь зла"; }
        }
        public override string ThirdAttack
        {
            get { return "Говорящая ступня"; }
        }
        public override string FourthAttack
        {
            get { return "Удар дубом"; }
        }
        public Ogr(int maxHp, int damage, int neededUlta)
            : base(maxHp, damage, neededUlta)
        {
            Source = new BitmapImage(new Uri(@"Resources/ogr.jpg", UriKind.Relative));
            Dead = new BitmapImage(new Uri(@"Resources/dead_ogr.jpg", UriKind.Relative));
            Name = "Огр";
        }
        public override void Atack(Hero hero)
        {
            base.Atack(hero);
            Ulta += 11;
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
                        HitWithOak(Transfer.heroes[i]);
                        VictimName = Transfer.heroes[i].Name;
                    }
                    else
                    {
                        bool ratat = false;
                        foreach (Hero h in Transfer.heroes)
                        {
                            if (h is Ratatosk)
                            {
                                SpeakingFoot(h as Ratatosk);
                                VictimName = h.Name;
                                ratat = true;
                                break;
                            }
                        }
                        if (!ratat)
                        {
                            StinkOfEvil(Transfer.heroes, Transfer.monsters);
                            VictimName = "всех";
                        }
                    }
                }
            }
        }
        public void HitWithOak(Hero hero)
        {
            if (Ulta == NeededUlta)
            {
                hero.Hp -= (2 * Damage + 10);
                Ulta = 0;
            }
            else
            {
                Hp -= hero.Damage / 2;
            }
        }
        public void StinkOfEvil(List<Hero> heroes,List<Monster> monsters)
        {
            int u = 0;
            foreach(Hero h in heroes)
            {
                u += h.Ulta / 10;
                h.KritChance--;
                h.Ulta -= h.Ulta / 10;
            }
            u = u / monsters.Count;
            foreach(Monster m in monsters)
            {
                m.Ulta += u;
            }
        }
        public void SpeakingFoot(Ratatosk ratatosk)
        {
            ratatosk.ActivitiOfAct = false;
        }
    }
}
