using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace проект.Characters
{
    class SiliCat:Hero
    {
        public override string ThirdAttack
        {
            get { return "RataHelp"; }
        }
        public override string FourthAttack
        {
            get { return "Царапина камня"; }
        }
        public override string FifthAttack
        {
            get { return "Песчаное цунами"; }
        }
        public override string SecondAttackInfo
        {
            get
            {
                return base.SecondAttackInfo + " , если после своего подлатывания маны осталось больше 4, лечит одного союзника";
            }
        }
        public override string ThirdAttackInfo
        {
            get
            {
                return "Если маны больше 30, герою Рататоск возвращается возможность атаковать (её может отнять Огр)";
            }
        }
        public override string FourthAttackInfo
        {
            get { return "Атакует огра: уменьшает его ульту на величину своего наносимого урона, уменьшает урон огру на 10 единиц"; }
        }
        public override string FifthAttackInfo
        {
            get { return "Если есть необходимая ульта (" + NeededUlta + "), то монстров отнимается от HP среднее арифметическое их наносимых уронов"; }
        }
        public SiliCat(int maxHp, int maxMana, int damage, int kritChance, int neededUlta)
            :base(maxHp, maxMana, damage, kritChance, neededUlta)
        {   
            Source = new BitmapImage(new Uri(@"Resources/silicat.jpg", UriKind.Relative));
            Dead = new BitmapImage(new Uri(@"Resources/dead_silicat.jpg", UriKind.Relative));
            Name = "КремнеКот";
        }
        public override void Atack(Monster monster)
        {
            base.Atack(monster);
            monster.Damage -= 1;
        }
        public void UseMana(Hero h)
        {
            base.UseMana();
            if (Mana > 4)
            {
                Mana -= 4;
                h.Hp += 10;
            }
        }
        public void RataHelp(Ratatosk ratatosk)
        {
            if (Mana > 30)
            {
                Mana -= 30;
                ratatosk.ActivitiOfAct = true;
                Ulta += 6;
            }
        }
        public void StoneScratch(Ogr ogr)
        {
            ogr.Ulta -= Damage;
            ogr.Damage -= 10;
            Ulta += 9;
        }
        public void SandTsunami(List<Monster> monsters)
        {
            if (Ulta == NeededUlta)
            {
                int d = 0;
                foreach(Monster m in monsters)
                {
                    d += m.Damage;
                }
                d /= monsters.Count;
                foreach (Monster m in monsters)
                {
                    m.Hp -= d;
                }
            }
        }
    }
}
