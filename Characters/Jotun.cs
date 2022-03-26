using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace проект
{
    class Jotun:Hero
    {
        private bool wolfTrust;
        int fingerCount;
        public int FingerCount
        {
            get { return fingerCount; }
            set
            {
                fingerCount = value;
                if (fingerCount < 0) fingerCount = 0;
            }
        }
        public bool WolfTrust
        {
            get { return wolfTrust; }
            set { wolfTrust = value; }
        }
        public override string ThirdAttack
        {
            get { return "Удар мизинцем"; }
        }
        public override string FourthAttack
        {
            get { return "Топот Фенрира"; }
        }
        public override string FifthAttack
        {
            get { return "Огненный куб"; }
        }
        public override string ThirdAttackInfo
        {
            get { return "Атака ориентирована на мизинчатых: если у мизинч. больше 9 пальцев, Йотун забирает все, кроме 9-ти, если пальцев"
                    +" меньше, то ульта и HP становится максимальной, а монстр умирает, если пальцев у героя больше 30, то открывается доверие волка и атака топот Фенрира"; }
        }
        public override string FourthAttackInfo
        {
            get { return "Если есть доверие волка, Йотун призывает Фенрира, и тот давит ящериц, забирая у них десятую часть ульты - 5 единиц ульты"; }
        }
        public override string FifthAttackInfo
        {
            get { return "Если есть необходимая ульта ("+NeededUlta+"), то у всех монстров отнимается 40 единиц HP"; }
        }
        public override string Info
        {
            get
            {
                return base.Info + "\nДоверие волка: " + WolfTrust + "\nСчётчик пальцев: " + FingerCount;
            }
        }
        public Jotun(int maxHp, int maxMana, int damage, int kritChance, int neededUlta)
            :base(maxHp, maxMana, damage, kritChance, neededUlta)
        {
            Source = new BitmapImage(new Uri(@"Resources/jotun.jpg", UriKind.Relative));
            Dead = new BitmapImage(new Uri(@"Resources/dead_jotun.jpg", UriKind.Relative));
            Name = "Йотун";
            WolfTrust = false;
        }
        public override void Atack(Monster monster)
        {
            base.Atack(monster);
            monster.Hp -= 2;
            Ulta += (int)(0.2 * NeededUlta);
        }
        public void  FireCube(List<Monster> monsters)
        {
            if (Ulta == NeededUlta)
            {
                foreach(Monster mon in monsters)
                {
                    mon.Hp -= 40;
                }
                Ulta = 0;
            }
            GenerateKrit();
        }
        public void LittleFingerHit(LittleFingerer lf)
        {
            if (lf.Fingers > 9)
            {
                fingerCount += lf.Fingers - 9;
                lf.Fingers = 9;
                Ulta += 10;
            }
            else
            {
                Ulta = NeededUlta;
                lf.Hp = 0;
                fingerCount += lf.Fingers;
                lf.Fingers = 0;
                Hp = MaxHp;
            }
            if (fingerCount > 30)
            {
                WolfTrust = true;
                FingerCount -= 30;
                Ulta += 1;
            }
            GenerateKrit();
        }
        public void FenrirBrattle(List<Monster> monsters)
        {
            if (wolfTrust)
            {
                foreach(Monster m in monsters)
                {
                    if(m is Lizard)
                    {
                        m.Hp = 0;
                        Ulta += m.Ulta / 10;
                        Ulta -= 5;
                    }
                }
                WolfTrust = false;
                Ulta += 4;
            }
            GenerateKrit();
        }
    }
}
