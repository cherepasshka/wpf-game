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

namespace проект
{
    /// <summary>
    /// Логика взаимодействия для MonsterElement.xaml
    /// </summary>
    public partial class MonsterElement : UserControl
    {
        Monster monster;
        public MonsterElement()
        {
            InitializeComponent();
        }
        public Monster Monster
        {
            get { return monster; }
            set
            {
                monster = value;
                GenerateButtonsAndBlock();
            }
        }
        public ImageSource Source
        {
            get
            {
                return image.Source;
            }
            set
            {
                image.Source = value;
            }
        }
        public void Update()
        {
            textBlockInfo.Text = Monster.Info;
        }
        private void GenerateButtonsAndBlock()
        {
            Source = monster.Source;
            textBlockInfo.Text = monster.Info;
        }
    }
}
