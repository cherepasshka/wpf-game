using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using проект.Characters;

namespace проект
{
    /// <summary>
    /// Логика взаимодействия для HeroElement.xaml
    /// </summary>
    public partial class HeroElement : UserControl
    {
        Hero hero;
        bool attacked;
        public HeroElement()
        {
            InitializeComponent();
            Attacked = false;
        }
        void FillButtonsAndTextBoxes()
        {
            Source = hero.Source;
            buttonAttack1.Content = hero.FirstAttack;
            buttonAttack2.Content = hero.SecondAttack;
            buttonAttack3.Content = hero.ThirdAttack;
            buttonAttack4.Content = hero.FourthAttack;
            buttonAttack5.Content = hero.FifthAttack;
            textBlockInfo.Text = hero.Info;
        }
        public Hero Hero
        {
            set
            {
                hero = value;
                FillButtonsAndTextBoxes();
            }
            get { return hero; }
        }
        public bool Attacked
        {
            get { return attacked; }
            set { attacked = value; }
        }
        public ImageSource Source
        {
            get { return image.Source; }
            set { image.Source = value; }
        }
        public object ButtonAttack1_Content
        {
            get { return buttonAttack1.Content; }
            set { buttonAttack1.Content = value; }
        }
        public void Update()
        {
            textBlockInfo.Text = Hero.Info;
        }
        private void buttonAttack1_Click(object sender, RoutedEventArgs e)
        {
            if (!Attacked)
            {
                MonsterChoose choose = new MonsterChoose();
                choose.ShowDialog();
                hero.Atack(Transfer.monster);
                Attacked = true;
                IsEnabled = false;
            }
        }
        private void buttonAttack2_Click(object sender, RoutedEventArgs e)
        {
            if (!Attacked)
            {
                if (hero is Assyrian)
                {
                    HeroChoose heroChoose = new HeroChoose();
                    heroChoose.ShowDialog();
                    (hero as Assyrian).UseMana(Transfer.hero);
                }
                if (hero is SiliCat)
                {
                    HeroChoose heroChoose = new HeroChoose();
                    heroChoose.ShowDialog();
                    (hero as SiliCat).UseMana(Transfer.hero);
                }
                else
                {
                    hero.UseMana();
                }
                Attacked = true;
                IsEnabled = false;
            }
        }
        private void buttonAttack3_Click(object sender, RoutedEventArgs e)
        {
            if (!Attacked)
            {
                if (Hero is Ratatosk)
                {
                    (Hero as Ratatosk).DarkHollow(Transfer.monsters);
                }
                
                if (Hero is Assyrian)
                {
                    (Hero as Assyrian).MantleOfRetribution(Transfer.monsters);
                }
                Attacked = true;
                IsEnabled = false;
                if (Hero is SiliCat)
                {
                    Attacked = false;
                    IsEnabled = true;
                    HeroChoose heroChoose = new HeroChoose();
                    heroChoose.ShowDialog();
                    if (Transfer.hero is Ratatosk)
                    {
                        (Hero as SiliCat).RataHelp((Ratatosk)Transfer.hero);
                        Transfer.helpedRatatosk = true;
                        Attacked = true;
                        IsEnabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Вы выбрали героя не Рататоск");//?
                    }
                }
                if (Hero is Jotun)
                {
                    Attacked = false;
                    IsEnabled = true;
                    MonsterChoose choose = new MonsterChoose();
                    choose.ShowDialog();
                    if (Transfer.monster is LittleFingerer)
                    {
                        (Hero as Jotun).LittleFingerHit((LittleFingerer)Transfer.monster);
                        Attacked = true;
                        IsEnabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Вы выбрали не Мизинчатого");//?
                    }
                }
            }
        }
        private void buttonAttack4_Click(object sender, RoutedEventArgs e)
        {
            if (!Attacked)
            {
                if (Hero is Ratatosk)
                {
                    (Hero as Ratatosk).NutAnger(Transfer.monsters);
                }
                if (Hero is Jotun)
                {
                    (Hero as Jotun).FenrirBrattle(Transfer.monsters);
                }
                if (Hero is Assyrian)
                {
                    (Hero as Assyrian).HealingBreath(Transfer.heroes);
                }
                if (Hero is SiliCat)
                {
                    MonsterChoose choose = new MonsterChoose();
                    choose.ShowDialog();
                    if (Transfer.monster is Ogr)
                    {
                        (Hero as SiliCat).StoneScratch((Ogr)Transfer.monster);
                    }
                }
                Attacked = true;
                IsEnabled = false;
            }
        }
        private void buttonAttack5_Click(object sender, RoutedEventArgs e)
        {
            if (!Attacked)
            {
                if (hero.Ulta == hero.NeededUlta)
                {
                    if (Hero is Ratatosk)
                    {
                        MonsterChoose choose = new MonsterChoose();
                        choose.ShowDialog();
                        (Hero as Ratatosk).PawHit(Transfer.monster, Transfer.heroes[Hero.r.Next(0,Transfer.heroes.Count)]);
                    }
                    if (Hero is Jotun)
                    {
                        (Hero as Jotun).FireCube(Transfer.monsters);
                    }
                    if (Hero is Assyrian)
                    {
                        (Hero as Assyrian).DeathReactor(Transfer.monsters);
                    }
                    if (Hero is SiliCat)
                    {
                        (Hero as SiliCat).SandTsunami(Transfer.monsters);
                    }
                    Attacked = true;
                    IsEnabled = false;
                }
                else
                {
                    MessageBox.Show("Недостаточно ульты!", "Ничего не вышло :(",MessageBoxButton.OK,MessageBoxImage.Asterisk);
                }
            }
        }

