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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DONT_DELITE_____
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Bitmap> bitmapList = new List<Bitmap>();
        private Bitmap currentPicture;
        string way;
        private int currentBitmap = 0;

        private ColorMatrix invertConMatrix = new ColorMatrix(
           new float[][]
            {
                new float[] { -1,   0,   0,  0,  1},
                new float[] {  0,  -1,   0,  0,  1},
                new float[] {  0,   0,  -1,  0,  1},
                new float[] {  0,   0,   0,  1,  0},
                new float[] {  1,   1,   1,  0,  1}
            });
        private ColorMatrix greyscaleConMatrix = new ColorMatrix(
            new float[][]
            {
                new float[] {.22f,.22f,.22f, 0, 0},
                new float[] {.59f,.59f,.59f, 0, 0},
                new float[] {.11f,.11f,.11f, 0, 0},
                new float[] {  0,   0,   0,  1, 0},
                new float[] {  0,   0,   0,  0, 1}
            });
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Clicks

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SavePhoto();
        }


        private void Invert_Click(object sender, RoutedEventArgs e)
        {
            InvertPicture();
        }

        private void Otraz_Click(object sender, RoutedEventArgs e)//vertical
        {
            currentPicture = bitmapList[currentBitmap];
            currentPicture.RotateFlip(RotateFlipType.Rotate180FlipY);
            addPicture(currentPicture);
        }

        private void Rotate_Click(object sender, RoutedEventArgs e)
        {
            currentPicture = bitmapList[currentBitmap];
            currentPicture.RotateFlip(RotateFlipType.Rotate270FlipXY);
            addPicture(currentPicture);
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

        private void Gray_Click(object sender, RoutedEventArgs e)
        {
            GreyPicture();
        }
        
        private void GreenSlider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            currentPicture = hue(bitmapList[0]);
        }
        private void RedSlider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            currentPicture = hue(bitmapList[0]);
        }
        private void BlueSlider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            currentPicture = hue(bitmapList[0]);
        }

        private void reload_Click(object sender, RoutedEventArgs e)
        {
            setMainPicture(0);
        }

        private void redo_Click(object sender, RoutedEventArgs e)
        {
            redoPicture();
        }

        private void undo_Click(object sender, RoutedEventArgs e)
        {
            undoPicture();
        }
        #endregion

        #region Functions
        public void OpenPhoto()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPEG Compressed Image (*.jpg)|*.jpg|GIF Image(*.gif)|*.gif|Bitmap Image(*.bmp)|*.bmp|PNG Image (*.png)|*.png";
            openFileDialog.FilterIndex = 1;

            Nullable<bool> result = openFileDialog.ShowDialog();

            if (result == true)
            {
                currentPicture = new Bitmap(openFileDialog.FileName);
                way = openFileDialog.FileName;
                addPicture(currentPicture);
                this.Title = openFileDialog.FileName;
            }
        }
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
        public void undoPicture()
        {
            if (currentBitmap > 0)
            {
                currentBitmap--;
                setMainPicture(currentBitmap);
            }
        }

        public void SavePhoto()
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Save picture as ";
            save.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (save.ShowDialog() == true)
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)imgPhoto.Source));
                using (Stream stm = System.IO.File.Create(save.FileName))
                {
                    encoder.Save(stm);
                }
            }
        }
        public void setTempPicture(Bitmap aBitmap)
        {
            imgPhoto.Source = BitmapToBitmapSource(aBitmap);
        }

        public Bitmap hue(Bitmap original)
        {
            float changered = (float)RedSlider.Value * 0.02f;
            float changegreen = (float)GreenSlider.Value * 0.02f;
            float changeblue = (float)BlueSlider.Value * 0.02f;
            Bitmap aBitmap = new Bitmap(original.Width, original.Height);
            Graphics g = Graphics.FromImage(aBitmap);

            ColorMatrix colorMatrix = new ColorMatrix(new float[][]       // now creating the color matrix object to change the colors or apply  image filter on image
                {
                    new float[]{1+changered, 0, 0, 0, 0},
                    new float[]{0, 1+changegreen, 0, 0, 0},
                    new float[]{0, 0, 1+changeblue, 0, 0},
                    new float[]{0, 0, 0, 1, 0},
                    new float[]{0, 0, 0, 0, 1}
                    //new float[] {1, 0, 0, 0, 0},
                    //new float[] {0, 1, 0, 0, 0},
                    //new float[] {0, 0, 1, 0, 0},
                    //new float[] {0, 0, 0, 1, 0},
                    //new float[] {changered, changegreen, changeblue, 0, 1}
                });;

            // Set an image attribute to our color matrix so that we can apply it to a bitmap
            ImageAttributes attr = new ImageAttributes();
            attr.SetColorMatrix(colorMatrix);

            //Uses graphics class to redraw the bitmap with our Color matrix applied
            g.DrawImage(original,                                                                   // Bitmap
                            new System.Drawing.Rectangle(0, 0, original.Width, original.Height),    // Contains the image
                            0,                                                                      // x, y, width, and height
                            0,
                            original.Width,
                            original.Height,
                            GraphicsUnit.Pixel,                                     // Unit of measure
                            attr);                                                  // Our ColorMatrix being applied
            g.Dispose();
            addPicture(aBitmap);
            return aBitmap;
        }

        private void InvertPicture()
        {
            try
            {
                if (bitmapList.Count > 0)
                {
                    currentPicture = bitmapList[currentBitmap];
                    currentPicture = MatrixConvertBitmap(currentPicture, invertConMatrix);
                    addPicture(currentPicture);
                }
                else
                {
                    MessageBox.Show("No Picture, please open a a picture to edit it");
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
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
                    DONT_DELITE_____.ColorDialog greyWindow = new DONT_DELITE_____.ColorDialog(this, "Crop");
                    //greyWindow.Show();
                }
                else
                {
                    MessageBox.Show("No Picture, please open a a picture to edit it");
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }

        public Bitmap MatrixConvertBitmap(Bitmap original, ColorMatrix cM)
        {
            Bitmap aBitmap = new Bitmap(original.Width, original.Height);
            Graphics g = Graphics.FromImage(aBitmap);

            ColorMatrix colorMatrix = cM;

            // Set an image attribute to our color matrix so that we can apply it to a bitmap
            ImageAttributes attr = new ImageAttributes();
            attr.SetColorMatrix(colorMatrix);

            //Uses graphics class to redraw the bitmap with our Color matrix applied
            g.DrawImage(original,                                                                   // Bitmap
                            new System.Drawing.Rectangle(0, 0, original.Width, original.Height),    // Contains the image
                            0,                                                                      // x, y, width, and height
                            0,
                            original.Width,
                            original.Height,
                            GraphicsUnit.Pixel,                                     // Unit of measure
                            attr);                                                  // Our ColorMatrix being applied
            g.Dispose();

            return aBitmap;
        }

        private void GreyPicture()
        {
            try
            {
                if (bitmapList.Count > 0)
                {
                    currentPicture = bitmapList[currentBitmap];
                    currentPicture = MatrixConvertBitmap(currentPicture, greyscaleConMatrix);
                    addPicture(currentPicture);
                }
                else
                {
                    MessageBox.Show("No Picture, please open a a picture to edit it");
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }

        System.Windows.Media.Imaging.BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }


        #endregion
    }
}
