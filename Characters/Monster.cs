using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace проект
{
     public class Monster
    {
        public static Random r = new Random();
        private int hp, maxHp, damage,ulta,neededUlta;
        private string name,victimName;
        private ImageSource source,dead;
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
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string VictimName
        {
            get { return victimName; }
            set { victimName = value; }
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
        public int Damage
        {
            get { return damage; }
            set { damage = value; }
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
        public virtual string SecondAttack
        {
            get { return ""; }
        }
        public virtual string ThirdAttack
        {
            get { return ""; }
        }
        public virtual string FourthAttack
        {
            get { return ""; }
        }
        public string FirstAttackInfo
        {
            get { return "Монстр наносит противнику урон"; }
        }
        public virtual string SecondAttackInfo
        {
            get { return ""; }
        }
        public virtual string ThirdAttackInfo
        {
            get { return ""; }
        }
        public virtual string FourthAttackInfo
        {
            get { return ""; }
        }
        public virtual string Info
        {
            get
            {
                return "Монстр: " + Name + "\nМаксимальное здоровье: " + MaxHp + "\nТекущее здоровье: " + Hp + "\nУрон: " + Damage 
                    + "\nНеобходимая ульта: " + NeededUlta + "\nУльта: " + Ulta;
            }
        }
        public Monster(Monster m)
        {
            Hp = m.Hp;
            MaxHp = m.MaxHp;
            Damage = m.Damage;
            Ulta = m.Ulta;
            NeededUlta = m.NeededUlta;
        }
        public Monster(int maxHp, int damage,int neededUlta)
        {
            MaxHp = maxHp;
            Hp = maxHp;
            Damage = damage;
            NeededUlta = neededUlta;
            Ulta = ulta;
        }
        public virtual void Atack(Hero hero)
        {
            hero.Hp -= Damage;
        }
        public virtual void ChooseAttatck() { }
    }
}
