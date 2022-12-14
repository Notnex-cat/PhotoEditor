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
using System.Linq;
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
        public int currentBitmap = 0;
        public Bitmap MWImg;
        private bool PenVisible = false; //кнопочка рисовать
        VignetteEffect vignette;
        VignetteShape shape;
        Color colour;

        // Lists of red, green and blue pixels in original image (unscaled).
        List<byte> pixels8Red;
        List<byte> pixels8Green;
        List<byte> pixels8Blue;

        // Lists of red, green and blue pixels in modified image (unscaled).
        List<byte> pixels8RedModified;
        List<byte> pixels8GreenModified;
        List<byte> pixels8BlueModified;

        // Lists of red, green and blue pixels in original image (scaled).
        List<byte> pixels8RedScaled;
        List<byte> pixels8GreenScaled;
        List<byte> pixels8BlueScaled;

        // Lists of red, green and blue pixels in modified image (scaled).
        List<byte> pixels8RedScaledModified;
        List<byte> pixels8GreenScaledModified;
        List<byte> pixels8BlueScaledModified;

        BitmapSource originalImage;      // Bitmap for the original image.
        BitmapSource newImage;           // Bitmap for the scaled image.
        TransformedBitmap scaledImage;   // Bitmap for the scaled image.

        // Tried to use a List<byte> for this, but found that BitmapSource.CopyPixels does not take 
        // this data type as one of its arguments.
        byte[] originalPixels;
        byte[] scaledPixels;

        int originalWidth, originalHeight;
        int viewportWidthHeight = 600;
        int scaledWidth;
        int scaledHeight;
        string fileName;

        byte red, green, blue;
        double scaleFactor;

        public MainWindow()
        {
            InitializeComponent();
            pixels8Red = new List<byte>();
            pixels8Green = new List<byte>();
            pixels8Blue = new List<byte>();

            pixels8RedModified = new List<byte>();
            pixels8GreenModified = new List<byte>();
            pixels8BlueModified = new List<byte>();

            pixels8RedScaled = new List<byte>();
            pixels8GreenScaled = new List<byte>();
            pixels8BlueScaled = new List<byte>();

            pixels8RedScaledModified = new List<byte>();
            pixels8GreenScaledModified = new List<byte>();
            pixels8BlueScaledModified = new List<byte>();

            scaleFactor = 1.0;

            // Magic numbers to represent the starting colour - predominantly blue
            red = 20;
            green = 20;
            blue = 240;
            colour = new Color();
            colour = Color.FromRgb(red, green, blue);
            

            comboTechnique.SelectedIndex = 1; // Select the ellipse shape
            vignette = null;
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
        void colorPickerV_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            colour = (Color)colorPickerV.SelectedColor;
               

                if (vignette != null)
                {
                    vignette.BorderColour = colour;
                    vignette.ApplyEffect(this);
                }
            
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
                PenButton.Content = Resources["Cursor"];
            }
            else {
                InkCanvas.Visibility = Visibility.Hidden;
                PenVisible = false;
                PenButton.Content = Resources["Pen"];
            }
        }
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabFilter.IsSelected)
            {
                effectsPicture = bitmapList[currentBitmap];
                addPicture(effectsPicture);
            }
            if (tabVignette.IsSelected)
            {
                ReadImage();
                ComputeScaledWidthAndHeight();
                ScaleImage();
                PopulatePixelsOriginalAndScaled();
                ApplyVignette();
            }
        }
        //private void RedSliderVignette_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    try
        //    {
        //        if (bitmapList.Count > 0)
        //        {
        //            Сorrection cor = new Сorrection();
        //            addPicture(cor.PaintVignette(this, bitmapList[currentBitmap]));
        //        }
        //        else
        //        {
        //            MessageBox.Show("Откройте картинку");
        //        }
        //    }
        //    catch (Exception except)
        //    {
        //        MessageBox.Show(except.Message);
        //    }
        //}
        //private void GreenSliderVignette_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    try
        //    {
        //        if (bitmapList.Count > 0)
        //        {
        //            Сorrection cor = new Сorrection();
        //            addPicture(cor.PaintVignette(this, bitmapList[currentBitmap]));
        //        }
        //        else
        //        {
        //            MessageBox.Show("Откройте картинку");
        //        }
        //    }
        //    catch (Exception except)
        //    {
        //        MessageBox.Show(except.Message);
        //    }
        //}
        //private void BlueSliderVignette_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    try
        //    {
        //        if (bitmapList.Count > 0)
        //        {
        //            Сorrection cor = new Сorrection();
        //            addPicture(cor.PaintVignette(this, bitmapList[currentBitmap]));
        //        }
        //        else
        //        {
        //            MessageBox.Show("Откройте картинку");
        //        }
        //    }
        //    catch (Exception except)
        //    {
        //        MessageBox.Show(except.Message);
        //    }
        //}
        private void New_Click(object sender, RoutedEventArgs e)
        {
            NewLayer(400, 300);
        }
        private void comboTechnique_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sliderAngle.IsEnabled = true;
            if (comboTechnique.SelectedIndex == 0)
            {
                shape = VignetteShape.Circle;
                sliderAngle.IsEnabled = false;
            }
            else if (comboTechnique.SelectedIndex == 1)
            {
                shape = VignetteShape.Ellipse;
            }
            else if (comboTechnique.SelectedIndex == 2)
            {
                shape = VignetteShape.Diamond;
            }
            else if (comboTechnique.SelectedIndex == 3)
            {
                shape = VignetteShape.Square;
            }
            else //if(comboTechnique.SelectedIndex == 4)
            {
                shape = VignetteShape.Rectangle;
            }

            if (vignette != null)
            {
                vignette.Shape = shape;
                vignette.ApplyEffect(this);
            }
        }

        private void sliderAngle_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (vignette != null)
            {
                vignette.Angle = sliderAngle.Value;
                vignette.ApplyEffect(this);
            }
        }

        private void sliderPercent_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (vignette != null)
            {
                vignette.Coverage = sliderPercent.Value;
                vignette.ApplyEffect(this);
            }
        }

        private void sliderBand_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (vignette != null)
            {
                vignette.BandPixels = Convert.ToInt32(sliderBand.Value);
                vignette.ApplyEffect(this);
            }
        }

        private void sliderOriginX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (vignette != null)
            {
                vignette.Xcentre = Convert.ToInt32(sliderOriginX.Value);
                vignette.ApplyEffect(this);
            }
        }

        private void sliderOriginY_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (vignette != null)
            {
                vignette.Ycentre = Convert.ToInt32(sliderOriginY.Value);
                vignette.ApplyEffect(this);
            }
        }

        private void sliderSteps_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (vignette != null)
            {
                vignette.NumberSteps = Convert.ToInt32(sliderSteps.Value);
                vignette.ApplyEffect(this);
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

        private void applyV_Click(object sender, RoutedEventArgs e)
        {
            VignetteEffect vig = new VignetteEffect(this);
            vig.Angle = sliderAngle.Value;
            vig.Coverage = sliderPercent.Value;

            vig.BandPixels = Convert.ToInt32(sliderBand.Value / scaleFactor);
            vig.NumberSteps = Convert.ToInt32(sliderSteps.Value / scaleFactor);
            vig.Xcentre = Convert.ToInt32(sliderOriginX.Value);
            vig.Ycentre = Convert.ToInt32(sliderOriginY.Value);
            vig.BorderColour = colour;
            vig.Shape = shape;

            Mouse.OverrideCursor = Cursors.Wait;
            vig.TransferImagePixels(ref pixels8Red, ref pixels8Green, ref pixels8Blue,
                    originalWidth, originalHeight,
                    ref pixels8RedModified, ref pixels8GreenModified, ref pixels8BlueModified,
                    ModeOfOperation.SaveMode);
            vig.SaveImage();
            
        }

        void ScaleImage()
        {
            double fac1 = Convert.ToDouble(scaledWidth / (1.0 * originalWidth));
            double fac2 = Convert.ToDouble(scaledHeight / (1.0 * originalHeight));
            scaleFactor = Math.Min(fac1, fac2);
            var scale = new ScaleTransform(fac1, fac2);
            scaledImage = new TransformedBitmap(originalImage, scale);

            imgPhoto.Source = scaledImage;

            int stride = (scaledImage.PixelWidth * scaledImage.Format.BitsPerPixel + 7) / 8;
            scaledPixels = new byte[stride * scaledHeight];

            // Update the array scaledPixels from the scaled image
            scaledImage.CopyPixels(Int32Rect.Empty, scaledPixels, stride, 0);
        }
        private bool ReadImage()
        {
            bool retVal = false;
            // Open the image
            originalImage = BitmapToBitmapSource(bitmapList[currentBitmap]);
            int stride = (originalImage.PixelWidth * originalImage.Format.BitsPerPixel + 7) / 8;
            originalWidth = originalImage.PixelWidth;
            originalHeight = originalImage.PixelHeight;

            if ((originalImage.Format == PixelFormats.Bgra32) ||
                (originalImage.Format == PixelFormats.Bgr32))
            {
                originalPixels = new byte[stride * originalHeight];
                // Read in pixel values from the image
                originalImage.CopyPixels(Int32Rect.Empty, originalPixels, stride, 0);
                
                retVal = true;
            }
            else
            {
                MessageBox.Show("Sorry, I don't support this image format.");
            }

            return retVal;
        }

        private void ComputeScaledWidthAndHeight()
        {
            if (originalWidth > originalHeight)
            {
                scaledWidth = viewportWidthHeight;
                scaledHeight = originalHeight * viewportWidthHeight / originalWidth;
            }
            else
            {
                scaledHeight = viewportWidthHeight;
                scaledWidth = originalWidth * viewportWidthHeight / originalHeight;
            }
        }
        private void PopulatePixelsOriginalAndScaled()
        {
            int bitsPerPixel = originalImage.Format.BitsPerPixel;

            if (bitsPerPixel == 24 || bitsPerPixel == 32)
            {
                byte red, green, blue;

                pixels8Red.Clear();
                pixels8Green.Clear();
                pixels8Blue.Clear();

                pixels8RedModified.Clear();
                pixels8GreenModified.Clear();
                pixels8BlueModified.Clear();

                pixels8RedScaled.Clear();
                pixels8GreenScaled.Clear();
                pixels8BlueScaled.Clear();

                pixels8RedScaledModified.Clear();
                pixels8GreenScaledModified.Clear();
                pixels8BlueScaledModified.Clear();

                // Populate the Red, Green and Blue lists.
                if (bitsPerPixel == 24) // 24 bits per pixel
                {
                    for (int i = 0; i < scaledPixels.Count(); i += 3)
                    {
                        // In a 24-bit per pixel image, the bytes are stored in the order 
                        // BGR - Blue Green Red order.
                        blue = (byte)(scaledPixels[i]);
                        green = (byte)(scaledPixels[i + 1]);
                        red = (byte)(scaledPixels[i + 2]);

                        pixels8RedScaled.Add(red);
                        pixels8GreenScaled.Add(green);
                        pixels8BlueScaled.Add(blue);

                        pixels8RedScaledModified.Add(red);
                        pixels8GreenScaledModified.Add(green);
                        pixels8BlueScaledModified.Add(blue);
                    }

                    for (int i = 0; i < originalPixels.Count(); i += 3)
                    {
                        // In a 24-bit per pixel image, the bytes are stored in the order 
                        // BGR - Blue Green Red order.
                        blue = (byte)(originalPixels[i]);
                        green = (byte)(originalPixels[i + 1]);
                        red = (byte)(originalPixels[i + 2]);

                        pixels8Red.Add(red);
                        pixels8Green.Add(green);
                        pixels8Blue.Add(blue);

                        pixels8RedModified.Add(red);
                        pixels8GreenModified.Add(green);
                        pixels8BlueModified.Add(blue);
                    }
                }
                if (bitsPerPixel == 32) // 32 bits per pixel
                {
                    for (int i = 0; i < scaledPixels.Count(); i += 4)
                    {
                        // In a 32-bit per pixel image, the bytes are stored in the order 
                        // BGR - Blue Green Red Alpha order.
                        blue = (byte)(scaledPixels[i]);
                        green = (byte)(scaledPixels[i + 1]);
                        red = (byte)(scaledPixels[i + 2]);

                        pixels8RedScaled.Add(red);
                        pixels8GreenScaled.Add(green);
                        pixels8BlueScaled.Add(blue);

                        pixels8RedScaledModified.Add(red);
                        pixels8GreenScaledModified.Add(green);
                        pixels8BlueScaledModified.Add(blue);
                    }

                    for (int i = 0; i < originalPixels.Count(); i += 4)
                    {
                        // In a 32-bit per pixel image, the bytes are stored in the order 
                        // BGR - Blue Green Red Alpha order.
                        blue = (byte)(originalPixels[i]);
                        green = (byte)(originalPixels[i + 1]);
                        red = (byte)(originalPixels[i + 2]);

                        pixels8Red.Add(red);
                        pixels8Green.Add(green);
                        pixels8Blue.Add(blue);

                        pixels8RedModified.Add(red);
                        pixels8GreenModified.Add(green);
                        pixels8BlueModified.Add(blue);
                    }
                }
            }
        }
        private void ApplyVignette()
        {
            vignette = new VignetteEffect(this);
            vignette.Angle = sliderAngle.Value;
            vignette.Coverage = sliderPercent.Value;
            vignette.BandPixels = Convert.ToInt32(sliderBand.Value);
            vignette.NumberSteps = Convert.ToInt32(sliderSteps.Value);
            vignette.Xcentre = Convert.ToInt32(sliderOriginX.Value);
            vignette.Ycentre = Convert.ToInt32(sliderOriginY.Value);
            vignette.BorderColour = colour;
            vignette.Shape = shape;
            vignette.TransferImagePixels(ref pixels8RedScaled, ref pixels8GreenScaled, ref pixels8BlueScaled,
                    scaledWidth, scaledHeight,
                    ref pixels8RedScaledModified, ref pixels8GreenScaledModified, ref pixels8BlueScaledModified,
                    ModeOfOperation.DisplayMode);
            //vignette.ApplyEffect();
        }
        public void UpdateImage(ref List<byte> pixels8RedScaledModified,
            ref List<byte> pixels8GreenScaledModified,
            ref List<byte> pixels8BlueScaledModified)
        {
            int bitsPerPixel = 24;
            int stride = (scaledWidth * bitsPerPixel + 7) / 8;
            byte[] pixelsToWrite = new byte[stride * scaledHeight];
            int i1;

            for (int i = 0; i < pixelsToWrite.Count(); i += 3)
            {
                i1 = i / 3;
                pixelsToWrite[i] = pixels8RedScaledModified[i1];
                pixelsToWrite[i + 1] = pixels8GreenScaledModified[i1];
                pixelsToWrite[i + 2] = pixels8BlueScaledModified[i1];
            }

            newImage = BitmapSource.Create(scaledWidth, scaledHeight, 96, 96, PixelFormats.Rgb24,
                null, pixelsToWrite, stride);
            imgPhoto.Source = newImage;
        }
        
    }
}