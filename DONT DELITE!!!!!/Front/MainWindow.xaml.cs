using DONT_DELITE_____.Back;
using Microsoft.Graph;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;
using static System.Net.Mime.MediaTypeNames;
using Brushes = System.Windows.Media.Brushes;
using Color = System.Windows.Media.Color;
using MessageBox = System.Windows.MessageBox;
using Size = System.Windows.Size;

namespace DONT_DELITE_____
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Bitmap> bitmapList = new List<Bitmap>();
        public Bitmap currentPicture;
        public Bitmap effectsPicture;//костыль для криво работающих эффектов
        private int currentBitmap = 0;
        public Bitmap MWImg;
        private bool PenVisible = false; //кнопочка рисовать

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Clicks
        public void Open_Click(object sender, RoutedEventArgs e)
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
        private void SavePicture(object sender, RoutedEventArgs e)
        {
            SaveCanvas(MainCanvas, 0);
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
                    addPicture(ef.Sliders(this, effectsPicture));
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
                    addPicture(ef.Sliders(this, effectsPicture));
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
                    addPicture(ef.Sliders(this, effectsPicture));
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
                    addPicture(ef.Sliders(this, effectsPicture));
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
                    addPicture(ef.Sliders(this, effectsPicture));
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
                    addPicture(ef.Sliders(this, effectsPicture));
                }
                else
                {
                    //MessageBox.Show("Откройте картинку");
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
        private void SaturSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                if (bitmapList.Count > 0)
                {
                    Effects ef = new Effects();
                    addPicture(ef.Sliders(this, effectsPicture));
                }
                else
                {
                    //MessageBox.Show("Откройте картинку");
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
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
        void colorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            InkCanvas.DefaultDrawingAttributes.Color = (Color)colorPicker.SelectedColor;
        }
        private void Width_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                if (bitmapList.Count > 0)
                {
                    InkCanvas.DefaultDrawingAttributes.Width = Width_Slider.Value;
                }
                else
                {
                    //MessageBox.Show("Откройте картинку");
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
        private void Height_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                if (bitmapList.Count > 0)
                {
                    InkCanvas.DefaultDrawingAttributes.Height = Height_Slider.Value;
                }
                else
                {
                    //MessageBox.Show("Откройте картинку");
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
        private void Pen_Click(object sender, RoutedEventArgs e)
        {
            if(PenVisible == false)
            {
                InkCanvas.Visibility = Visibility.Visible;
                PenVisible = true;
            }
            else {
                InkCanvas.Visibility = Visibility.Hidden;
                PenVisible = false;
            }
        }
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabFilter.IsSelected)
            {
                effectsPicture = bitmapList[currentBitmap];
                addPicture(effectsPicture);
            }
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
        private void SaveCanvas(Canvas canvas, int dpi)
        {
            var width = bitmapList[currentBitmap].Width;
            var height = bitmapList[currentBitmap].Height;

            var size = new Size(width, height);
            canvas.Measure(size);

            var rtb = new RenderTargetBitmap(
                (int)width,
                (int)height,
                dpi, //dpi x 
                dpi, //dpi y 
                PixelFormats.Pbgra32 // pixelformat 
                );
            rtb.Render(canvas);

            SaveAsPng(rtb);
        }
        private void SaveAsPng(RenderTargetBitmap bmp)
        {
            addPicture(RTBtoB(bmp));
            this.InkCanvas.Strokes.Clear();
        }
        private Bitmap RTBtoB(RenderTargetBitmap bmp)
        {
            MemoryStream stream = new MemoryStream();
            BitmapEncoder encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            encoder.Save(stream);
            Bitmap bitmap = new Bitmap(stream);
            return bitmap;
        }


        public void NewLayer(int PixelWidth, int PixelHeight)
        {
            RenderTargetBitmap bitmap = new RenderTargetBitmap(PixelWidth, PixelHeight, 96, 96, PixelFormats.Pbgra32);
            Effects ef = new Effects();
            currentPicture = RTBtoB(bitmap);
            addPicture(ef.Invert(this, currentPicture));

        }

        #endregion

        private void New_Click(object sender, RoutedEventArgs e)
        {
            NewLayer(400, 300);
        }
    }
}