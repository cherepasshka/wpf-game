using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace проект
{
    class Assyrian : Hero
    {
        public override string ThirdAttack
        {
            get { return "Мантия возмездия"; }
        }
        public override string FourthAttack
        {
            get { return "Дыхание хила"; }
        }
        public override string FifthAttack
        {
            get { return "Реактор смерти"; }
        }
        public override string SecondAttackInfo
        {
            get
            {
                return base.SecondAttackInfo + " , если после своего подлатывания маны осталось больше 20, увеличивает урон союзнику на 10 единиц";
            }
        }
        public override string ThirdAttackInfo
        {
            get
            {
                return "Если больше 30 маны, у всех монстров уменьшается урон на 10 единиц";
            }
        }
        public override string FourthAttackInfo
        {
            get { return "Если маны больше 60, всем героям восстанавливается 50 единиц HP"; }
        }
        public override string FifthAttackInfo
        {
            get { return "Если есть необходимая ульта (" + NeededUlta + "), то у половины монстров становится HP в 2 раза меньше"; }
        }
        public Assyrian(int maxHp, int maxMana, int damage, int kritChance, int neededUlta)
            :base(maxHp, maxMana, damage, kritChance, neededUlta)
        {
            Source = new BitmapImage(new Uri(@"Resources/assyrian.jpg", UriKind.Relative));
            Dead = new BitmapImage(new Uri(@"Resources/dead_assyrian.jpg", UriKind.Relative));
            Name = "Ассириан";
        }
        public override void Atack(Monster monster)
        {
            base.Atack(monster);
            monster.Ulta -= 10;
        }
        public void UseMana(Hero h)
        {
            base.UseMana();
            if (Mana > 20)
            {
                h.Damage += 10;
                Mana -= 20;
            }
        }
        public void MantleOfRetribution(List<Monster> monsters)
        {
            if (Mana > 30)
            {
                foreach (Monster m in monsters)
                {
                    m.Damage -= 10;
                }
                Mana -= 30;
                Ulta += 5;
            }
            GenerateKrit();
        }
        public void HealingBreath(List<Hero> heroes)
        {
            if (Mana > 60)
            {
                foreach (Hero h in heroes)
                {
                    h.Hp += 50;
                }
                Mana -= 60;
                Ulta += 2;
            }
            GenerateKrit();
        }
        public void DeathReactor(List<Monster> monsters)
        {
            if (Ulta == NeededUlta)
            {
                for(int i = 0; i < monsters.Count / 2; ++i)
                {
                    monsters[i].Hp /= 2;
                }
                Ulta = 0;
            }
            GenerateKrit();
        }
    }
}
