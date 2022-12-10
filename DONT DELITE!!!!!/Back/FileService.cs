 using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using static DONT_DELITE_____.MainWindow;
using _Bitmap = System.Drawing.Bitmap;
using _ImagingInterop = System.Windows.Interop.Imaging;

namespace DONT_DELITE_____.Back
{
    public class FileService
    {
        MainWindow mw = new MainWindow();
        private Bitmap currentPicture;

        public Bitmap OpenPhoto(MainWindow mainWibdowCls)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPEG Compressed Image (*.jpg)|*.jpg|GIF Image(*.gif)|*.gif|Bitmap Image(*.bmp)|*.bmp|PNG Image (*.png)|*.png";
            openFileDialog.FilterIndex = 1;

            Nullable<bool> result = openFileDialog.ShowDialog();

            if (result == true)
            {
                currentPicture = new Bitmap(openFileDialog.FileName);
                mainWibdowCls.MWImg = currentPicture;
            }
            return currentPicture;
        }
        public void SavePhoto(MainWindow mainWibdowCls, Bitmap currentPicture)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Save picture as ";
            save.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (save.ShowDialog() == true)
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(BitmapToBitmapSource(currentPicture)));
                using (Stream stm = System.IO.File.Create(save.FileName))
                {
                    encoder.Save(stm);
                }
            }
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
    }
}