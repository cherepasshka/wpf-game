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
using System.IO;
using проект.Characters;

namespace проект
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Random r = new Random();
        List<HeroElement> heroElements = new List<HeroElement>();
        List<MonsterElement> monsterElements = new List<MonsterElement>();
        int round=1, step = 1;
        public MainWindow()
        {
            InitializeComponent();
            GenerateHeroes();
            GenerateMonsters();
            Transfer.hero = null;
            Transfer.monster = null;
            Transfer.records = "Сводка о битве героев игрока "+Transfer.userName+":";
            Height = SystemParameters.PrimaryScreenHeight;
            Width = SystemParameters.PrimaryScreenWidth;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Game();
            easy.IsChecked = true;
        }
        private void mainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (Transfer.wasMouseEnter)
            {
                textBlockInfo.Text = Transfer.infoMouseEnter;
            }
            if (Transfer.wasMouseLeave)
            {
                textBlockInfo.Text = "";
            }
            foreach (HeroElement h in heroElements)
            {
                h.Update();

            }
            foreach (MonsterElement m in monsterElements)
            {
                m.Update();
            }
            Game();
        }
        public void GenerateHeroes()
        {
            heroElements.Add(new HeroElement());
            heroElements.Add(new HeroElement());
            heroElements.Add(new HeroElement());
            heroElements.Add(new HeroElement());
            heroElements[0].Hero = new Ratatosk(300, 100, 85, 0, 200);
            heroElements[1].Hero = new Jotun(350, 100, 90, 0, 200);
            heroElements[2].Hero = new Assyrian(200, 100, 75, 0, 200);
            heroElements[3].Hero = new SiliCat(250, 100, 80, 0, 200);
            Transfer.heroes.Add(heroElements[0].Hero);
            Transfer.heroes.Add(heroElements[1].Hero);
            Transfer.heroes.Add(heroElements[2].Hero);
            Transfer.heroes.Add(heroElements[3].Hero);
            stackPanelHeroes.Children.Add(heroElements[0]);
            stackPanelHeroes.Children.Add(heroElements[1]);
            stackPanelHeroes.Children.Add(heroElements[2]);
            stackPanelHeroes.Children.Add(heroElements[3]);
        }
        public void GenerateMonsters()
        {
            Random r = new Random();
            int num = r.Next(4, 9);
            for (int i = 0; i < num; ++i)
            {
                monsterElements.Add(new MonsterElement());
                switch (r.Next(0, 5))
                {
                    case 0:
                        monsterElements[i].Monster = new Lizard(200, 100, 200);
                        break;
                    case 1:
                        monsterElements[i].Monster = new LittleFingerer(200, 100, 200,10);
                        break;
                    case 2:
                        monsterElements[i].Monster = new Ogr(200, 150, 200);
                        break;
                    case 3:
                        monsterElements[i].Monster = new LavenderMonster(200, 100, 200);
                        break;
                    case 4:
                        monsterElements[i].Monster = new WoodenWolf(200, 100, 200);
                        break;
                    default:
                        break;
                }
                Transfer.monsters.Add(monsterElements[i].Monster);
                canvasMonsters.Children.Add(monsterElements[i]);
                Canvas.SetTop(monsterElements[i], i * monsterElements[i].Height +10);
                if (i > 3)
                {
                    Canvas.SetTop(monsterElements[i], (i-4) * monsterElements[i].Height + 10);
                    Canvas.SetLeft(monsterElements[i], monsterElements[i].Width + 10);
                }
            }
        }
        private void menuItemNewGame_Click(object sender, RoutedEventArgs e)
        {
            Reload();
            hard.IsChecked = easy.IsChecked = middle.IsChecked = false;
            MessageBox.Show("Выберите уровень сложности!");
        }
        private void menuItemRules_Click(object sender, RoutedEventArgs e)
        {
            Rules rules = new Rules();
            rules.Show();
        }
        private void menuItemRecordTable_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Transfer.records,"Таблица рекордов");
        }
        private void menuItemCreateInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Создала приложение Жукова Дарья"+Environment.NewLine+"Обучается в 22 группе", "О создателе");
        }
        public void Game()
        {
            if (Transfer.monsters.Count != 0 && Transfer.heroes.Count != 0)
            {
                for (int i = 0; i < monsterElements.Count; ++i)//обновление изображения не живым монстрам
                {
                    if (monsterElements[i].Monster.Hp == 0)
                    {
                        monsterElements[i].Source = monsterElements[i].Monster.Dead;
                        monsterElements[i].IsEnabled = false;
                        Transfer.monsters.Remove(monsterElements[i].Monster);
                        monsterElements.Remove(monsterElements[i]);
                    }
                }
                for (int i = 0; i < heroElements.Count; ++i)//обновление изображения не живым героям
                {
                    if (heroElements[i].Hero.Hp == 0)
                    {
                        heroElements[i].Source = heroElements[i].Hero.Dead;
                        heroElements[i].IsEnabled = false;
                        Transfer.heroes.Remove(heroElements[i].Hero);
                        heroElements.Remove(heroElements[i]);
                    }
                }
                int c = 0;
                foreach (HeroElement mn in heroElements)
                {
                    if (mn.Hero is Ratatosk && Transfer.helpedRatatosk)//если герою Рататоск помогли, ему разрешается ходить
                    {
                        mn.IsEnabled = true;
                        (mn.Hero as Ratatosk).ActivitiOfAct = true;
                        Transfer.helpedRatatosk = false;
                        mn.Attacked = false;
                    }
                    if (mn.Attacked)//подсчёт тех героев, которые сходили
                    {
                        c++;
                    }
                }
                if (c == heroElements.Count && c!=0)
                {
                    for (int i = 0; i < monsterElements.Count; ++i)
                    {
                        monsterElements[i].Monster.ChooseAttatck();
                        MessageBox.Show(monsterElements[i].Monster.Name + " атаковал " + monsterElements[i].Monster.VictimName,"Атака монстров",MessageBoxButton.OK);
                    }
                    foreach (HeroElement mn in heroElements)//переход возможности атаковать к героям
                    {
                        if (mn.Hero is Ratatosk)
                        {
                            if ((mn.Hero as Ratatosk).ActivitiOfAct)
                            {
                                mn.IsEnabled = true;
                                mn.Attacked = false;
                            }
                        }
                        else
                        {
                            mn.IsEnabled = true;
                            mn.Attacked = false;
                        }
                    }
                    step++;
                }
            }
            else
            {
                if (Transfer.heroes.Count == 0)
                {
                    MessageBox.Show("Победили монстры!", "Проигрыш", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    Transfer.records += "\nГерои проиграли в " + round + "м раунде за " + step + " ходов";
                    if(MessageBox.Show("Хотити начать заново?","",MessageBoxButton.YesNo)== MessageBoxResult.Yes)
                    {
                        Reload();
                        Transfer.records = "Сводка о битве героев игрока " + Transfer.userName + ":";
                    }
                    else
                    {
                        MessageBox.Show(Transfer.records, "Таблица рекордов");
                        StreamWriter writer = new StreamWriter(@"../../records.txt");
                        writer.Write(Transfer.records);
                        writer.Close();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Победили герои!", "Победа", MessageBoxButton.OK, MessageBoxImage.Information);
                    Transfer.records += "\nГерои победили в " + round + "м раунде за " + step + " ходов";
                    int previousRound = round;
                    Reload();
                    round = previousRound + 1;
                    step = 1;
                }
            }
            textBlockCount.Text = "Раунд " + round + ", ход " + step;
        }
        public void Reload()
        {
            step = round = 1;
            Transfer.heroes.Clear();
            Transfer.monsters.Clear();
            heroElements.Clear();
            monsterElements.Clear();
            canvasMonsters.Children.Clear();
            GenerateMonsters();
            foreach (HeroElement h in stackPanelHeroes.Children)
            {
                heroElements.Add(h);
                Transfer.heroes.Add(h.Hero);
                h.Source = h.Hero.Source;
                h.Hero.Hp = h.Hero.MaxHp;
                h.IsEnabled = true;
                h.Attacked = false;
            }
            easy.IsChecked = easy.IsChecked;
            hard.IsChecked = hard.IsChecked;
            middle.IsChecked = middle.IsChecked;
        }
        private void easy_Checked(object sender, RoutedEventArgs e)
        {
            foreach (MonsterElement m in monsterElements)
            {
                if (m.Monster is Lizard)
                {
                    m.Monster.Damage = 35;
                    m.Monster.MaxHp = 110;
                    m.Monster.Hp = 110;
                }
                if (m.Monster is LittleFingerer)
                {
                    m.Monster.Damage = 20;
                    m.Monster.MaxHp = 100;
                    m.Monster.Hp = 100;
                }
                if (m.Monster is Ogr)
                {
                    m.Monster.Damage = 50;
                    m.Monster.MaxHp = 150;
                    m.Monster.Hp = 150;
                }
                if (m.Monster is LavenderMonster)
                {
                    m.Monster.Damage = 30;
                    m.Monster.MaxHp = 120;
                    m.Monster.Hp = 120;
                }
                if (m.Monster is WoodenWolf)
                {
                    m.Monster.Damage = 40;
                    m.Monster.MaxHp = 120;
                    m.Monster.Hp = 120;
                }
            }
            foreach (HeroElement h in heroElements)
            {
                if (h.Hero is Ratatosk)
                {
                    h.Hero.MaxHp = 190;
                    h.Hero.Hp = 190; 
                    h.Hero.Damage = 70;
                    h.Hero.MaxMana = 50;
                    h.Hero.Mana = 50;
                }
                if (h.Hero is Assyrian)
                {
                    h.Hero.MaxHp = 180;
                    h.Hero.Hp = 180;
                    h.Hero.Damage = 65;
                    h.Hero.MaxMana = 90;
                    h.Hero.Mana = 90;
                }
                if (h.Hero is Jotun)
                {
                    h.Hero.MaxHp = 250;
                    h.Hero.Hp = 250;
                    h.Hero.Damage = 95;
                    h.Hero.MaxMana = 50;
                    h.Hero.Mana = 50;
                }
                if(h.Hero is SiliCat)
                {
                    h.Hero.MaxHp = 160;
                    h.Hero.Hp = 160;
                    h.Hero.Damage = 65;
                    h.Hero.MaxMana = 50;
                    h.Hero.Mana = 50;
                }
            }
            middle.IsChecked = hard.IsChecked= false;
            Transfer.maxDamage = 70;
        }
        private void middle_Checked(object sender, RoutedEventArgs e)
        {
            foreach (MonsterElement m in monsterElements)
            {
                if (m.Monster is Lizard)
                {
                    m.Monster.Damage = 35;
                    m.Monster.MaxHp = 140;
                    m.Monster.Hp = 140;
                }
                if (m.Monster is LittleFingerer)
                {
                    m.Monster.Damage = 20;
                    m.Monster.MaxHp = 130;
                    m.Monster.Hp = 130;
                }
                if (m.Monster is Ogr)
                {
                    m.Monster.Damage = 50;
                    m.Monster.MaxHp = 180;
                    m.Monster.Hp = 180;
                }
                if (m.Monster is LavenderMonster)
                {
                    m.Monster.Damage = 30;
                    m.Monster.MaxHp = 150;
                    m.Monster.Hp = 150;
                }
                if (m.Monster is WoodenWolf)
                {
                    m.Monster.Damage = 40;
                    m.Monster.MaxHp = 150;
                    m.Monster.Hp = 150;
                }
            }
            foreach (HeroElement h in heroElements)
            {
                if (h.Hero is Ratatosk)
                {
                    h.Hero.MaxHp = 190;
                    h.Hero.Hp = 190;
                    h.Hero.Damage = 70;
                    h.Hero.MaxMana = 50;
                    h.Hero.Mana = 50;
                }
                if (h.Hero is Assyrian)
                {
                    h.Hero.MaxHp = 180;
                    h.Hero.Hp = 180;
                    h.Hero.Damage = 65;
                    h.Hero.MaxMana = 90;
                    h.Hero.Mana = 90;
                }
                if (h.Hero is Jotun)
                {
                    h.Hero.MaxHp = 250;
                    h.Hero.Hp = 250;
                    h.Hero.Damage = 95;
                    h.Hero.MaxMana = 50;
                    h.Hero.Mana = 50;
                }
                if (h.Hero is SiliCat)
                {
                    h.Hero.MaxHp = 160;
                    h.Hero.Hp = 160;
                    h.Hero.Damage = 65;
                    h.Hero.MaxMana = 50;
                    h.Hero.Mana = 50;
                }
            }
            easy.IsChecked = hard.IsChecked = false;
            Transfer.maxDamage = 80;
        }
        private void hard_Checked(object sender, RoutedEventArgs e)
        {
            foreach (MonsterElement m in monsterElements)
            {
                if (m.Monster is Lizard)
                {
                    m.Monster.Damage = 50;
                    m.Monster.MaxHp = 140;
                    m.Monster.Hp = 140;
                }
                if (m.Monster is LittleFingerer)
                {
                    m.Monster.Damage = 35;
                    m.Monster.MaxHp = 130;
                    m.Monster.Hp = 130;
                }
                if (m.Monster is Ogr)
                {
                    m.Monster.Damage = 65;
                    m.Monster.MaxHp = 180;
                    m.Monster.Hp = 180;
                }
                if (m.Monster is LavenderMonster)
                {
                    m.Monster.Damage = 45;
                    m.Monster.MaxHp = 150;
                    m.Monster.Hp = 150;
                }
                if (m.Monster is WoodenWolf)
                {
                    m.Monster.Damage = 55;
                    m.Monster.MaxHp = 150;
                    m.Monster.Hp = 150;
                }
            }
            foreach (HeroElement h in heroElements)
            {
                if (h.Hero is Ratatosk)
                {
                    h.Hero.MaxHp = 190;
                    h.Hero.Hp = 190;
                    h.Hero.Damage = 70;
                    h.Hero.MaxMana = 50;
                    h.Hero.Mana = 50;
                }
                if (h.Hero is Assyrian)
                {
                    h.Hero.MaxHp = 180;
                    h.Hero.Hp = 180;
                    h.Hero.Damage = 65;
                    h.Hero.MaxMana = 90;
                    h.Hero.Mana = 90;
                }
                if (h.Hero is Jotun)
                {
                    h.Hero.MaxHp = 250;
                    h.Hero.Hp = 250;
                    h.Hero.Damage = 95;
                    h.Hero.MaxMana = 50;
                    h.Hero.Mana = 50;
                }
                if (h.Hero is SiliCat)
                {
                    h.Hero.MaxHp = 160;
                    h.Hero.Hp = 160;
                    h.Hero.Damage = 65;
                    h.Hero.MaxMana = 50;
                    h.Hero.Mana = 50;
                }
            }
            middle.IsChecked = easy.IsChecked = false;
            Transfer.maxDamage = 100;
        }
    }
}