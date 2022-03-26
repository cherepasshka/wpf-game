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
using System.Windows.Shapes;
using проект.Characters;
namespace проект
{
    /// <summary>
    /// Логика взаимодействия для Rules.xaml
    /// </summary>
    public partial class Rules : Window
    {
        public Rules()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            heroElementRatatosk.Hero = new Ratatosk(100, 200, 30, 0, 100);
            heroElementJotun.Hero = new Jotun(300, 100, 50, 0, 50);
            heroElementAssyrian.Hero = new Assyrian(150, 250, 20, 0, 100);
            heroElementSiliCat.Hero = new SiliCat(200,100, 40, 0, 150);
            monsterElementLizard.Monster = new Lizard(100, 30, 100);
            monsterElementLittleFingerer.Monster = new LittleFingerer(200, 40, 120, 10);
            monsterElementOgr.Monster = new Ogr(150, 50, 120);
            monsterElementLavanderMonstr.Monster = new LavenderMonster(190, 55, 130);
            monsterElementWoodenWolf.Monster = new WoodenWolf(150, 45, 125);
        }

        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex++;
        }
    }
}
