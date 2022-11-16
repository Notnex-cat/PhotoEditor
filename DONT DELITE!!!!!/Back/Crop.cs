using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Input;

namespace DONT_DELITE_____
{
    internal class Crop
    {
        Rectangle CropRect;
        double ZoomedRatio;
        private MainWindow myParentWindow;
        private ColorDialog myColorDialog;
        private int originalBitmapCount = new int();
        private Bitmap previewBitmap;
        public Crop(MainWindow mPW, ColorDialog cD)
        {
            myParentWindow = mPW;
            myColorDialog = cD;
            originalBitmapCount = myParentWindow.CurrentBitmap;
        }
       
        
        public void CropImageButton_Click(object sender, RoutedEventArgs e)
        {
            // output image size is based upon the visible crop rectangle and scaled to 
            // the ratio of actual image size to displayed image size
            Bitmap bmp = null;
            SaveFileDialog save = new SaveFileDialog();
            Rectangle ScaledCropRect = new Rectangle();
            ScaledCropRect.X = (int)(CropRect.X / ZoomedRatio);
            ScaledCropRect.Y = (int)(CropRect.Y / ZoomedRatio);
            ScaledCropRect.Width = (int)((double)(CropRect.Width) / ZoomedRatio);
            ScaledCropRect.Height = (int)((double)(CropRect.Height) / ZoomedRatio);

            if (save.ShowDialog() == true)
            {
                try
                {
                    bmp = (Bitmap)CropImage(previewBitmap, ScaledCropRect);
                    // 85% quality
                    saveJpeg(save.FileName, bmp, 100);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "btnOK_Click()");
                }
            }
        }
        
        void saveJpeg(string path, Bitmap img, long quality)
        {
            // Encoder parameter for image quality
            EncoderParameter qualityParam = new EncoderParameter(
                    System.Drawing.Imaging.Encoder.Quality, (long)quality);

            // Jpeg image codec
            ImageCodecInfo jpegCodec = getEncoderInfo("image/jpeg");

            if (jpegCodec == null)
            {
                MessageBox.Show("Can't find JPEG encoder?", "saveJpeg()");
                return;
            }
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            img.Save(path, jpegCodec, encoderParams);
        }
        static Image CropImage(Image img, Rectangle cropArea)
        {
            try
            {
                Bitmap bmpImage = new Bitmap(img);
                Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
                return (Image)(bmpCrop);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "CropImage()");
            }
            return null;
        }
        private ImageCodecInfo getEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];

            return null;
        }
    }
}