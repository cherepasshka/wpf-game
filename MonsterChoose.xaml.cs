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
    /// Логика взаимодействия для MonsterChoose.xaml
    /// </summary>
    public partial class MonsterChoose : Window
    {
        public MonsterChoose()
        {
            InitializeComponent();
            RadioButton radio;
            int i = 0;
            foreach (Monster m in Transfer.monsters)
            {
                radio = new RadioButton();
                radio.FontSize = 14;
                radio.Content = m.Name;
                canvas.Children.Add(radio);
                Canvas.SetTop(radio, i * 20);
                Canvas.SetLeft(radio, Width /3 );
                ++i;
            }
        }

        private void buttonChoose_Click(object sender, RoutedEventArgs e)
        {
            foreach(RadioButton r in canvas.Children)
            {
                if (r.IsChecked==true)
                {
                    foreach(Monster m in Transfer.monsters)
                    {
                        if (m.Name == r.Content.ToString())
                        {
                            Transfer.monster = m;
                        }
                    }
                    this.Close();
                    return;
                }
            }
            MessageBox.Show("Вы не выбрали монстра, которого надо атаковать","Выберите монстра!", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
