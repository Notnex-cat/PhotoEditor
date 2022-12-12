using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using Color = System.Windows.Media.Color;
using static DONT_DELITE_____.MainWindow;
using System.ComponentModel;

namespace DONT_DELITE_____.Back
{
    internal class Сorrection
    {
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

        VignetteEffect vignette;
        VignetteShape shape;
        System.Windows.Media.Color colour;              // Border colour
        byte red, green, blue;
        double scaleFactor;
        MainWindow mainWindowCls;

        public Сorrection()
        {
            
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
            
            vignette = null;
        }

        /// <summary>
        /// Method to read in an image.
        /// </summary>
        private bool ReadImage(string fn, string fileNameOnly)
        {
            bool retVal = false;
            // Open the image
            Uri imageUri = new Uri(fn, UriKind.RelativeOrAbsolute);
            originalImage = BitmapToBitmapSource(mainWindowCls.currentPicture);
            int stride = (BitmapToBitmapSource(mainWindowCls.bitmapList[mainWindowCls.currentBitmap]).PixelWidth * originalImage.Format.BitsPerPixel + 7) / 8;
            originalWidth = 400;//mainWindowCls.bitmapList[mainWindowCls.currentBitmap].Width;
            originalHeight = 500;// mainWindowCls.bitmapList[mainWindowCls.currentBitmap].Height;

            if ((originalImage.Format == PixelFormats.Bgra32) ||
                (originalImage.Format == PixelFormats.Bgr32))
            {
                originalPixels = new byte[4000000];
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

        /// <summary>
        /// Method to scale the original image to 600 x 600.
        /// This should preserve the aspect ratio of the original image.
        /// </summary>
        void ScaleImage(MainWindow mainWindowCls, Bitmap currentPicture)
        {
            double fac1 = Convert.ToDouble(scaledWidth / (1.0 * originalWidth));
            double fac2 = Convert.ToDouble(scaledHeight / (1.0 * originalHeight));
            scaleFactor = Math.Min(fac1, fac2);
            var scale = new ScaleTransform(fac1, fac2);
            scaledImage = new TransformedBitmap(BitmapToBitmapSource(mainWindowCls.bitmapList[mainWindowCls.currentBitmap]), scale);

            mainWindowCls.imgPhoto.Source = scaledImage;

            int stride = (scaledImage.PixelWidth * scaledImage.Format.BitsPerPixel + 7) / 8;
            scaledPixels = new byte[stride * scaledHeight];

            // Update the array scaledPixels from the scaled image
            scaledImage.CopyPixels(Int32Rect.Empty, scaledPixels, stride, 0);
        }

        /// <summary>
        /// Computes the scaled width and height of the image, so as to 
        /// maintain the aspect ratio of original image.
        /// </summary>
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
                scaledWidth = 500 * viewportWidthHeight / 400;
            }
        }

        /// <summary>
        /// Method to fill in the different pixel arrays from the original and scaled images.
        /// </summary>
        private void PopulatePixelsOriginalAndScaled(MainWindow mainWindowCls)
        {
            int bitsPerPixel = BitmapToBitmapSource(mainWindowCls.bitmapList[mainWindowCls.currentBitmap]).Format.BitsPerPixel;

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

        public Bitmap bnOpen(MainWindow mainWindowCls, Bitmap currentPicture)
        {
            ComputeScaledWidthAndHeight();
            ScaleImage(mainWindowCls, currentPicture);
            PopulatePixelsOriginalAndScaled(mainWindowCls);
            ApplyVignette(mainWindowCls, currentPicture);
            return currentPicture;
        }

        /// <summary>
        /// Method to apply the vignette
        /// </summary>
        private void ApplyVignette(MainWindow mainWindowCls, Bitmap currentPicture)
        {
            vignette = new VignetteEffect(this);
            vignette.Angle = mainWindowCls.sliderAngle.Value;
            vignette.Coverage = mainWindowCls.sliderPercent.Value;
            vignette.BandPixels = Convert.ToInt32(mainWindowCls.sliderBand.Value);
            vignette.NumberSteps = Convert.ToInt32(mainWindowCls.sliderSteps.Value);
            vignette.Xcentre = Convert.ToInt32(mainWindowCls.sliderOriginX.Value);
            vignette.Ycentre = Convert.ToInt32(mainWindowCls.sliderOriginY.Value);
            vignette.BorderColour = colour;
            vignette.Shape = shape;
            vignette.TransferImagePixels(ref pixels8RedScaled, ref pixels8GreenScaled, ref pixels8BlueScaled,
                    scaledWidth, scaledHeight,
                    ref pixels8RedScaledModified, ref pixels8GreenScaledModified, ref pixels8BlueScaledModified,
                    ModeOfOperation.DisplayMode);
            vignette.ApplyEffect();
        }

        /// <summary>
        /// Method to update the image. The Vignette class computes the modified colours and transfers
        /// these colours to the main window.
        /// </summary>
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
           mainWindowCls.imgPhoto.Source = newImage;
        }
        //private void bnSaveImage_Click(object sender, RoutedEventArgs e)
        //{
        //    SaveFileDialog dlg = new SaveFileDialog();
        //    dlg.Filter = "PNG Images (.png)|*.png|JPG Images (.jpg)|*.jpg|BMP Images (.bmp)|*.bmp";

        //    // Show save file dialog box
        //    Nullable<bool> result = dlg.ShowDialog();

        //    try
        //    {
        //        // Process save file dialog box results
        //        if (result == true)
        //        {
        //            // Save image
        //            VignetteEffect vig = new VignetteEffect(this);
        //            vig.Angle = sliderAngle.Value;
        //            vig.Coverage = sliderPercent.Value;
        //            // While all other parameters are percentages, and are independent of image 
        //            // dimensions, two parameters - Width of the band, and Number of Steps need 
        //            // to be scaled depending upon the image dimensions.
        //            // Here, the variable scaleFactor comes in handy to perform such scaling.
        //            // Though scaleFactor can never be zero, we enclose the entire saving code 
        //            // within a try-catch block, just in case things go out of control.
        //            vig.BandPixels = Convert.ToInt32(sliderBand.Value / scaleFactor);
        //            vig.NumberSteps = Convert.ToInt32(sliderSteps.Value / scaleFactor);
        //            vig.Xcentre = Convert.ToInt32(sliderOriginX.Value);
        //            vig.Ycentre = Convert.ToInt32(sliderOriginY.Value);
        //            vig.BorderColour = colour;
        //            vig.Shape = shape;
        //            string fileToSave = dlg.FileName;
        //            // I don't want the original file to be overwritten, since the vignetting operation
        //            // is a lossy one (where some pixels of the original image may be lost).
        //            // Therefore, if the user inadvertently selects the original filename for saving,
        //            // I create the new file name with an underscore _ appended to the filename.
        //            if (fileToSave == fileName)
        //            {
        //                fileToSave = GetNewFileName(fileToSave);
        //            }
        //            vig.FileNameToSave = fileToSave;

        //            Mouse.OverrideCursor = Cursors.Wait;
        //            vig.TransferImagePixels(ref pixels8Red, ref pixels8Green, ref pixels8Blue,
        //                    originalWidth, originalHeight,
        //                    ref pixels8RedModified, ref pixels8GreenModified, ref pixels8BlueModified,
        //                    ModeOfOperation.SaveMode);
        //            vig.ApplyEffect();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    finally
        //    {
        //        Mouse.OverrideCursor = null;
        //    }
        //}
    }
}