        private void buttonAttack1_MouseEnter(object sender, MouseEventArgs e)
        {
            Transfer.wasMouseEnter = true;
            Transfer.wasMouseLeave = false;
            Transfer.infoMouseEnter = hero.FirstAttackInfo;
        }
        private void buttonAttack1_MouseLeave(object sender, MouseEventArgs e)
        {
            Transfer.wasMouseEnter = false;
            Transfer.wasMouseLeave = true;
        }
        private void buttonAttack2_MouseEnter(object sender, MouseEventArgs e)
        {
            Transfer.wasMouseEnter = true;
            Transfer.wasMouseLeave = false;
            Transfer.infoMouseEnter = hero.SecondAttackInfo;
        }
        private void buttonAttack2_MouseLeave(object sender, MouseEventArgs e)
        {
            Transfer.wasMouseEnter = false;
            Transfer.wasMouseLeave = true;
        }
        private void buttonAttack3_MouseEnter(object sender, MouseEventArgs e)
        {
            Transfer.wasMouseEnter = true;
            Transfer.wasMouseLeave = false;
            Transfer.infoMouseEnter = hero.ThirdAttackInfo;
        }
        private void buttonAttack3_MouseLeave(object sender, MouseEventArgs e)
        {
            Transfer.wasMouseEnter = false;
            Transfer.wasMouseLeave = true;
        }
        private void buttonAttack4_MouseEnter(object sender, MouseEventArgs e)
        {
            Transfer.wasMouseEnter = true;
            Transfer.wasMouseLeave = false;
            Transfer.infoMouseEnter = hero.FourthAttackInfo;
        }
        private void buttonAttack4_MouseLeave(object sender, MouseEventArgs e)
        {
            Transfer.wasMouseEnter = false;
            Transfer.wasMouseLeave = true;
        }
        private void buttonAttack5_MouseEnter(object sender, MouseEventArgs e)
        {
            Transfer.wasMouseEnter = true;
            Transfer.wasMouseLeave = false;
            Transfer.infoMouseEnter = hero.FifthAttackInfo;
        }
        private void buttonAttack5_MouseLeave(object sender, MouseEventArgs e)
        {
            Transfer.wasMouseEnter = false;
            Transfer.wasMouseLeave = true;
        }
    }
}
