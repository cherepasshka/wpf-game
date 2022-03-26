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

namespace проект
{
    /// <summary>
    /// Логика взаимодействия для HeroChoose.xaml
    /// </summary>
    public partial class HeroChoose : Window
    {
        public HeroChoose()
        {
            InitializeComponent();
            RadioButton radio;
            int i = 0;
            foreach (Hero m in Transfer.heroes)
            {
                radio = new RadioButton();
                radio.FontSize = 14;
                radio.Content = m.Name;
                canvas.Children.Add(radio);
                Canvas.SetTop(radio, i * 20);
                Canvas.SetLeft(radio, Width / 4);
                ++i;
            }
        }

        private void buttonChoose_Click(object sender, RoutedEventArgs e)
        {
            foreach (RadioButton r in canvas.Children)
            {
                if (r.IsChecked == true)
                {
                    //Monster min = new Monster(1000000,10,10);
                    foreach (Hero m in Transfer.heroes)
                    {
                        if (m.Name == r.Content.ToString())
                        {
                            Transfer.hero = m;
                        }
                    }
                    this.Close();
                    return;
                }
            }
            MessageBox.Show("Вы не выбрали героя, которого надо подлечить", "Выберите героя!", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
