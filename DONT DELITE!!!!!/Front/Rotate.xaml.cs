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

namespace DONT_DELITE_____.Front
{
    /// <summary>
    /// Логика взаимодействия для Rotate.xaml
    /// </summary>
    public partial class Rotate : Window
    {
        public Rotate()
        {
            InitializeComponent();
        }

        private void RotateOk_Click(object sender, RoutedEventArgs e)
        {
            int number = Int32.Parse(angle.Text);
            MainWindow mainWindow = new MainWindow();
            mainWindow.Rotation(number);
        }
    }
}
