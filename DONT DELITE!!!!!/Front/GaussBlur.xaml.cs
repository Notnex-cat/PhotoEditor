using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Логика взаимодействия для GaussBlur.xaml
    /// </summary>
    public partial class GaussBlur : Window
    {
        public GaussBlur()
        {
            InitializeComponent();
        }

        private void BlurOk_Click(object sender, RoutedEventArgs e)
        {
            int number = Int32.Parse(angle.Text);
            MainWindow mainWindow = new MainWindow();
            mainWindow.GausBlur1(number);
        }

        private void GB(int i, MainWindow mainWindowCls, Bitmap currentPicture)
        {
            
        }
    }
}
