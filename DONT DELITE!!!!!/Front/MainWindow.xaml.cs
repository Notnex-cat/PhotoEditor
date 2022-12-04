using DONT_DELITE_____.Back;
using Microsoft.Graph;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DONT_DELITE_____
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Bitmap> bitmapList = new List<Bitmap>();
        public Bitmap currentPicture;
        string way;
        private int currentBitmap = 0;
        public Bitmap MWImg;
        
        public MainWindow()
        {
            InitializeComponent();
            //DataContext = new VM(imgPhoto);
        }

        #region Clicks
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            FileService fs = new FileService();
            addPicture(fs.OpenPhoto(this));
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bitmapList.Count > 0)
                {
                    FileService fs = new FileService();
                    fs.SavePhoto(this, bitmapList[currentBitmap]);
                }
                else
                {
                    MessageBox.Show("Откройте картинку");
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
        private void Invert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bitmapList.Count > 0)
                {
                    Effects ef = new Effects();
                    currentPicture = bitmapList[currentBitmap];
                    addPicture(ef.Invert(this, currentPicture));
                }
                else
                {
                    MessageBox.Show("Откройте картинку");
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
        private void Gray_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bitmapList.Count > 0)
                {
                    Effects ef = new Effects();
                    currentPicture = bitmapList[currentBitmap];
                    addPicture(ef.Gray(this, currentPicture));
                }
                else
                {
                    MessageBox.Show("Откройте картинку");
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
        private void Gaus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bitmapList.Count > 0)
                {
                    int i = Int32.Parse(g.Text);
                    currentPicture = bitmapList[currentBitmap];
                    //GaussBlur gb = new GaussBlur();
                    //gb.Show();
                    GausBlur1(i);
                }
                else
                {
                    MessageBox.Show("Откройте картинку");
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
        private void Fog_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bitmapList.Count > 0)
                {
                    Effects ef = new Effects();
            currentPicture = bitmapList[currentBitmap];
            addPicture(ef.Fog(this, currentPicture));
                }
                else
                {
                    MessageBox.Show("Откройте картинку");
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
        private void Flash_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bitmapList.Count > 0)
                {
                    Effects ef = new Effects();
                    currentPicture = bitmapList[currentBitmap];
                    addPicture(ef.Flash(this, currentPicture));
                }
                else
                {
                    MessageBox.Show("Откройте картинку");
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
        private void Frozen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bitmapList.Count > 0)
                {
                    Effects ef = new Effects();
                    currentPicture = bitmapList[currentBitmap];
                    addPicture(ef.Frozen(this, currentPicture));
                }
                else
                {
                    MessageBox.Show("Откройте картинку");
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
        private void Arctic_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bitmapList.Count > 0)
                {
                    Effects ef = new Effects();
                    currentPicture = bitmapList[currentBitmap];
                    addPicture(ef.Arctic(this, currentPicture));
                }
                else
                {
                    MessageBox.Show("Откройте картинку");
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
        private void Sepia_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bitmapList.Count > 0)
                {
                    Effects ef = new Effects();
                    currentPicture = bitmapList[currentBitmap];
                    addPicture(ef.Sepia(this, currentPicture));
                }
                else
                {
                    MessageBox.Show("Откройте картинку");
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
        private void Kakao_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bitmapList.Count > 0)
                {
                    Effects ef = new Effects();
                    currentPicture = bitmapList[currentBitmap];
                    addPicture(ef.Kakao(this, currentPicture));
                }
                else
                {
                    MessageBox.Show("Откройте картинку");
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
        private void Cuji_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bitmapList.Count > 0)
                {
                    Effects ef = new Effects();
                    currentPicture = bitmapList[currentBitmap];
                    addPicture(ef.Cuji(this, currentPicture));
                }
                else
                {
                    MessageBox.Show("Откройте картинку");
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
        private void Dramatic_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bitmapList.Count > 0)
                {
                    Effects ef = new Effects();
                    currentPicture = bitmapList[currentBitmap];
                    addPicture(ef.Dramatic(this, currentPicture));
                }
                else
                {
                    MessageBox.Show("Откройте картинку");
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
        private void Otraz_Click(object sender, RoutedEventArgs e)//vertical
        {
            try
            {
                if (bitmapList.Count > 0)
                {
                    currentPicture = bitmapList[currentBitmap];
                    currentPicture.RotateFlip(RotateFlipType.Rotate180FlipY);
                    addPicture(currentPicture);
                }
                else
                {
                    MessageBox.Show("Откройте картинку");
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
        private void Rotate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bitmapList.Count > 0)
                {
                    int number = Int32.Parse(angle.Text);
                    MainWindow mainWindow = new MainWindow();
                    Ron(number);
                }
                else
                {
                    MessageBox.Show("Откройте картинку");
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
        private void Otraz1_Click(object sender, RoutedEventArgs e)
        {
            currentPicture = bitmapList[currentBitmap];
            currentPicture.RotateFlip(RotateFlipType.Rotate180FlipX);
            addPicture(currentPicture);
        }
        private void crop_Click(object sender, RoutedEventArgs e)
        {
            CropPirture();
        }
        private void RedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                if (bitmapList.Count > 0)
                {
                    Effects ef = new Effects();
                    addPicture(ef.Sliders(this, bitmapList[0]));
                }
                else
                {
                    MessageBox.Show("Откройте картинку");
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
        private void GreenSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                if (bitmapList.Count > 0)
                {
                    Effects ef = new Effects();
                    addPicture(ef.Sliders(this, bitmapList[0]));
                }
                else
                {
                    MessageBox.Show("Откройте картинку");
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
        private void BlueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                if (bitmapList.Count > 0)
                {
                    Effects ef = new Effects();
                    addPicture(ef.Sliders(this, bitmapList[0]));
                }
                else
                {
                    MessageBox.Show("Откройте картинку");
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
        private void TransSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                if (bitmapList.Count > 0)
                {
                    Effects ef = new Effects();
                    addPicture(ef.Sliders(this, bitmapList[0]));
                }
                else
                {
                    MessageBox.Show("Откройте картинку");
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
        private void LightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                if (bitmapList.Count > 0)
                {
                    Effects ef = new Effects();
                    addPicture(ef.Sliders(this, bitmapList[0]));
                }
                else
                {
                    MessageBox.Show("Откройте картинку");
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
        private void ContrSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                if (bitmapList.Count > 0)
                {
                    Effects ef = new Effects();
                    addPicture(ef.Sliders(this, bitmapList[0]));
                }
                else
                {
                    //MessageBox.Show("Откройте картинку");
                }
            }
            catch (Exception except)
            {
                //MessageBox.Show(except.Message);
            }
        }
        private void SaturSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                if (bitmapList.Count > 0)
                {
                    Effects ef = new Effects();
                    addPicture(ef.Sliders(this, bitmapList[0]));
                }
                else
                {
                    //MessageBox.Show("Откройте картинку");
                }
            }
            catch (Exception except)
            {
                //MessageBox.Show(except.Message);
            }
        }
        private void reload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bitmapList.Count > 0)
                {
                    setMainPicture(0);
                }
                else
                {
                    MessageBox.Show("Откройте картинку");
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
        private void redo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bitmapList.Count > 0)
                {
                    redoPicture();
                }
                else
                {
                    MessageBox.Show("Откройте картинку");
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
        private void undo_Click(object sender, RoutedEventArgs e)
        {
            undoPicture();
        }
        #endregion

        #region Functions
        public void setMainPicture(int currentState)
        {
            imgPhoto.Source = BitmapToBitmapSource(bitmapList[currentState]);
            currentBitmap = currentState;
        }
        public int CurrentBitmap
        {
            get { return currentBitmap; }
            set { currentBitmap = value; }
        }
        public void redoPicture()
        {
            if (currentBitmap < bitmapList.Count - 1)
            {
                currentBitmap++;
                setMainPicture(currentBitmap);
            }
        }
        public void Ron(int i)
        {
            RotateTransform rotateTransform = new RotateTransform(i);
            imgPhoto.RenderTransform = rotateTransform;
            System.Windows.Point p = new System.Windows.Point(0.5, 0.5);
            imgPhoto.RenderTransformOrigin = p;
            //RenderSize rs = new RenderSize(currentPicture.Width, currentPicture.Height);



            //imgPhoto.TranslateTransform(originX, originY);
            //Back.Rotation r = new Back.Rotation();
            //Console.WriteLine(bitmapList.Count);
            //Console.WriteLine(currentBitmap);
            //addPicture(r.Rotat(this, i, bitmapList[currentBitmap]));
        }
        public void GausBlur1(int i)
        {
            Bitmap img = currentPicture;
            GaussianBlur gaussianBlur = new GaussianBlur(this, img);
            addPicture(gaussianBlur.Process(i));
        }
        public void undoPicture()
        {
            if (currentBitmap > 0)
            {
                currentBitmap--;
                setMainPicture(currentBitmap);
            }
        }
        public void setTempPicture(Bitmap aBitmap)
        {
            imgPhoto.Source = BitmapToBitmapSource(aBitmap);
        }
        public void addPicture(Bitmap aBitmap)
        {
            bitmapList.Add(aBitmap);
            imgPhoto.Source = BitmapToBitmapSource(aBitmap);
            currentBitmap = bitmapList.Count - 1;
        }
        public static BitmapSource BitmapToBitmapSource(Bitmap source)
        {
            BitmapSource bitSrc = null;
            var hBitmap = source.GetHbitmap();

            try
            {
                bitSrc = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    hBitmap,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
            }
            catch (Win32Exception)
            {
                bitSrc = null;
            }
            finally
            {
                NativeMethods.DeleteObject(hBitmap);
            }
            return bitSrc;
        }
        internal static class NativeMethods
        {
            [DllImport("gdi32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool DeleteObject(IntPtr hObject);
        }
        private void CropPirture()
        {
            try
            {
                if (bitmapList.Count > 0)
                {
                    //DONT_DELITE_____.ColorDialog greyWindow = new DONT_DELITE_____.ColorDialog(this, "Crop");
                }
                else
                {
                    MessageBox.Show("Откройте картинку");
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
        #endregion
        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Fill = System.Windows.Media.Brushes.Sienna;
            ellipse.Width = 100;
            ellipse.Height = 100;
            ellipse.StrokeThickness = 2;

            InkCanvas.Children.Add(ellipse);

            Canvas.SetLeft(ellipse, e.GetPosition(imgPhoto).X);
            Canvas.SetTop(ellipse, e.GetPosition(imgPhoto).Y);
            Console.WriteLine("yytouiy");
        }
    }
}
