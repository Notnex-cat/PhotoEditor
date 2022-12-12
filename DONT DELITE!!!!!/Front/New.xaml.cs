using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DONT_DELITE_____.Front
{
    /// <summary>
    /// Логика взаимодействия для New.xaml
    /// </summary>
    public partial class New : Window
    {
        public int height;
        public int width;
        public New()
        {
            InitializeComponent();
        }
   
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Start s = new Start();
            s.CloseW();
            height = int.Parse(Height.Text);
            width = int.Parse(Width.Text);
            MainWindow mainWindow = new MainWindow();
            mainWindow.NewLayer(width, height);
            mainWindow.Show();
            
            Close();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
