using DONT_DELITE_____.Front;
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

namespace DONT_DELITE_____
{
    /// <summary>
    /// Логика взаимодействия для Start.xaml
    /// </summary>
    public partial class Start : Window
    {
        public Start()
        {
            InitializeComponent();
        }
        
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Open_Click(sender, e);
            mainWindow.Show();
            Close();
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            New n = new New();
            n.Show();
        }
        public void CloseW()
        {
            Close();
        }
    }
}
