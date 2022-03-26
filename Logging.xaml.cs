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
    /// Логика взаимодействия для Logging.xaml
    /// </summary>
    public partial class Logging : Window
    {
        MainWindow mainWindow;
        public Logging()
        {
            InitializeComponent();
        }
        private void buttonToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxName.Text != "")
            {
                Transfer.userName = textBoxName.Text;
                mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
                Rules r = new Rules();
                r.ShowDialog();

            }
            else
            {
                MessageBox.Show("Заполните поле для имени!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void textBoxName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key>=Key.D0 && e.Key <= Key.D9 || e.Key>=Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                MessageBox.Show("Имя не должно содержать цифры!","Предупреждение",MessageBoxButton.OK,MessageBoxImage.Information);
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }
    }
}
