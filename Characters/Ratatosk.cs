using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace проект
{
    class Ratatosk:Hero
    {
        private int nuts;
        private bool activitiOfAct;
        public bool ActivitiOfAct
        {
            get { return activitiOfAct; }
            set { activitiOfAct = value; }
        }
        public int Nuts
        {
            get { return nuts; }
            set {
                nuts = value;
                if (nuts < 0) nuts = 0;
            }
        }
        public override string ThirdAttack
        {
            get { return "Тёмное дупло"; }
        }
        public override string FourthAttack
        {
            get { return "Ореховая ярость"; }
        }
        public override string FifthAttack
        {
            get { return "Удар лапкой"; }
        }
        public override string ThirdAttackInfo
        {
            get { return "Массовая атака. Уменьшает урон на 5 очков и здоровье на 5 очков ящерицам"; }
        }
        public override string FourthAttackInfo
        {
            get { return "Закидывает врагов орехами, сколько есть орехов, столько закидает врагов, отнимает 10 очков HP"; }
        }
        public override string FifthAttackInfo
        {
            get { return "Наносит двойной урон, собирает орех, если нет необходимой ульты ("+NeededUlta+") то бъет союзника и хилит монстра"; }
        }
        public override string Info
        {
            get { return base.Info+ "\nКоличество орехов: "+ Nuts+"\nВозможность действовать: "+ActivitiOfAct; }
        }
        public Ratatosk(int maxHp, int maxMana, int damage, int kritChance, int neededUlta)
            :base(maxHp, maxMana, damage, kritChance, neededUlta)
        {
            Source = new BitmapImage(new Uri(@"Resources/ratatosk.jpg", UriKind.Relative));
            Dead = new BitmapImage(new Uri(@"Resources/dead_ratatosk.jpg", UriKind.Relative));
            Name = "Рататоск";
            Nuts = 10;
            ActivitiOfAct = true;
        }
        public override void Atack(Monster monster)
        {
            if (ActivitiOfAct)
            {
                base.Atack(monster);
                Nuts += 1;
            }
        }
        public override void UseMana()
        {
            if (ActivitiOfAct)
            {
                base.UseMana();
                if (Mana > 5)
                {
                    Nuts += 1;
                    Mana -= 5;
                }
            }
        }
        public void DarkHollow(List<Monster> monsters)
        {
            if (ActivitiOfAct)
            {
                foreach (Monster m in monsters)
                {
                    if (m is Lizard)
                    {
                        m.Damage -= 10;
                        m.Hp -= 5;
                        Ulta += 10;
                    }
                }
                Ulta += 5;
            }
            GenerateKrit();
        }
        public void NutAnger(List<Monster> monsters)
        {
            if (ActivitiOfAct)
            {
                int i = 0;
                while (nuts > 0 && i < monsters.Count)
                {
                    monsters[i].Hp -= 10;
                    i++;
                    nuts--;
                }
                Ulta += (int)(0.3 * NeededUlta);
                GenerateKrit();
            }
        }
        public void PawHit(Monster m, Hero h)
        {
            if (ActivitiOfAct)
            {
                if (Ulta == NeededUlta)
                {
                    m.Hp -= 2 * Damage;
                    h.Hp += Damage;
                    Ulta = 0;
                    Nuts += 1;
                }
                else
                {
                    h.Hp -= 2 * Damage;
                    m.Hp += Damage;
                }
            }
            GenerateKrit();
        }
    }
}
