using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace проект
{
    class LittleFingerer:Monster
    {
        private int fingers;
        public int Fingers
        {
            get { return fingers; }
            set
            {
                fingers = value;
                if (fingers < 0) fingers = 0;
            }
        }
        private bool FingersFlaw
        {
            get { return Fingers > 0; }
        }
        public override string SecondAttack
        {
            get { return "Целебный палец"; }
        }
        public override string ThirdAttack
        {
            get { return "Воскресить"; }
        }
        public override string FourthAttack
        {
            get { return "Пальце-торнадо"; }
        }
        public override string Info
        {
            get { return base.Info + "\nКоличество пальцев: " + Fingers; }
        }
        public LittleFingerer(Monster m):base(m)
        {
            Fingers = 100;
        }
        public LittleFingerer(int maxHp, int damage, int neededUlta,int fingers):base(maxHp, damage, neededUlta)
        {
            Source = new BitmapImage(new Uri(@"Resources/littlefingerer.jpg", UriKind.Relative));
            Dead = new BitmapImage(new Uri(@"Resources/dead_littlefingerer.jpg", UriKind.Relative));
            Fingers = fingers;
            Name = "Мизинчатый";
        }
        public override void Atack(Hero hero)
        {
            base.Atack(hero);
            if (hero.KritChance > 10)
            {
                hero.KritChance -= 10;
                Fingers += 1;
            }
            if (FingersFlaw)
            {
                Hp -= 1;
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
                        FingerTornado(Transfer.heroes);
                        VictimName = "всех";
                    }
                    else
                    {
                        bool dead = false;
                        foreach (Monster n in Transfer.monsters)
                        {
                            if (n.Hp == 0)
                            {
                                Resurrect(n);
                                VictimName = "никого, воскресил " + n.Name;
                                dead = true;
                                break;
                            }
                        }
                        if (!dead)
                        {
                            HealingFinger(Transfer.monsters);
                            VictimName = "никого, лечит союзников";
                        }
                    }
                }
            }
        }
        public void HealingFinger(List<Monster> teammates)
        {
            if (Fingers >= 3)
            {
                Monster min = teammates[0];
                foreach (Monster m in teammates)
                {
                    if (m.Hp < min.Hp)
                    {
                        min = m;
                    }
                }
                min.Hp = min.MaxHp;
                Fingers -= 3;
                Ulta += (int)(0.1 * NeededUlta);
                //hero.Hp -= 40;
            }
            if (FingersFlaw)
            {
                Hp -= 1;
            }
        }
        public void FingerTornado(List<Hero> heroes)
        {
            if (Ulta == NeededUlta)
            {
                foreach (Hero h in heroes)
                {
                    h.Hp -= 5;
                }
                Fingers += 1;
                Ulta = 0;
            }
            if (FingersFlaw)
            {
                Hp -= 1;
            }
        }
        public void Resurrect(Monster dead)//воскрешение
        {
            if (Fingers > 0)
            {
                dead.Hp = dead.MaxHp;
                Ulta = Ulta / 2;
            }
            if (FingersFlaw)
            {
                Hp -= 1;
            }
        }
    }
}
