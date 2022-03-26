using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
namespace проект
{
    public class Hero
    {
        public static Random r = new Random();
        private int hp, maxHp, mana, maxMana, damage, kritChance,ulta,neededUlta;
        private string name;
        private ImageSource source, dead;
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }
        public ImageSource Source
        {
            get { return source; }
            set { source = value; }
        }
        public ImageSource Dead
        {
            get { return dead; }
            set { dead = value; }
        }
        public int Hp
        {
            get { return hp; }
            set 
            { 
                hp = value;
                if (hp > maxHp) hp = maxHp;
                if (hp < 0) hp = 0;
            }
        }
        public int MaxHp
        {
            get { return maxHp; }
            set { maxHp = value; }
        }
        public int Mana
        {
            get { return mana; }
            set 
            {
                mana = value;
                if (mana > maxMana) mana = maxMana;
                if (mana < 0) mana = 0;
            }
        }
        public int MaxMana
        {
            get { return maxMana; }
            set { maxMana = value; }
        }
        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }
        public int KritChance
        {
            get { return kritChance; }
            set
            {
                kritChance = value;
                if (kritChance < 0) kritChance = 0;
            }
        }
        public void GenerateKrit()
        {
            KritChance = r.Next(0, 101);
        }
        public int Ulta
        {
            get { return ulta; }
            set
            {
                ulta = value;
                if (ulta > neededUlta) ulta = neededUlta;
                if (ulta < 0) ulta = 0;
            }
        }
        public int NeededUlta
        {
            get { return neededUlta; }
            set { neededUlta = value; }
        }
        public string FirstAttack
        {
            get { return "Базовая атака"; }
        }
        public string SecondAttack
        {
            get { return "Магическая атака"; }
        }
        public virtual string ThirdAttack
        {
            get { return ""; }
        }
        public virtual string FourthAttack
        {
            get { return ""; }
        }
        public virtual string FifthAttack
        {
            get { return ""; }
        }
        public string FirstAttackInfo
        {
            get { return "Герой наносит противнику урон, если критический шанс удовлетворяет определённым условиям, противнику наносится дополнительный урон"; }
        }
        public virtual string SecondAttackInfo
        {
            get { return "Герой подлечивается, если маны больше 20 единиц"; }
        }
        public virtual string ThirdAttackInfo
        {
            get { return ""; }
        }
        public virtual string FourthAttackInfo
        {
            get { return ""; }
        }
        public virtual string FifthAttackInfo
        {
            get { return ""; }
        }
        public virtual string Info
        {
            get
            {
                return "Герой: "+Name+"\nМаксимальное здоровье: " + MaxHp + "\nТекущее здоровье: " + Hp + "\nМаксимальная мана: " + MaxMana + "\nТекущая мана: "
                    + Mana + "\nУрон: " + Damage + "\nКритический шанс: " + KritChance + "\nНеобходимая ульта: " + NeededUlta + "\nУльта: " + Ulta;

            }
        }
        public Hero(int maxHp, int maxMana, int damage, int kritChance,int neededUlta)
        {
            MaxHp = maxHp;
            Hp = maxHp;
            MaxMana = maxMana;
            Mana = maxMana;
            Damage = damage;
            KritChance = kritChance;
            NeededUlta = neededUlta;
            Ulta = 0;
            //Attacked = false;
        }
        public virtual void Atack(Monster monster)
        {
            monster.Hp -= Damage;
            Mana += 10;
            int dam = Hero.r.Next(0, 101);
            if (dam <= KritChance)
            {
                monster.Hp -= (Damage * dam) / 10;
                KritChance -= dam;
            }
            Ulta += 10;
            GenerateKrit();
        }
        public virtual void UseMana()
        {
            if (Mana > 20)
            {
                Hp += MaxHp / 2;
                Mana -= 20;
                Ulta += 3;
            }
            GenerateKrit();
        }
    }
}
