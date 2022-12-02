using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Input;

namespace DONT_DELITE_____.Back
{
    internal class Effects
    {
        MainWindow mw = new MainWindow();
        private Bitmap result;
        private string path;
        private System.Windows.Controls.Image image;
        private int currentBitmap = 0;
        private Bitmap currentPicture;
        private CustomBitmap myBitmap;
        private List<Bitmap> bitmapList = new List<Bitmap>();

        private ColorMatrix invert = new ColorMatrix(
           new float[][]
           {
                new float[] { -1,   0,   0,  0,  1},
                new float[] {  0,  -1,   0,  0,  1},
                new float[] {  0,   0,  -1,  0,  1},
                new float[] {  0,   0,   0,  1,  0},
                new float[] {  1,   1,   1,  0,  1}
           });
        private ColorMatrix gray = new ColorMatrix(new float[][]       // now creating the color matrix object to change the colors or apply  image filter on image
                       {
                new float[]{0.299f, 0.299f, 0.299f, 0, 0},
                new float[]{0.587f, 0.587f, 0.587f, 0, 0},
                new float[]{0.114f, 0.114f, 0.114f, 0, 0},
                new float[]{0, 0, 0, 1, 0},
                new float[]{0, 0, 0, 0, 0}
        });

        public Bitmap Invert(MainWindow mainWindowCls, Bitmap currentPicture)
        {
            currentPicture = MatrixConvertBitmap(currentPicture, invert);
            return currentPicture;
        }

        public Bitmap Gray(MainWindow mainWindowCls, Bitmap currentPicture)
        {
            currentPicture = MatrixConvertBitmap(currentPicture, gray);
            return currentPicture;
        }

        public Bitmap Sliders(MainWindow mainWindowCls, Bitmap original)
        {
            float changered = (float)mainWindowCls.RedSlider.Value * 0.02f;
            float changegreen = (float)mainWindowCls.GreenSlider.Value * 0.02f;
            float changeblue = (float)mainWindowCls.BlueSlider.Value * 0.02f;
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
                }); ;

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
            //addPicture(aBitmap);
            return aBitmap;
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
    }
}